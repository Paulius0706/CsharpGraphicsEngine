using OpenGLAbstraction.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Natives
{
    public class TextureNode<Atributes, Uniforms> : AbstractTextureNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        public TextureNode(RenderNode<Atributes, Uniforms> parent, string path) : base(parent)
        {
            texture = new Texture(path);
        }
    }
}
