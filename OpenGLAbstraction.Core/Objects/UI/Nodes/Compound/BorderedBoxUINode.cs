using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;
using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Nodes.Helpers;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OpenGLAbstraction.Core.Objects.UI.Nodes.Compound
{
    public class BorderedBoxOptions : BoxOptions
    {
        public Color4 BorderColor { get; set; } = Color4.Black;
        public float BorderTopPading { get; set; } = 5f;
        public float BorderBottomPading { get; set; } = 5f;
        public float BorderRightPading { get; set; } = 5f;
        public float BorderLeftPading { get; set; } = 5f;
        private float _thickness = 3f;
        public float Thickness { get { return _thickness; } set { _thickness = value; BorderTopPading = value; BorderBottomPading = value; BorderRightPading = value; BorderLeftPading = value; } }

    }

    public abstract class BorderedBoxUINode : UINode, IBoxUINode, IBorderedBoxUINode
    {
        public readonly IBoxUINode BorderBox;
        public readonly IBoxUINode InnerBox;
        public BorderedBoxUINode(IUINode parent, Transform2D transform, BorderedBoxOptions boxOptions) : base(parent, transform)
        {
            boxOptions.Transform = new Transform2D(transform, new Vector2(), transform.SizeInPixels);
            InnerBox = CreateInnerBox(this, boxOptions);

            boxOptions.Color = boxOptions.BorderColor;
            boxOptions.RightPading = boxOptions.BorderRightPading;
            boxOptions.LeftPading = boxOptions.BorderLeftPading;
            boxOptions.BottomPading = boxOptions.BorderBottomPading;
            boxOptions.TopPading = boxOptions.BorderTopPading;

            boxOptions.Transform = new Transform2D(transform, new Vector2(), transform.SizeInPixels);
            BorderBox = CreateBorderBox(this, boxOptions);

            BorderBox.InnerBoxTransform = InnerBox.Transform;
            BorderBox.Depth += 1;
        }

        public Color4 BorderColor { get => BorderBox.Color; set => BorderBox.Color = value; }
        public float BorderTopPading { get => BorderBox.TopPading; set => BorderBox.TopPading = value; }
        public float BorderBottomPading { get => BorderBox.BottomPading; set => BorderBox.BottomPading = value; }
        public float BorderRightPading { get => BorderBox.RightPading; set => BorderBox.RightPading = value; }
        public float BorderLeftPading { get => BorderBox.LeftPading; set => BorderBox.LeftPading = value; }
        public float Thickness { set => BorderBox.Padding = value; }



        public Color4 Color { get => InnerBox.Color; set => InnerBox.Color = value; }
        public float TopPading { get => InnerBox.TopPading; set => InnerBox.TopPading = value; }
        public float BottomPading { get => InnerBox.BottomPading; set => InnerBox.BottomPading = value; }
        public float RightPading { get => InnerBox.RightPading; set => InnerBox.RightPading = value; }
        public float LeftPading { get => InnerBox.LeftPading; set => InnerBox.LeftPading = value; }
        public float Padding { set => InnerBox.Padding = value; }
        public Transform2D InnerBoxTransform { get => InnerBox.InnerBoxTransform; set => InnerBox.InnerBoxTransform = value; }


        public void LoadUniform(IBoxObjectInstanceRenderNode objectNode)
        {

        }

        protected abstract IBoxUINode CreateBorderBox(IBoxUINode borderedBox, BoxOptions boxOptions);
        protected abstract IBoxUINode CreateInnerBox(IBoxUINode borderedBox, BoxOptions boxOptions);
        
        protected override void InternalUpdate()
        {
            Transform.Update();
        }

    }
}
