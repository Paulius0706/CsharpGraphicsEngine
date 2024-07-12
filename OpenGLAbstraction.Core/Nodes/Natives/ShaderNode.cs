using OpenGLAbstraction.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Natives
{
    public class ShaderNode<Atributes, Uniforms> : AbstractShaderNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        private Action<Shader<Atributes, Uniforms>, Uniforms> loadUniformsAction = null;
        public ShaderNode(WindowNode parent, string vertexShaderPath, string fragmentShaderPath, Action<Shader<Atributes, Uniforms>,Uniforms> loadUniformsAction) : base(parent)
        {
            this.loadUniformsAction = loadUniformsAction;
            shader = new Shader<Atributes, Uniforms>(vertexShaderPath, fragmentShaderPath);
        }
        public ShaderNode(RenderNode<Atributes, Uniforms> parent, string vertexShaderPath, string fragmentShaderPath, Action<Shader<Atributes, Uniforms>, Uniforms> loadUniformsAction) : base(parent)
        {
            this.loadUniformsAction = loadUniformsAction;
            shader = new Shader<Atributes, Uniforms>(vertexShaderPath, fragmentShaderPath);
        }
        protected override void LoadStaticUniforms()
        {
            if(loadUniformsAction != null)
            {
                loadUniformsAction.Invoke(Shader, StaticUniforms);
            }
        }
    }
}
