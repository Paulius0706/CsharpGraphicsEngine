using OpenGLAbstraction.Core.Components;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes
{
    public class AbstractShaderNode<Atributes, Uniforms> : RenderNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        protected Shader<Atributes, Uniforms> shader;
        public override Shader<Atributes, Uniforms> Shader => shader;
        protected Uniforms StaticUniforms;
        public AbstractShaderNode(WindowNode parent) : base(parent) { }
        public AbstractShaderNode(RenderNode<Atributes, Uniforms> parent) : base(parent) { }

        public sealed override void Render(FrameEventArgs args)
        {
            if (shader == null) { throw new Exception("Shader is not loaded in node"); }
            shader.Use();
            try { LoadStaticUniforms(); }
            catch { }
            base.Render(args);
            shader.UnUse();
        }
        protected sealed override void NodeLoadCheck()
        {
            base.NodeLoadCheck();
            if (shader == null) { throw new Exception("Shader is not loaded for node"); }
        }
        protected virtual void LoadStaticUniforms()
        {

        }
        public void ModifyUniform(Action<Uniforms> uniformChange)
        {
            lock (lockObject)
            {
                try
                {
                    uniformChange.Invoke(StaticUniforms);
                }
                catch
                {

                }
            }
        }
        protected override void InternalDispose()
        {
            shader.Dispose();
        }
    }
}
