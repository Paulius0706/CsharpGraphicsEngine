using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes
{
    public class RenderNode : IDisposable
    {
        protected int Counter = 0;
        public RenderNode()
        {

        }


        protected void OnRenderFrame(FrameEventArgs args)
        {

        }
        public void Dispose()
        {

        }
    }
}
