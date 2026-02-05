using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Natives
{
    public class ObjectinstanceRenderNode : AbstractObjectInstanceRenderNode, IObjectInstanceRenderNode
    {
        private Action<IObjectInstanceRenderNode> loadUniformsAction = null;
        public ObjectinstanceRenderNode(IRenderNode parent) : base(parent)
        {
        }
        public override void LoadUniforms()
        {
            if (loadUniformsAction != null)
            {
                loadUniformsAction.Invoke(this);
            }
        }

        protected override void InternalResize()
        {
        
        }
    }
}
