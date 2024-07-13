using OpenGLAbstraction.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Helpers
{
    public class LetterTextureNode<Atributes, Uniforms> : AbstractTextureNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        public LetterTextureNode(RenderNode<Atributes, Uniforms> parent, string path) : base(parent)
        {
            texture = new FontTexture(path);
        }
    }
}
