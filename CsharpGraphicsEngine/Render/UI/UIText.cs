using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Nodes.Helpers;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGLAbstraction.Core.Objects;
using OpenGLAbstraction.Core.Objects.UI.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;

namespace CsharpGameReforged.Render.UI
{

    public class UIText : TextUINode ,ITextUINode, IUINode
    {
        protected override IRenderNode LettersRenderNode => Program.Window.LetterLayoutRenderNode;
        public UIText(UINode parent, TextOptions textBoxOptions) : base(parent, textBoxOptions) { }
        public override ILetterObjectInstanceRenderNode CreateLetter(IRenderNode renderNode, ITextUINode text, char character, Transform2D transform, int size) => new LetterObjectInstanceRenderNode(renderNode, text, character, transform, size);
        public override void LoadUniforms(ILetterObjectInstanceRenderNode letter)
        {
            var mat = letter.Transform.MatrixInWindows;
            LettersRenderNode.Shader.SetUniform("UVPositionSize", new Vector4(letter.RealUvPosition.X, letter.RealUvPosition.Y, letter.RealUvSize.X, letter.RealUvSize.Y));
            LettersRenderNode.Shader.SetUniform("Matrix", ref mat);
            LettersRenderNode.Shader.SetUniform("Depth", WindowdDepth);
            LettersRenderNode.Shader.SetUniform("Color", new Vector4(0, 0, 0, 0));
        }

        
    }
}
