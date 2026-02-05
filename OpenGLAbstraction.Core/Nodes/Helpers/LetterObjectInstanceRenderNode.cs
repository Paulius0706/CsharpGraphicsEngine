using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.Components;
using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;
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

    public class LetterObjectInstanceRenderNode : AbstractObjectInstanceRenderNode, ILetterObjectInstanceRenderNode
    {
        public char _character = '#';
        public char Character 
        {
            get => _character;
            set 
            {
                _character = value;
                _letterUV = FontTexture.LettersUVs[_character];
                if (Character == ' ')
                {
                    Transform.SizeInPixels = new Vector2(FontSize / FontTexture.SpaceHeight * FontTexture.SpaceWidth, FontSize);
                }
                else
                {
                    Transform.SizeInPixels = new Vector2(FontSize / UvSize.Y * UvSize.X, FontSize);
                }
            } 
        }
        public Transform2D Transform { get; private set; }


        public int FontSize { get; private set; } = 20;
        public IFontTexture FontTexture => (FontTexture)Texture;

        private TransformUV _letterUV;
        public Vector2 UvPosition => _letterUV.Position;
        public Vector2 UvSize => _letterUV.Size;
        public Vector2 RealUvPosition => _letterUV.RealPosition;
        public Vector2 RealUvSize => _letterUV.RealSize;
        private ITextUINode _textLine;

        public LetterObjectInstanceRenderNode(IRenderNode parent, ITextUINode textLine, char character, Transform2D transform, int size) : base(parent)
        {
            _textLine = textLine;
            Transform = transform;
            Character = character;
            FontSize = size;
            
        }
        public override void LoadUniforms()
        {
            _textLine?.LoadUniforms(this);
        }

        protected override void InternalResize()
        {
        
        }
    }
}
