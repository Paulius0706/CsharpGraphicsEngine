using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Nodes.Helpers;
using OpenGLAbstraction.Core.Objects;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OpenGLAbstraction.Core.Objects.UI.Nodes
{

    public class TextBoxOptions
    {
        public readonly string Text;
        public readonly Transform2D Transform;
        public int Size { get; private set; }

        public bool InLine { get; set; } = false;
        public float LineHeight { get; set; } = 1.2f;
        public TextBoxOptions(string Text, Transform2D Transform, int Size = 20) 
        {
            this.Text = Text;
            this.Transform = Transform;
            this.Size = Size;
        }
    }
    public abstract class Text<Letter, Atributes, Uniforms> : UINode where Letter : LetterNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        public string Content { get; protected set; }
        public int Size { get; protected set; }
        public bool InLine { get { return _inLine; } set { _inLine = value; Update(); } }
        private bool _inLine = false;

        private float _lineHeight;
        public float LineHeight { get { return _lineHeight; } set { _lineHeight = value; Update(); } }

        protected abstract RenderNode<Atributes, Uniforms> LettersRenderNode { get; }
        private List<Letter> letterNodes = new List<Letter>();

        public Text(TextBoxOptions textBoxOptions) : this(null, textBoxOptions) { }
        public Text(UINode parent, TextBoxOptions textBoxOptions) : base(parent, textBoxOptions.Transform)
        {
            Content = textBoxOptions.Text;
            Size = textBoxOptions.Size;
            _inLine = textBoxOptions.InLine;
            _lineHeight = textBoxOptions.LineHeight;

            float offset = 0;
            foreach (var character in Content)
            {
                LettersRenderNode.NodeThreadAction(() =>
                {
                    Vector2 pixelPosition = Transform.PixelPostion + Vector2.UnitX * offset;
                    Transform2D transform = new Transform2D(LettersRenderNode.Window, pixelPosition);

                    Letter letterNode = (Letter)Activator.CreateInstance(typeof(Letter), new object[] { LettersRenderNode, this, character, transform, Size });  //this.ConstructLetter(character, lowerleftPosition + Vector2.UnitX * offset, size);
                    offset += letterNode.Transform.PixelSize.X;
                    letterNodes.Add(letterNode);
                });
            }
            RedistributeLetters();
        }
        protected sealed override void InternalUpdate()
        {
            Transform.Update();
            RedistributeLetters();
        }
        private void RedistributeLetters()
        {
            float Xoffset = 0;
            float Yoffset = Transform.PixelSize.Y - Size;
            foreach (var node in letterNodes)
            {
                var Xoffset1 = Xoffset + node.Transform.PixelSize.X;
                if (Transform.PixelPostion.X + Transform.PixelSize.X < Transform.PixelPostion.X + Xoffset1 + node.Transform.PixelSize.X && !InLine)
                {
                    Xoffset = 0;
                    Yoffset -= (float)Size * LineHeight;
                }
                Vector2 pixelPosition = Transform.PixelPostion + Vector2.UnitX * Xoffset + Vector2.UnitY * Yoffset;
                node.Transform.RelativePixelPosition = pixelPosition;
                node.Transform.Update();
                Xoffset += node.Transform.PixelSize.X;
            }
        }
        public abstract void LoadUniforms(Letter letter);
    }
}
