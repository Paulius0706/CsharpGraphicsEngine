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
        protected readonly object lockObject = new();

        protected Dictionary<string, RenderNode> nodes;
        public RenderNode(bool createNodes = true)
        {
            if (createNodes)
            {
                nodes = new Dictionary<string, RenderNode>();
            }
        }
        public virtual void Render(FrameEventArgs args)
        {
            List<string> nodeKeys = nodes.Keys.Select(o => o).ToList();
            foreach (var node in nodeKeys)
            {
                try
                {
                    nodes[node].Render(args);
                }
                catch
                {

                }
            }
        }

        public void Dispose()
        {

        }
    }
}
