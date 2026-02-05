using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;
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
    public class UIBox : BoxUINode, IBoxUINode
    {
        protected override IRenderNode BoxRenderNode => Program.Window.BoxLayoutRenderNode;
        public UIBox(IUINode parent, BoxOptions boxOptions) : base(parent, boxOptions) { }
        public override IBoxObjectInstanceRenderNode CreateBox(IRenderNode renderNode, IBoxUINode box, Transform2D transform) => new BoxObjectInstanceRenderNode(renderNode, this, transform);
        public override void LoadUniform(IBoxObjectInstanceRenderNode objectNode)
        {
            var mat = objectNode.Transform.MatrixInWindows;
            BoxRenderNode.Shader.SetUniform("UVPositionSize", new Vector4(0, 0, 1, 1));
            BoxRenderNode.Shader.SetUniform("Matrix", ref mat);
            BoxRenderNode.Shader.SetUniform("Color", Color);
            BoxRenderNode.Shader.SetUniform("Depth", WindowdDepth);
        }
    }
}
