using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.Components;
using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes
{
    public abstract class AbstractLayoutRenderNode : RenderNode, ILayoutRenderNode
    {
        public AbstractLayoutRenderNode(IRenderNode parent) : base(parent) {}
        protected sealed override void NodeLoadCheck()
        {
            base.NodeLoadCheck();
            if (Layout == null) { throw new Exception("Layout is not loaded for node"); }
            if (Shader == null) { throw new Exception("Shader is not loaded for node"); }
        }
        public sealed override void Render(FrameEventArgs args)
        {
            if (_layout == null) throw new Exception("Layout is not loaded in node");
            if (!_nodes.Any() && (_nodeActionsQueue == null || !_nodeActionsQueue.Any())) return;
            _layout.Use();
            base.Render(args);
            _layout.UnUse();
        }
        protected override void InternalDispose()
        {
            _layout.Dispose();
        }
    }
}
