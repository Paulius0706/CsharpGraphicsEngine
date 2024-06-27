using OpenGLAbstraction.Core.Components;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes
{
    public class TextureNode<Atributes, Uniforms> : RenderNode where Atributes : struct where Uniforms : struct
    {
        protected Shader<Atributes, Uniforms> shader;
        protected Layout<Atributes, Uniforms> layout;
        protected Texture texture;
        public TextureNode(Shader<Atributes, Uniforms> shader, Layout<Atributes, Uniforms> layout = null) 
        {                  
            this.shader = shader;
            this.layout = layout;
            nodes = new Dictionary<string, RenderNode>();
        }
        public sealed override void Render(FrameEventArgs args)
        {
            if (texture == null) { throw new Exception("Texture is not loaded"); }
            texture.Use();
            base.Render(args);
            texture.Unuse();
        }
    }
}
