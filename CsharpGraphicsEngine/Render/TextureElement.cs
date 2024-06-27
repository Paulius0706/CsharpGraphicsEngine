using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGraphicsEngine.Render
{
    struct Atrib
    {

    }
    struct Unif {

    }
    internal class TextureElement : TextureNode<Atrib, Unif>
    {
        public TextureElement(Shader<Atrib, Unif> shader, Layout<Atrib, Unif> layout = null) : base(shader, layout)
        {
        }
    }
}
