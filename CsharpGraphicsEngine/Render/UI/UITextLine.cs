using CsharpGameReforged.Render.UI.Objects;
using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Nodes.Helpers;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Render.UI
{

    public class UITextLine : TextLine<LetterNode<UIAtributes, UIUniforms>, UIAtributes, UIUniforms>
    {
        protected override RenderNode<UIAtributes, UIUniforms> LettersRenderNode => Program.Window.UIShaderNode.LettersRenderNode;
        public UITextLine(string text, Vector2 lowerleftPosition, int size) : base(text, lowerleftPosition, size)
        {
        }

        public override void LoadUniforms(LetterNode<UIAtributes, UIUniforms> letter)
        {
            LettersRenderNode.Shader.SetUniform("TextureSize", new Vector2(LettersRenderNode.Texture.Width, LettersRenderNode.Texture.Height));
            LettersRenderNode.Shader.SetUniform("PositionSize", new Vector4(letter.Position.X, letter.Position.Y, letter.Size.X, letter.Size.Y));
            LettersRenderNode.Shader.SetUniform("UVPositionSize", new Vector4(letter.UvPosition.X, letter.UvPosition.Y, letter.UvSize.X, letter.UvSize.Y));
        }
    }
}
