using OpenGLAbstraction.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Natives
{
    public class LayoutNode<Atributes, Uniforms> : AbstractLayoutNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        public LayoutNode(RenderNode<Atributes, Uniforms> parent, IEnumerable<Atributes> vertices, IEnumerable<int> indices = null) : base(parent)
        {
            layout = new Layout<Atributes, Uniforms>(Shader, vertices, indices);
        }
    }
}
