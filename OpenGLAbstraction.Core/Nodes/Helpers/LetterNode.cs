using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Objects;
using OpenGLAbstraction.Core.Objects.UI.Nodes;
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
        public readonly Transform2D Transform;


        public int FontSize { get; private set; } = 20;
        public FontTexture FontTexture => (FontTexture)Texture;

        private TransformUV _letterUV;
        public Vector2 UvPosition => _letterUV.Position;
        public Vector2 UvSize => _letterUV.Size;
        public Vector2 RealUvPosition => _letterUV.RealPosition;
        public Vector2 RealUvSize => _letterUV.RealSize;
        private Text<LetterNode<Atributes, Uniforms>, Atributes, Uniforms> _textLine;

        public LetterNode(RenderNode<Atributes, Uniforms> parent, Text<LetterNode<Atributes, Uniforms>,Atributes, Uniforms> textLine, char character, Transform2D transform, int size) : base(parent)
        {
            Character = character;
            FontSize = size;
            _textLine = textLine;
            Transform = transform;
            _letterUV = FontTexture.LettersUVs[Character];
            if(Character == ' ')
            {
                transform.PixelSize = new Vector2(FontSize / FontTexture.SpaceHeight * FontTexture.SpaceWidth, FontSize);
            }
            else
            {
                transform.PixelSize = new Vector2(FontSize / UvSize.Y * UvSize.X, FontSize);
            }
        }
        public override void LoadUniforms()
        {
            _textLine?.LoadUniforms(this);
        }
    }
}
