using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Helpers
{
    public class LetterTextureRenderNode : AbstractTextureRenderNode, ILetterTextureRenderNode
    {
        public LetterTextureRenderNode(IRenderNode parent, string path) : base(parent)
        {
            _texture = new FontTexture(path);
        }

        protected override void InternalResize()
        {
        
        }
    }
}
