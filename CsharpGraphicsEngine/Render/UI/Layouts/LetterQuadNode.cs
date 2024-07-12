
using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Nodes;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Render.UI.Layouts
{
    public class LetterQuadNode : AbstractLayoutNode<UIAtributes, UIUniforms>
    {
        public LetterQuadNode(RenderNode<UIAtributes, UIUniforms> parent) : base(parent)
        {
            layout = new Layout<UIAtributes, UIUniforms>(Shader, new List<UIAtributes>()
            {
                new UIAtributes(new Vector3(1.0f,  1.0f, 0.0f), new Vector2(1.0f, 0.0f)),
                new UIAtributes(new Vector3(1.0f,  0.0f, 0.0f), new Vector2(1.0f, 1.0f)),
                new UIAtributes(new Vector3(0.0f,  0.0f, 0.0f), new Vector2(0.0f, 1.0f)),
                new UIAtributes(new Vector3(0.0f,  1.0f, 0.0f), new Vector2(0.0f, 0.0f))
            },
            new List<int>()
            {
                3,0,1,2,1,3
            });

            // triangle.load(
            // UIVertex::AssignAtributes
            //, new UIVertex[4]{
            //     // positions         // colors
            //     {{ 1.0f,  1.0f, 0.0f},  /*{1.0f, 0.0f, 0.0f, 1.0f},*/ {1.0f, 1.0f}},
            //     {{ 1.0f,  0.0f, 0.0f},  /*{0.0f, 1.0f, 0.0f, 1.0f},*/ {1.0f, 0.0f}},
            //     {{ 0.0f,  0.0f, 0.0f},  /*{0.0f, 0.0f, 1.0f, 1.0f},*/ {0.0f, 0.0f}},
            //     {{ 0.0f,  1.0f, 0.0f},  /*{0.0f, 0.0f, 1.0f, 1.0f},*/ {0.0f, 1.0f}}
            //}
            //, 4
            //, new int[6] { 3, 0, 1, 2, 1, 3 }
            //, 6)
        }

    }
}
