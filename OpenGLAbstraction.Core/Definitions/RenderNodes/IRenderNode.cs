using OpenGLAbstraction.Core.Definitions.Components;
using OpenGLAbstraction.Core.Nodes;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.RenderNodes
{
    public interface IRenderNode : IDisposable
    {
        public string _generatedId { get; }
        public object _lockObject { get; }
        public Dictionary<string, RenderNode> _nodes { get; }
        public ConcurrentQueue<(Action, ManualResetEvent)> _nodeActionsQueue { get; }
        public string Id { get; }
        public WindowNode Window { get; }
        public IRenderNode Parent { get; }
        public ILayout Layout { get; }
        public IShader Shader { get; }
        public ITexture Texture { get; }

        public void Dispose();
        public void LoadCheck();
        public void NodeThreadAction(Action action);
        public void Render(FrameEventArgs args);
        public void Resize();
    }
}
