using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Natives
{
    public class LayoutRenderNode<T> : AbstractLayoutRenderNode, ILayoutRenderNode where T : struct
    {
        public LayoutRenderNode(RenderNode parent, IEnumerable<T> vertices, IEnumerable<int> indices = null) : base(parent)
        {
            _layout = new Layout<T>(Shader, vertices, indices);
        }

        protected override void InternalResize()
        {
        
        }
    }
}
