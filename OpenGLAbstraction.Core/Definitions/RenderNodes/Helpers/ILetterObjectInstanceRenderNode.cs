using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.Components;
using OpenGLAbstraction.Core.Objects;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers
{
    public interface ILetterObjectInstanceRenderNode : IObjectInstanceRenderNode
    {
        public char Character { get; set; }
        public int FontSize { get; }
        public IFontTexture FontTexture { get; }
        public Vector2 RealUvPosition { get; }
        public Vector2 RealUvSize { get; }
        public Vector2 UvPosition { get; }
        public Vector2 UvSize { get; }
        public Transform2D Transform { get; }
        public void LoadUniforms();
    }
}
