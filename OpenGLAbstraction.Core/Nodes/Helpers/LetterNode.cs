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

        private Vector2 _size = Vector2.Zero;
        public Vector2 Size { get { if (_size == Vector2.Zero) _size = new Vector2(FontSize / UvSize.Y * UvSize.X, FontSize); return _size; } }

        public Vector2 RealPosition
        {
            get
            {
                return new Vector2(Position.X/Window.Size.X, Position.Y/Window.Size.Y) * 2 - Vector2.One;
            }
            set
            {
                Position = value * Window.Size / 2 * new Vector2(Window.Size.X, Window.Size.Y) * 0.5f;
            }
        }
        public Vector2 RealSize => Size / Window.Size * 2f;

        public int FontSize { get; private set; } = 20;
        public FontTexture FontTexture => (FontTexture)Texture;

        private FontTexture.Letter _letter;
        public Vector2 UvPosition => _letter.Position;
        public Vector2 UvSize => _letter.Size;
        public Vector2 RealUvPosition => _letter.RealPosition;
        public Vector2 RealUvSize => _letter.RealSize;
        private TextBox<LetterNode<Atributes, Uniforms>, Atributes, Uniforms> _textLine;

        public LetterNode(RenderNode<Atributes, Uniforms> parent, TextBox<LetterNode<Atributes, Uniforms>,Atributes, Uniforms> textLine, char character, Vector2 position, int size) : base(parent)
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
