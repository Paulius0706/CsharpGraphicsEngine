using OpenGLAbstraction.Core.Components;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes
{
    public class AbstractTextureNode<Atributes, Uniforms> : RenderNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        protected Texture texture;
        public override Texture Texture => texture;
        public AbstractTextureNode(RenderNode<Atributes, Uniforms> parent) : base(parent)
        {                  
        }
        protected sealed override void NodeLoadCheck()
        {
            base.NodeLoadCheck();
            if (Shader == null) { throw new Exception("Shader is not loaded for node"); }
            if (Texture == null) { throw new Exception("Texture is not loaded for node"); }
        }
        public sealed override void Render(FrameEventArgs args)
        {
            if (texture == null) { throw new Exception("Texture is not loaded"); }
            texture.Use();
            base.Render(args);
            texture.Unuse();
        }
        protected override void InternalDispose()
        {
            texture.Dispose();
        }
    }
}
