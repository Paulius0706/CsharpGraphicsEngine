
using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Nodes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Natives
{
    public class LetterNode<Atributes, Uniforms> : AbstractObjectInstanceNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Size => new Vector2(FontSize / UvSize.Y * UvSize.X, FontSize);
        public int FontSize { get; private set; } = 20;
        public char Character { get; private set; } = '#';
        
        private FontTexture _fontTexture;
        public FontTexture FontTexture { get { if (_fontTexture == null) _fontTexture = (FontTexture)Texture; return _fontTexture; } }
        
        private FontTexture.Letter _letter;
        public FontTexture.Letter Letter { get{ if(_letter == null) _letter = FontTexture.Letters[Character]; return _letter; } }
        
        private Vector2 _uvPosition = -Vector2.One;
        public Vector2 UvPosition { get { if(_uvPosition == -Vector2.One) _uvPosition = new Vector2(Letter.LeftPixel, Letter.LowerPixel); return _uvPosition; } }
        
        private Vector2 _uvSize = -Vector2.One;
        public Vector2 UvSize { get { if(_uvSize == -Vector2.One) _uvSize = new Vector2(Letter.Width, Letter.Height); return _uvSize; } }


        public LetterNode(char character, Vector2 position, int size, RenderNode<UIAtributes, UIUniforms> parent, UIUniforms uniforms) : base(parent, uniforms)
        {
            this.Character = character;
            this.Position = position;
            this.FontSize = size;
        }
        public override void LoadUniforms()
        {
            Shader.SetUniform(nameof(uniforms.PositionSize), new Vector4(Position.X,Position.Y, FontSize / UvSize.Y * UvSize.X, FontSize));
            Shader.SetUniform(nameof(uniforms.TextureSize), new Vector2(Texture.Width, Texture.Height));
            Shader.SetUniform(nameof(uniforms.UVPositionSize), new Vector4(UvPosition.X, UvPosition.Y, UvSize.X, UvSize.Y));
        }
    }
}
