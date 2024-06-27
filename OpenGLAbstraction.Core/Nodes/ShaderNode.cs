using OpenGLAbstraction.Core.Components;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes
{
    internal class ShaderNode<Atributes, Uniforms> : RenderNode where Atributes : struct where Uniforms : struct
    {
        protected Shader<Atributes, Uniforms> shader;
        protected Uniforms StaticUniforms;
        public ShaderNode() : base()
        {
            nodes = new Dictionary<string, RenderNode>();
        }

        public sealed override void Render(FrameEventArgs args)
        {
            if (shader == null) { throw new Exception("Shader is not loaded in node"); }
            shader.Use();
            try { LoadStaticUniforms(); }
            catch { }
            base.Render(args);
            shader.UnUse();
        }
        protected virtual void LoadStaticUniforms()
        {

        }
    }
}
