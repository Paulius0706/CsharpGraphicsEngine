using OpenGLAbstraction.Core.Components;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.Components
{
    public interface IShader : IComponent
    {
        public IReadOnlyCollection<ShaderUniform> ShaderUniforms { get; }
        public IReadOnlyCollection<ShaderAttribute> ShaderAttributes { get; }
        public void SetUniform(string name, int value);
        public void SetUniform(string name, float value);
        public void SetUniform(string name, Vector2 value);
        public void SetUniform(string name, Vector3 value);
        public void SetUniform(string name, Vector4 value);
        public void SetUniform(string name, Color4 value);
        public void SetUniform(string name, ref Matrix4 value);
    }
}
