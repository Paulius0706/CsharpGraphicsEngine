using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Objects.UI.Nodes;
using OpenGLAbstraction.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Definitions.Nodes;

namespace OpenGLAbstraction.Core.Nodes.Helpers
{
    public class BoxObjectInstanceRenderNode: AbstractObjectInstanceRenderNode, IBoxObjectInstanceRenderNode
    {
        public Transform2D Transform { get; private set; }
        private IBoxUINode _box;
        public BoxObjectInstanceRenderNode(IRenderNode parent, IBoxUINode box, Transform2D transform) : base(parent)
        {
            Transform = transform;
            _box = box;
        }
        public override void LoadUniforms()
        {
            _box?.LoadUniform(this);
        }

        protected override void InternalResize()
        {
        
        }
    }
}
