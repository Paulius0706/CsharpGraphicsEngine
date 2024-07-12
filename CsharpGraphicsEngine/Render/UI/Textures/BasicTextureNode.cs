using CsharpGameReforged.Render.UI.Layouts;
using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Render.UI.Textures
{
    public class BasicTextureNode : AbstractTextureNode<UIAtributes, UIUniforms>
    {
        public LetterQuadNode QuadNode { get; set; }
        public BasicTextureNode(RenderNode<UIAtributes, UIUniforms> parent) : base(parent)
        {
            texture = new FontTexture("Render/UI/Textures/output-seomagnifier(2).png");
            QuadNode = new LetterQuadNode(this);
        }
    }
}
