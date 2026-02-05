using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Nodes.Helpers;
using OpenGLAbstraction.Core.Objects;
using OpenGLAbstraction.Core.Objects.UI.Nodes;
using OpenGLAbstraction.Core.Objects.UI.Nodes.Compound;

namespace CsharpGameReforged.Render.UI
{
    public class UITextLabel : TextLabelUINode, IUINode, ITextLabelUINode
    {
        public UITextLabel(UINode parent, Transform2D transform, BoxOptions textLabelOptions, TextOptions textOptions) : base(parent, transform, textLabelOptions, textOptions) { }
        protected override IBoxUINode CreateBox(TextLabelUINode textLabel, BoxOptions boxOptions) => new UIBox(textLabel, boxOptions);
        protected override ITextUINode CreateText(TextLabelUINode textLabel, TextOptions textOptions) => new UIText(textLabel, textOptions);
        
    }
}
