using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Nodes.Helpers;
using OpenGLAbstraction.Core.Objects;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Objects.UI.Nodes
{
    public abstract class TextBox<Letter, Atributes, Uniforms> : UINode where Letter : LetterNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        protected abstract RenderNode<Atributes, Uniforms> LettersRenderNode { get; }
        private List<Letter> letterNodes = new List<Letter>();
        public readonly string Text;
        public readonly int Size;
        public TextBox(UINode parent, string text, Transform2D transform, int fontSize) : base(parent, transform)
        {
            Text = text;
            Size = fontSize;
            float offset = 0;
            foreach (var character in text)
            {
                //TODO: add whiteSpace and newLine
                LettersRenderNode.NodeThreadAction(() =>
                {
                    Vector2 pixelPosition = Transform.PixelPostion + Vector2.UnitX * offset;
                    Transform2D transform = new Transform2D(LettersRenderNode.Window, pixelPosition);
                    Letter letterNode = (Letter)Activator.CreateInstance(typeof(Letter), new object[] { LettersRenderNode, this, character, transform, fontSize });  //this.ConstructLetter(character, lowerleftPosition + Vector2.UnitX * offset, size);
                    offset += letterNode.Transform.PixelSize.X;
                    letterNodes.Add(letterNode);
                });
            }
            RedistributeLetters();
        }
        protected override void InternalOnResize()
        {
            Transform.ResizeUpdate();
            RedistributeLetters();
        }
        private void RedistributeLetters()
        {
            float Xoffset = 0;
            float Yoffset = Transform.PixelSize.Y - Size;
            foreach (var node in letterNodes)
            {
                var Xoffset1 = Xoffset + node.Transform.PixelSize.X;
                if (Transform.PixelPostion.X + Transform.PixelSize.X < Transform.PixelPostion.X + Xoffset1 + node.Transform.PixelSize.X)
                {
                    Xoffset = 0;
                    Yoffset -= (float)Size * 1.2f;
                }
                Vector2 pixelPosition = Transform.PixelPostion + Vector2.UnitX * Xoffset + Vector2.UnitY * Yoffset;
                node.Transform.RelativePixelPosition = pixelPosition;
                node.Transform.ResizeUpdate();
                Xoffset += node.Transform.PixelSize.X;
            }
        }
        public abstract void LoadUniforms(Letter letter);
    }
}
