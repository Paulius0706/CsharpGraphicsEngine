using CsharpGameReforged.Render.UI.Objects;
using OpenGLAbstraction.Core.Components;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Helpers
{
    public class LetterNode<Atributes, Uniforms> : AbstractObjectInstanceNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        public char Character { get; private set; } = '#';
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Size => new Vector2(FontSize / UvSize.Y * UvSize.X, FontSize);
        public int FontSize { get; private set; } = 20;
        public FontTexture FontTexture => (FontTexture)Texture;

        private FontTexture.Letter _letter;
        public Vector2 UvPosition => _letter.Position;
        public Vector2 UvSize => _letter.Size;
        public Vector2 RealUvPosition => _letter.RealPosition;
        public Vector2 RealUvSize => _letter.RealSize;
        private TextLine<LetterNode<Atributes, Uniforms>, Atributes, Uniforms> _textLine;

        public LetterNode(RenderNode<Atributes, Uniforms> parent, TextLine<LetterNode<Atributes, Uniforms>,Atributes, Uniforms> textLine, char character, Vector2 position, int size) : base(parent)
        {
            Character = character;
            Position = position;
            FontSize = size;
            _letter = FontTexture.Letters[Character];
            _textLine = textLine;
        }
        public override void LoadUniforms()
        {
            _textLine?.LoadUniforms(this);
        }
    }
}
