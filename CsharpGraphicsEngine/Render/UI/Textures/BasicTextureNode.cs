using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Nodes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Render.UI.Textures
{
    public class BasicTextureNode : AbstractTextureNode<UIAtributes, UIUniforms>
    {
        public LetterQuadNode<UIAtributes, UIUniforms> QuadNode { get; set; }
        public BasicTextureNode(RenderNode<UIAtributes, UIUniforms> parent) : base(parent)
        {
            texture = new FontTexture("Render/UI/Textures/output-seomagnifier(2).png");
            QuadNode = new LetterQuadNode<UIAtributes, UIUniforms>(this, (pos, uv) => new UIAtributes(pos,uv));
        }
    }
}
