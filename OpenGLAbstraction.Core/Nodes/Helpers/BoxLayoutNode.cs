
using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Nodes;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Helpers
{
    public class BoxLayoutNode<Atributes, Uniforms> : AbstractLayoutNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="constructor">method to construct atribute with position and uv</param>
        public BoxLayoutNode(RenderNode<Atributes, Uniforms> parent, Func<Vector3,Vector2,Atributes> constructor) : base(parent)
        {
            layout = new Layout<Atributes, Uniforms>(Shader, new List<Atributes>()
            {
                constructor(new Vector3(1.0f,  1.0f, 0.0f), new Vector2(1.0f, 0.0f)),
                constructor(new Vector3(1.0f,  0.0f, 0.0f), new Vector2(1.0f, 1.0f)),
                constructor(new Vector3(0.0f,  0.0f, 0.0f), new Vector2(0.0f, 1.0f)),
                constructor(new Vector3(0.0f,  1.0f, 0.0f), new Vector2(0.0f, 0.0f)),
            },
            new List<int>()
            {
                3,0,1,2,1,3
            });
        }

    }
}
