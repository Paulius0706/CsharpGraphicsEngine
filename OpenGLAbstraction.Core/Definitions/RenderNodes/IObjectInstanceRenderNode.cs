using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.RenderNodes
{
    public interface IObjectInstanceRenderNode : IRenderNode
    {
        void LoadUniforms();
        void Render(FrameEventArgs args);
    }
}
