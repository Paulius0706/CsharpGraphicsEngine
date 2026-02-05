using OpenGLAbstraction.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers
{
    public interface IBoxObjectInstanceRenderNode : IObjectInstanceRenderNode
    {
        public Transform2D Transform { get; }
    }
}
