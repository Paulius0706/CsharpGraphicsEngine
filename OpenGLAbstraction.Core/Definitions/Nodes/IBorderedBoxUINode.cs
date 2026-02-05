using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.Nodes
{
    public interface IBorderedBoxUINode : IUINode
    {
        public Color4 BorderColor { get; set; }
        public float BorderBottomPading { get; set; }
        public float BorderLeftPading { get; set; }
        public float BorderRightPading { get; set; }
        public float BorderTopPading { get; set; }
        public Color4 Color { get; set; }
    }
}
