
using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Nodes.Helpers;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Render.UI.Objects
{
    public abstract class TextLine<Letter ,Atributes, Uniforms> where Letter : LetterNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        protected abstract RenderNode<Atributes, Uniforms> LettersRenderNode { get; }
        private List<Letter> letterNodes = new List<Letter>();
        public readonly string text;
        public TextLine(string text, Vector2 lowerleftPosition, int size) 
        {
            this.text = text;
            float offset = 0;
            foreach(var character in text)
            {
                LettersRenderNode.NodeThreadAction(() =>
                {
                    Letter letterNode = (Letter)Activator.CreateInstance(typeof(Letter), new object[] { LettersRenderNode, this, character, lowerleftPosition + Vector2.UnitX * offset, size });  //this.ConstructLetter(character, lowerleftPosition + Vector2.UnitX * offset, size);
                    offset += letterNode.Size.X;
                    letterNodes.Add(letterNode);
                });
            }
        }
        public abstract void LoadUniforms(Letter letter);
    }
}
