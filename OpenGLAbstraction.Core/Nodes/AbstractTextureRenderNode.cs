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
    public abstract class AbstractTextureRenderNode : RenderNode, ITextureRenderNode
    {
        public AbstractTextureRenderNode(IRenderNode parent) : base(parent)
        {                  
        }
        protected sealed override void NodeLoadCheck()
        {
            base.NodeLoadCheck();
            if (Shader == null) { throw new Exception("Shader is not loaded for node"); }
            if (Texture == null) { throw new Exception("Texture is not loaded for node"); }
        }
        public sealed override void Render(FrameEventArgs args)
        {
            if (_texture == null) { throw new Exception("Texture is not loaded"); }
            if (!_nodes.Any() && (_nodeActionsQueue == null || !_nodeActionsQueue.Any())) return;
            _texture.Use();
            base.Render(args);
            _texture.UnUse();
        }
        protected override void InternalDispose()
        {
            _texture.Dispose();
        }
    }
}
