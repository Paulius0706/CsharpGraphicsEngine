
using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Nodes.Helpers;
using OpenGLAbstraction.Core.Objects;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Render.UI.Objects
{
    public abstract class TextBox<Letter ,Atributes, Uniforms> where Letter : LetterNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        protected abstract RenderNode<Atributes, Uniforms> LettersRenderNode { get; }
        private List<Letter> letterNodes = new List<Letter>();
        public readonly string Text;
        public readonly Transform2D Transform;
        public TextBox(string text, Transform2D transform, int fontSize) 
        {
            this.Transform = transform;
            this.Text = text;
            float offset = 0;
            foreach(var character in text)
            {
                LettersRenderNode.NodeThreadAction(() =>
                {
                    Vector2 pixelPosition = Transform.PixelPostion + Vector2.UnitX * offset;
                    Transform2D transform = new Transform2D(LettersRenderNode.Window, pixelPosition);
                    Letter letterNode = (Letter)Activator.CreateInstance(typeof(Letter), new object[] { LettersRenderNode, this, character, transform, fontSize });  //this.ConstructLetter(character, lowerleftPosition + Vector2.UnitX * offset, size);
                    offset += letterNode.Transform.PixelSize.X;
                    letterNodes.Add(letterNode);
                });
            }
        }
        public abstract void LoadUniforms(Letter letter);
    }
}
