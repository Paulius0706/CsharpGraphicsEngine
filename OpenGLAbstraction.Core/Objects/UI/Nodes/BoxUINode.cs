
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Objects.UI.Nodes
{
    public class BoxOptions
    {
        public Color4 Color { get; set; } = Color4.White;
        public float TopPading { get; set; } = 5f;
        public float BottomPading { get; set; } = 5f;
        public float RightPading { get; set; } = 5f;
        public float LeftPading { get; set; } = 5f;
        private float padding;
        public float Padding { get { return padding; } set { padding = value; TopPading = value; BottomPading = value; RightPading = value; LeftPading = value; } }
        public Transform2D Transform { get; set; }

    }

    public abstract class BoxUINode : UINode, IBoxUINode 
    {
        protected abstract IRenderNode BoxRenderNode { get; }
        private IBoxObjectInstanceRenderNode box;
        public Color4 Color { get; set; }

        private float _topPading = 5f;
        public float TopPading 
        { 
            get => _topPading; 
            set 
            {
                _topPading = value;
                UpdateInnerBox();
            }
        }
        
        private float _bottomPading = 5f;
        public float BottomPading
        {
            get => _bottomPading;
            set
            {
                _bottomPading = value;
                UpdateInnerBox();
            }
        }
        
        private float _rightPading = 5f;
        public float RightPading
        {
            get => _rightPading;
            set
            {
                _rightPading = value;
                UpdateInnerBox();
            }
        }
        
        private float _leftPading = 5f;
        public float LeftPading
        {
            get => _leftPading;
            set
            {
                _leftPading = value;
                UpdateInnerBox();
            }
        }
        public float Padding { set { _topPading = value; _bottomPading = value; _rightPading = value; _leftPading = value; UpdateInnerBox(); } }
        private Transform2D _innerBoxTransform;
        public Transform2D InnerBoxTransform { get => _innerBoxTransform; set { _innerBoxTransform = value; UpdateInnerBox(); } }
        public BoxUINode(IUINode parent, BoxOptions boxOptions) : base(parent, boxOptions.Transform)
        {
            _topPading = boxOptions.TopPading;
            _bottomPading = boxOptions.BottomPading;
            _rightPading = boxOptions.RightPading;
            _leftPading = boxOptions.LeftPading;
            //_innerBoxTransform = new Transform2D(this.Transform, new Vector2(RightPading, BottomPading), this.Transform.SizeInPixels - new Vector2(LeftPading + RightPading, TopPading + BottomPading));
            this.Color = boxOptions.Color;
            BoxRenderNode.NodeThreadAction(() =>
            {
                box = CreateBox(BoxRenderNode, this, Transform);
            });
        }
        protected override void InternalUpdate()
        {
            Transform.Update();
            UpdateInnerBox();
        }

        private void UpdateInnerBox()
        {
            if (InnerBoxTransform == null) return;
            InnerBoxTransform.RotationInRadians = 0;
            InnerBoxTransform.RelativePositionInPixels = new Vector2(RightPading, BottomPading);
            InnerBoxTransform.SizeInPixels = this.Transform.SizeInPixels - new Vector2(LeftPading + RightPading, TopPading + BottomPading);
        }
        public abstract void LoadUniform(IBoxObjectInstanceRenderNode objectNode);
        public abstract IBoxObjectInstanceRenderNode CreateBox(IRenderNode renderNode, IBoxUINode box, Transform2D transform);
    }
}
