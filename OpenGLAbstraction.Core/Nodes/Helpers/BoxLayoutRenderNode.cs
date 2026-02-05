
using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.Components;
using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;
using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Objects;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Helpers
{
    public class BoxLayoutRenderNode<T> : AbstractLayoutRenderNode, IBoxLayoutRenderNode where T : struct
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="constructor">method to construct atribute with position and uv</param>
        public BoxLayoutRenderNode(IRenderNode parent, Func<Vector3,Vector2,T> constructor) : base(parent)
        {
            _layout = new Layout<T>(Shader, new List<T>()
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

        protected override void InternalResize()
        {
        
        }
    }
}
