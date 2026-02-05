using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.Components;
using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace OpenGLAbstraction.Core.Nodes
{


    public abstract class RenderNode : IRenderNode
    {

        public string _generatedId => "GEN-" + _counter++;
        public object _lockObject { get; private set; } = new();

        protected bool _disposed = false;
        protected IShader _shader = null;
        protected ILayout _layout = null;
        protected ITexture _texture = null;
        
        private WindowNode _windowNode;
        private int _counter = 0;
        
        public Dictionary<string, RenderNode> _nodes { get; private set; }
        public ConcurrentQueue<(Action, ManualResetEvent)> _nodeActionsQueue { get; private set; }
        
        public string Id { get; private set; }
        public IRenderNode Parent { get; private set; }

        public virtual IShader Shader => _shader;
        public virtual ILayout Layout => _layout;
        public virtual ITexture Texture => _texture;

        public WindowNode Window => _windowNode != null ? _windowNode : Parent == null ? null : Parent.Window;

        public RenderNode(WindowNode window, bool createNodes = true, bool createActionQueue = true)
        {
            if (window == null) throw new Exception("Render Node should always have a parent node or window");
            this._windowNode = window;
            if (createNodes)
            {
                _nodes = new Dictionary<string, RenderNode>();
            }
            if (createActionQueue)
            {
                _nodeActionsQueue = new ConcurrentQueue<(Action, ManualResetEvent)>();
            }
            lock (Window.lockObject)
            {
                try
                {
                    Id = Window.GeneratedId;
                    Window.Nodes.Add(Id, this);
                }
                catch
                {
                    throw new NotImplementedException();
                }
            }
        }
        public RenderNode(IRenderNode parent, bool createNodes = true, bool createActionQueue = true)
        {
            if (parent == null) throw new Exception("Render Node should always have a parent node or window");
            Parent = parent;
            if (createNodes)
            {
                _nodes = new Dictionary<string, RenderNode>();
            }
            if (createActionQueue)
            {
                _nodeActionsQueue = new ConcurrentQueue<(Action, ManualResetEvent)>();
            }
            lock (Parent._lockObject)
            {
                try
                {
                    Id = Parent._generatedId;
                    Parent._nodes.Add(Id, this);
                    _shader = Parent == null ? null : Parent.Shader;
                    _layout = Parent == null ? null : Parent.Layout;
                    _texture = Parent == null ? null : Parent.Texture;
                }
                catch
                {
                    throw new NotImplementedException();
                }
            }
        }
        public void LoadCheck()
        {
            NodeLoadCheck();
            NodesLoadCheck();
        }
        protected virtual void NodeLoadCheck()
        {
            if (Window == null) { throw new Exception("Window is not attached to nodeTree"); }
        }
        private void NodesLoadCheck()
        {
            List<string> nodeKeys = _nodes.Keys.Select(o => o).ToList();
            foreach (var node in nodeKeys)
            {
                _nodes[node].LoadCheck();
            }
        }
        public void NodeThreadAction(Action action)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            if(_nodeActionsQueue != null)
            {
                _nodeActionsQueue.Enqueue((action, manualResetEvent));
            }
            else
            {
                Parent._nodeActionsQueue.Enqueue((action, manualResetEvent));
            }
            manualResetEvent.WaitOne(3000);
        }
        public virtual void Render(FrameEventArgs args)
        {
            lock (_lockObject)
            {
                if (_disposed) return;
                if (_nodeActionsQueue != null)
                {
                    while (!_nodeActionsQueue.IsEmpty)
                    {
                        if (_nodeActionsQueue.TryDequeue(out (Action, ManualResetEvent) action))
                        {
                            if (_disposed) break;
                            try { action.Item1.Invoke(); } catch { }
                            action.Item2.Set();
                        }
                        else
                        {
                            if (_disposed) break;
                            throw new Exception("TryDequeue failed: risk for infinite loop");
                        }
                    }
                }
                if (_nodes == null) return;
                foreach (var node in _nodes.Keys)
                {
                    _nodes[node].Render(args);
                }
            }
        }
        public void Resize()
        {
            RecResize();
        }
        private void RecResize()
        {
            InternalResize();
            if (this._nodes != null)
            {
                var nodes = this._nodes.ToArray();
                foreach (var node in nodes)
                {
                    node.Value.RecResize();
                }
            }
        }
        protected abstract void InternalResize();

        public void Dispose()
        {
            this.RecDispose();
            //NodeThreadAction(() => this.RecDispose());
        }
        private void RecDispose()
        {
            if (_disposed) return;
            if (this._nodes != null)
            {
                var nodes = this._nodes.Values.ToArray();
                foreach (var node in nodes)
                {
                    node.RecDispose();
                }
            }
            if(Parent != null)
            {
                Parent._nodes.Remove(Id);
            }

            InternalDispose();
        }
        protected abstract void InternalDispose();
    }
}
