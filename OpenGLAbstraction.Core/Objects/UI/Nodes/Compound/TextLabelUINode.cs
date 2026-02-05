using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;
using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Nodes.Helpers;
using OpenGLAbstraction.Core.Nodes.Natives;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Objects.UI.Nodes.Compound
{
    public class TextLabelUINodeOptions : BoxOptions
    {

    }
    public abstract class TextLabelUINode : UINode , IUINode, ITextLabelUINode
    {
        public readonly IBoxUINode Box;
        public readonly ITextUINode Text;
        public TextLabelUINode(IUINode parent, Transform2D transform, BoxOptions boxOptions, TextOptions textOptions) : base(parent, transform)
        {
            boxOptions.Transform = new Transform2D(transform, new Vector2(), transform.SizeInPixels);
            Box = CreateBox(this, boxOptions);
            textOptions.Transform = new Transform2D(transform, new Vector2(), transform.SizeInPixels);

            Text =  CreateText(this, textOptions);
            Box.InnerBoxTransform = Text.Transform;
            Text.Depth -= 1;
        }

        public string Content { get => Text.Content; set { Text.Content = value; } }
        public bool InLine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float LineHeight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int FontSize => throw new NotImplementedException();

        public ILetterObjectInstanceRenderNode CreateLetter(IRenderNode renderNode, ITextUINode text, char character, Transform2D transform, int size)
        {
            throw new NotImplementedException();
        }

        public void LoadUniforms(ILetterObjectInstanceRenderNode letter)
        {
            throw new NotImplementedException();
        }

        protected abstract IBoxUINode CreateBox(TextLabelUINode textLabel, BoxOptions boxOptions);
        protected abstract ITextUINode CreateText(TextLabelUINode textLabel, TextOptions textLabelOptions);

        protected override void InternalUpdate()
        {
            Transform.Update();
        }
    }
}
