using OpenGLAbstraction.Core.Definitions.Components;
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
    public class Shader : IShader//<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
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

        public IReadOnlyCollection<ShaderUniform> ShaderUniforms     => shaderUniforms.Values.ToList();
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
        public void SetUniform(string name, Color4 value)
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
    }
}
