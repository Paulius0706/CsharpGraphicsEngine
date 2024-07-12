using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Components
{
    public readonly struct ShaderUniform
    {
        public readonly string Name;
        public readonly int Location;
        public readonly ActiveUniformType Type;

        public ShaderUniform(string Name, int Location, ActiveUniformType Type)
        {
            this.Name = Name;
            this.Location = Location;
            this.Type = Type;
        }
    }
    public readonly struct ShaderAttribute
    {
        public readonly string Name;
        public readonly int Location;
        public readonly ActiveAttribType Type;

        public ShaderAttribute(string Name, int Location, ActiveAttribType Type)
        {
            this.Name = Name;
            this.Location = Location;
            this.Type = Type;
        }
    }
    public class Shader<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        private bool disposed;

        public readonly string name;
        public readonly int ShaderProgramHandle;

        public readonly int VertexShaderHandle;
        public readonly int FragmentShaderHandle;

        private readonly IReadOnlyDictionary<string, ShaderUniform> shaderUniforms;
        private readonly ShaderAttribute[] shaderAttributes;



        public Shader(string vertexShaderPath = "shader.vert", string fragmentShaderPath = "shader.frag")
        {
            this.name = name;
            string errorMessage;

            if (!CompileVertexShader(out VertexShaderHandle, out errorMessage, vertexShaderPath)) { throw new ArgumentException(errorMessage); }
            if (!CompileFragmentShader(out FragmentShaderHandle, out errorMessage, fragmentShaderPath)) { throw new ArgumentException(errorMessage); }

            ShaderProgramHandle = CreateLinkProgram(VertexShaderHandle, FragmentShaderHandle);

            shaderUniforms = CreateUniformList();
            shaderAttributes = CreateAtributeList();

            CheckGenericAtributesWithGLSL();
            CheckGenericUniformsWithGLSL();

        }
        ~Shader()
        {
            Dispose();
        }
        

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            GL.DeleteShader(VertexShaderHandle);
            GL.DeleteShader(FragmentShaderHandle);

            GL.UseProgram(0);
            GL.DeleteProgram(ShaderProgramHandle);

            GC.SuppressFinalize(this);
        }

        public IReadOnlyCollection<ShaderUniform> ShaderUniforms => shaderUniforms.Values.ToList();
        public IReadOnlyCollection<ShaderAttribute> ShaderAttributes => shaderAttributes;

        public void Use()
        {
            GL.UseProgram(ShaderProgramHandle);
        }
        public void UnUse()
        {
            GL.UseProgram(0);
        }

        public void SetUniform(string name, int value)
        {
            if (!GetShaderUniform(name, out ShaderUniform shaderUniform)) { throw new ArgumentException("uniformName not found " + name); }
            if (shaderUniform.Type != ActiveUniformType.Int && shaderUniform.Type != ActiveUniformType.Sampler2D) { throw new ArgumentException("uniform is not float. it is " + shaderUniform.Type); }

            GL.Uniform1(shaderUniform.Location, value);
        }
        public void SetUniform(string name, float value)
        {
            if (!GetShaderUniform(name, out ShaderUniform shaderUniform)) { throw new ArgumentException("uniformName not found " + name); }
            if (shaderUniform.Type != ActiveUniformType.Float) { throw new ArgumentException("uniform is not float. it is " + shaderUniform.Type); }

            GL.Uniform1(shaderUniform.Location, value);
        }
        public void SetUniform(string name, Vector2 value)
        {
            if (!GetShaderUniform(name, out ShaderUniform shaderUniform)) { throw new ArgumentException("uniformName not found " + name); }
            if (shaderUniform.Type != ActiveUniformType.FloatVec2) { throw new ArgumentException("uniform is not floatVec2. it is " + shaderUniform.Type); }

            GL.Uniform2(shaderUniform.Location, value);
        }
        public void SetUniform(string name, Vector3 value)
        {
            if (!GetShaderUniform(name, out ShaderUniform shaderUniform)) { throw new ArgumentException("uniformName not found " + name); }
            if (shaderUniform.Type != ActiveUniformType.FloatVec3) { throw new ArgumentException("uniform is not floatVec3. it is " + shaderUniform.Type); }

            GL.Uniform3(shaderUniform.Location, value);
        }
        public void SetUniform(string name, Vector4 value)
        {
            if (!GetShaderUniform(name, out ShaderUniform shaderUniform)) { throw new ArgumentException("uniformName not found " + name); }
            if (shaderUniform.Type != ActiveUniformType.FloatVec4) { throw new ArgumentException("uniform is not floatVec4. it is " + shaderUniform.Type); }

            GL.Uniform4(shaderUniform.Location, value);
        }
        public void SetUniform(string name, ref Matrix4 value)
        {
            if (!GetShaderUniform(name, out ShaderUniform shaderUniform)) { throw new ArgumentException("uniformName not found " + name); }
            if (shaderUniform.Type != ActiveUniformType.FloatMat4) { throw new ArgumentException("uniform is not floatMarix4. it is " + shaderUniform.Type); }

            GL.UniformMatrix4(shaderUniform.Location, true, ref value);
        }


        private bool GetShaderUniform(string name, out ShaderUniform shaderUniform)
        {
            if (shaderUniforms.TryGetValue(name, out shaderUniform)) 
            {
                return true;
            }
            shaderUniform = new ShaderUniform();
            return false;
        }


        private bool CompileVertexShader(out int vertexShaderHandle, out string errorMessage, string vertexShaderPath = "shader.vert")
        {
            int success;
            string vertexShaderCode = File.ReadAllText(vertexShaderPath);
            vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderHandle, vertexShaderCode);
            GL.CompileShader(vertexShaderHandle);
            GL.GetShader(vertexShaderHandle, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                errorMessage = GL.GetShaderInfoLog(vertexShaderHandle);
                return false;
            }
            errorMessage = "";
            return true;
        }
        private bool CompileFragmentShader(out int fragmentShaderHandle, out string errorMessage, string fragmentShaderPath = "shader.frag")
        {
            int success;
            string fragmentShaderCode = File.ReadAllText(fragmentShaderPath);
            fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderHandle, fragmentShaderCode);
            GL.CompileShader(fragmentShaderHandle);
            GL.GetShader(fragmentShaderHandle, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                errorMessage = GL.GetShaderInfoLog(fragmentShaderHandle);
                return false;
            }
            errorMessage = "";
            return true;
        }

        private int CreateLinkProgram(int VertexShaderHandle, int FragmentShaderHandle)
        {
            int ShaderProgramHandle = GL.CreateProgram();
            GL.AttachShader(ShaderProgramHandle, VertexShaderHandle);
            GL.AttachShader(ShaderProgramHandle, FragmentShaderHandle);

            GL.LinkProgram(ShaderProgramHandle);

            // Delete shaders
            GL.DetachShader(ShaderProgramHandle, VertexShaderHandle);
            GL.DetachShader(ShaderProgramHandle, FragmentShaderHandle);

            return ShaderProgramHandle;
        }

        private Dictionary<string, ShaderUniform> CreateUniformList()
        {
            GL.GetProgram(ShaderProgramHandle, GetProgramParameterName.ActiveUniforms, out int uniformsCount);
            ShaderUniform[] shaderUniforms = new ShaderUniform[uniformsCount];
            for (int i = 0; i < uniformsCount; i++)
            {
                GL.GetActiveUniform(ShaderProgramHandle, i, 256, out _, out _, out ActiveUniformType activeUniformType, out string uniformName);
                int location = GL.GetUniformLocation(ShaderProgramHandle, uniformName);
                shaderUniforms[i] = new ShaderUniform(uniformName, location, activeUniformType);
            }
            return shaderUniforms.ToDictionary(o => o.Name, o => o);
        }
        private ShaderAttribute[] CreateAtributeList()
        {
            GL.GetProgram(ShaderProgramHandle, GetProgramParameterName.ActiveAttributes, out int attributesCount);
            ShaderAttribute[] shaderAttributes = new ShaderAttribute[attributesCount];
            for (int i = 0; i < attributesCount; i++)
            {
                GL.GetActiveAttrib(ShaderProgramHandle, i, 256, out _, out _, out ActiveAttribType activeAttributeType, out string layoutName);
                int location = GL.GetAttribLocation(ShaderProgramHandle, layoutName);
                shaderAttributes[i] = new ShaderAttribute(layoutName, location, activeAttributeType);
            }
            return shaderAttributes;
        }

        private void CheckGenericAtributesWithGLSL()
        {
            List<string> Error = new List<string>();

            //Get VertexStruct info
            Type vertexStructType = typeof(Atributes);
            FieldInfo[] vertexStructFields = vertexStructType.GetFields();


            //Check if VertexStruct has all fields from layoutFields
            foreach (ShaderAttribute attrib in shaderAttributes)
            {
                Type atribType = null;
                switch (attrib.Type)
                {
                    case ActiveAttribType.Float:
                        atribType = typeof(float);
                        break;
                    case ActiveAttribType.FloatVec2:
                        atribType = typeof(Vector2);
                        break;
                    case ActiveAttribType.FloatVec3:
                        atribType = typeof(Vector3);
                        break;
                    case ActiveAttribType.FloatVec4:
                        atribType = typeof(Vector4);
                        break;
                    default: throw new NotImplementedException();
                }

                if(!vertexStructFields.Any(o => o.Name == attrib.Name))
                {
                    Error.Add($"Shader layout (location = {attrib.Location}) in {atribType.Name} {attrib.Name}; is not present in shader generic struct {vertexStructType.Name}");
                    continue;
                }


                //Addtional check in VertexStruct has same type as layoutField
                FieldInfo vertexStructFieldMatch = vertexStructFields.FirstOrDefault(o => o.Name == attrib.Name);
                if(vertexStructFieldMatch.FieldType != atribType)
                {

                    Error.Add($"Shader layout (location = {attrib.Location}) in {atribType.Name} {attrib.Name}; is not same type as shader generic struct {vertexStructType.Name}.{vertexStructFieldMatch.Name} type {vertexStructFieldMatch.FieldType.Name}");
                    continue;
                }
            }


            //Check if VertexStruct has no additional Fields from layoutFields
            foreach(FieldInfo vertexStructField in vertexStructFields)
            {
                if(!shaderAttributes.Select(o => o.Name).Any(o => o == vertexStructField.Name))
                {
                    Error.Add($"Shader generic struct {vertexStructType.Name}.{vertexStructField.Name} not exist in shader layout");
                    continue;
                }
            }
            if(Error.Count > 0)
            {
                throw new Exception("Shader Layout<->GenericAtribute not match\n" + Error.Aggregate((o1, o2) => $"{o1}\n{o2}"));
            }
        }
        private void CheckGenericUniformsWithGLSL()
        {
            List<string> Error = new List<string>();

            //Get UniformsStruct info
            Type uniformStructType = typeof(Uniforms);
            FieldInfo[] uniformStructFields = uniformStructType.GetFields();

            foreach (ShaderUniform uniform in ShaderUniforms)
            {
                Type shaderUniformType = null;
                if(uniform.Type.ToString().Contains("Image") || uniform.Type.ToString().Contains("Sampler"))
                {
                    continue;
                }
                switch (uniform.Type)
                {
                    case ActiveUniformType.Float:
                        shaderUniformType = typeof(float);
                        break;
                    case ActiveUniformType.FloatVec2:
                        shaderUniformType = typeof(Vector2);
                        break;
                    case ActiveUniformType.FloatVec3:
                        shaderUniformType = typeof(Vector3);
                        break;
                    case ActiveUniformType.FloatVec4:
                        shaderUniformType = typeof(Vector4);
                        break;
                    case ActiveUniformType.FloatMat4:
                        shaderUniformType = typeof(Matrix4);
                        break;
                    default: throw new Exception();
                }

                if (!uniformStructFields.Any(o => o.Name == uniform.Name))
                {
                    Error.Add($"Shader Uniform {shaderUniformType.Name} {uniform.Name}; is not present in shader generic struct {uniformStructType.Name}");
                    continue;
                }


                //Addtional check in VertexStruct has same type as layoutField
                FieldInfo uniformStructFieldMatch = uniformStructFields.FirstOrDefault(o => o.Name == uniform.Name);
                if (uniformStructFieldMatch.FieldType != shaderUniformType)
                {
                    Error.Add($"Shader Uniform {shaderUniformType.Name} {uniform.Name} is not same type as shader generic struct {uniformStructType.Name}.{uniformStructFieldMatch.Name} type {uniformStructFieldMatch.FieldType.Name}");
                    continue;
                }
            }
            //Check if VertexStruct has no additional Fields from layoutFields
            foreach (FieldInfo uniformStructField in uniformStructFields)
            {
                if (!ShaderUniforms.Select(o => o.Name).Any(o => o == uniformStructField.Name))
                {
                    Error.Add($"Shader generic struct {uniformStructType.Name}.{uniformStructField.Name} not exist in shader uniform");
                    continue;
                }
            }
            if (Error.Count > 0)
            {
                throw new Exception("Shader Uniform<->GenericUniform not match\n" + Error.Aggregate((o1, o2) => $"{o1}\n{o2}"));
            }
        }

    }
}
