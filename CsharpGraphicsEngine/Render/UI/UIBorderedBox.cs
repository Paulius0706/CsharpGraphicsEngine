using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Objects;
using OpenGLAbstraction.Core.Objects.UI.Nodes;
using OpenGLAbstraction.Core.Objects.UI.Nodes.Compound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Render.UI
{
    public class UIBorderedBox : BorderedBoxUINode, IBoxUINode
    {
        public UIBorderedBox(UINode parent, Transform2D transform, BorderedBoxOptions boxOptions) : base(parent, transform, boxOptions)
        {
        }
        protected override IBoxUINode CreateBorderBox(IBoxUINode borderedBox, BoxOptions boxOptions) => new UIBox(borderedBox, boxOptions);
        protected override IBoxUINode CreateInnerBox(IBoxUINode borderedBox, BoxOptions boxOptions) => new UIBox(borderedBox, boxOptions);
    }
}
