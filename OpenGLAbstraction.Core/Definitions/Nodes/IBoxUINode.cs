using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Objects;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.Nodes
{
    public interface IBoxUINode : IUINode
    {
        Color4 Color { get; set; }
        public float TopPading { get; set; }
        public float BottomPading { get; set; }
        public float RightPading { get; set; }
        public float LeftPading { get; set; }
        public float Padding { set; }
        public Transform2D InnerBoxTransform { get; set; }
        public void LoadUniform(IBoxObjectInstanceRenderNode objectNode);
        //IBoxLayoutNode CreateBox(IRenderNode renderNode, IBoxUINode box, Transform2D transform);
    }
}
