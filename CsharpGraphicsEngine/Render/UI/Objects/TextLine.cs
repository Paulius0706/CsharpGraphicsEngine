using CsharpGameReforged.Render.UI.NodeObjects;
using OpenGLAbstraction.Core.Nodes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Render.UI.Objects
{
    public class TextLine
    {
        public readonly string text;
        List<LetterNode> letterNodes = new List<LetterNode>();
        public TextLine(RenderNode<UIAtributes, UIUniforms> renderNode, string text, Vector2 lowerleftPosition, int size) 
        {
            this.text = text;
            float offset = 0;
            foreach(var chareacter in text)
            {
                renderNode.NodeThreadAction(() =>
                {
                    LetterNode letterNode = new LetterNode(chareacter, lowerleftPosition + Vector2.UnitX * offset, size, renderNode, new UIUniforms());
                    offset += letterNode.Size.X;
                    letterNodes.Add(letterNode);

                });
            }
        }
    }
}
