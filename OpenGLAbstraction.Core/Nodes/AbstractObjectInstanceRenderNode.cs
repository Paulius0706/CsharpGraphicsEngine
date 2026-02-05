using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenGLAbstraction.Core.Nodes
{

    public abstract class AbstractObjectInstanceRenderNode : RenderNode, IObjectInstanceRenderNode
    {
        public AbstractObjectInstanceRenderNode(IRenderNode parent) : base(parent, false, false)
        {

        }
        protected sealed override void NodeLoadCheck()
        {
            base.NodeLoadCheck();
            if (Layout == null) { throw new Exception("Layout is not loaded for node"); }
            if (Shader == null) { throw new Exception("Shader is not loaded for node"); }
        }
        public sealed override void Render(FrameEventArgs args)
        {
            LoadUniforms();
            Layout.Render();
        }
        public virtual void LoadUniforms()
        {

        }
        protected override void InternalDispose()
        {

        }
    }
}
