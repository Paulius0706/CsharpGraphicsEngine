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
    public abstract class Box<BoxNode, Atributes, Uniforms> : UINode where BoxNode : BoxNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        protected abstract RenderNode<Atributes, Uniforms> BoxRenderNode { get; }
        private BoxNode box;
        public Color4 Color { get; set; }
        public Box(UINode parent, Transform2D transform, Color4 color) : base(parent, transform)
        {
            this.Color = color;
            BoxRenderNode.NodeThreadAction(() =>
            {
                box = (BoxNode)Activator.CreateInstance(typeof(BoxNode), new object[] { BoxRenderNode, this, transform});
            });
        }
        protected override void InternalUpdate()
        {
            Transform.Update();
        }
        public abstract void LoadUniform(BoxNode objectNode);
    }
}
