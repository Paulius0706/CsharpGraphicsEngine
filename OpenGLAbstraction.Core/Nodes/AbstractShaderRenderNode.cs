using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.Components;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes
{
    public abstract class AbstractShaderRenderNode : RenderNode, IShaderRenderNode
    {
        private Action loadUniformsAction = null;
        public IReadOnlyDictionary<string, int> StaticUniforms { get; set; }
        public AbstractShaderRenderNode(WindowNode parent) : base(parent) { }
        public AbstractShaderRenderNode(IRenderNode parent) : base(parent) { }

        public sealed override void Render(FrameEventArgs args)
        {

            if (_shader == null) { throw new Exception("Shader is not loaded in node"); }
            if (!_nodes.Any() && (_nodeActionsQueue == null || !_nodeActionsQueue.Any())) return;
            _shader.Use();
            if (loadUniformsAction != null) loadUniformsAction.Invoke();
            base.Render(args);
            _shader.UnUse();
        }
        protected sealed override void NodeLoadCheck()
        {
            base.NodeLoadCheck();
            if (_shader == null) { throw new Exception("Shader is not loaded for node"); }
        }
        public void SetGlobalUniforms(Action uniformChange = null)
        {
            loadUniformsAction = uniformChange;
        }
        protected override void InternalDispose()
        {
            _shader.Dispose();
        }
    }
}
