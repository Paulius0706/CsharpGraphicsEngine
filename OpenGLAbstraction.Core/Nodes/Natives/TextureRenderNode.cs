using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Natives
{
    public class TextureRenderNode : AbstractTextureRenderNode, ITextureRenderNode
    {
        public TextureRenderNode(IRenderNode parent, string path) : base(parent)
        {
            _texture = new Texture(path);
        }

        protected override void InternalResize()
        {
        
        }
    }
}
