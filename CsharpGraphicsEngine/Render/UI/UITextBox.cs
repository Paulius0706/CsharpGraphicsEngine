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

namespace CsharpGameReforged.Render.UI
{

    public class UITextBox : TextBox<LetterNode<UIAtributes, UIUniforms>, UIAtributes, UIUniforms>
    {
        protected override RenderNode<UIAtributes, UIUniforms> LettersRenderNode => Program.Window.UIRender.LettersRenderNode;
        public UITextBox(UINode parent, string text, Transform2D transform, int size) : base(parent, text, transform, size)
        {
        }

        public override void LoadUniforms(LetterNode<UIAtributes, UIUniforms> letter)
        {
            //LettersRenderNode.Shader.SetUniform("TextureSize", new Vector2(LettersRenderNode.Texture.Width, LettersRenderNode.Texture.Height));
            LettersRenderNode.Shader.SetUniform("PositionSize", new Vector4(letter.Transform.WindowPosition.X, letter.Transform.WindowPosition.Y, letter.Transform.WindowSize.X, letter.Transform.WindowSize.Y));
            LettersRenderNode.Shader.SetUniform("UVPositionSize", new Vector4(letter.RealUvPosition.X, letter.RealUvPosition.Y, letter.RealUvSize.X, letter.RealUvSize.Y));
            LettersRenderNode.Shader.SetUniform("Depth", WindowdDepth);
            LettersRenderNode.Shader.SetUniform("Color", new Vector4(0, 0, 0, 0));
        }

        protected override void InternalUpdate()
        {

        }
    }
}
