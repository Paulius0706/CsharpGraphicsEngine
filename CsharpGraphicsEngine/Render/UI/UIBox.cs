using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Nodes.Helpers;
using OpenGLAbstraction.Core.Objects;
using OpenGLAbstraction.Core.Objects.UI.Nodes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Render.UI
{
    public class UIBox : Box<BoxNode<UIAtributes, UIUniforms>, UIAtributes, UIUniforms>
    {
        protected override RenderNode<UIAtributes, UIUniforms> BoxRenderNode => Program.Window.UIRender.BoxRenderNode;
        public UIBox(UINode parent, Transform2D transform, Color4 color) : base(parent, transform, color)
        {

        }


        public override void LoadUniform(BoxNode<UIAtributes, UIUniforms> objectNode)
        {
            BoxRenderNode.Shader.SetUniform("PositionSize", new Vector4(Transform.PositionInWindows.X, Transform.PositionInWindows.Y, Transform.SizeInWindows.X, Transform.SizeInWindows.Y));
            BoxRenderNode.Shader.SetUniform("UVPositionSize", new Vector4(0, 0, 1, 1));
            BoxRenderNode.Shader.SetUniform("Color", Color);
            BoxRenderNode.Shader.SetUniform("Depth", WindowdDepth);

        }
    }
}
