using OpenGLAbstraction.Core.Components;
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
    public class RenderNode : IDisposable
    {
        protected bool disposed = false;

        private int counter = 0;
        public string GeneratedId => "GEN-" + counter++;
        public string Id { get; private set; }

        protected readonly object lockObject = new();
        protected readonly RenderNode Parent;
        private WindowNode windowNode;
        public WindowNode Window => windowNode != null ? windowNode : Parent == null ? null : Parent.Window;

        protected Dictionary<string, RenderNode> nodes;
        protected ConcurrentQueue<(Action,ManualResetEvent)> nodeActionsQueue;
        


        public RenderNode(WindowNode window, bool createNodes = true, bool createActionQueue = true)
        {
            if (window == null) throw new Exception("Render Node should always have a parent node or window");
            this.windowNode = window;
            if (createNodes)
            {
                nodes = new Dictionary<string, RenderNode>();
            }
            if (createActionQueue)
            {
                nodeActionsQueue = new ConcurrentQueue<(Action, ManualResetEvent)>();
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
        public RenderNode(RenderNode parent, bool createNodes = true, bool createActionQueue = true)
        {
            if (parent == null) throw new Exception("Render Node should always have a parent node or window");
            Parent = parent;
            if (createNodes)
            {
                nodes = new Dictionary<string, RenderNode>();
            }
            if (createActionQueue)
            {
                nodeActionsQueue = new ConcurrentQueue<(Action, ManualResetEvent)>();
            }
            lock (Parent.lockObject)
            {
                try
                {
                    Id = Parent.GeneratedId;
                    Parent.nodes.Add(Id, this);
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
            List<string> nodeKeys = nodes.Keys.Select(o => o).ToList();
            foreach (var node in nodeKeys)
            {
                nodes[node].LoadCheck();
            }
        }
        public void NodeThreadAction(Action action)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            nodeActionsQueue.Enqueue((action, manualResetEvent));
            manualResetEvent.WaitOne(3000);
        }
        public virtual void Render(FrameEventArgs args)
        {
            lock (lockObject)
            {
                if (nodes == null) return;
                if (nodeActionsQueue != null)
                {
                    while (!nodeActionsQueue.IsEmpty)
                    {
                        if (nodeActionsQueue.TryDequeue(out (Action,ManualResetEvent) action))
                        {
                            if (disposed) break;
                            try { action.Item1.Invoke(); } catch { }
                            try { action.Item2.Set(); } catch { }
                        }
                        else
                        {
                            if (disposed) break;
                            throw new Exception("TryDequeue failed: risk for infinite loop");
                        }
                    }
                }
                if(disposed) return;
                foreach (var node in nodes.Keys)
                {
                    try
                    {
                        nodes[node].Render(args);
                    }
                    catch
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }
        private void RecDispose()
        {
            if (disposed) return;
            if (this.nodes != null)
            {
                var nodes = this.nodes.Values.ToArray();
                foreach (var node in nodes)
                {
                    node.RecDispose();
                }
            }

            InternalDispose();
        }


        public void ResizeUpdate()
        {
            InternalResizeUpdate();
            if(this.nodes != null)
            {
                var nodes = this.nodes.ToArray();
                foreach(var node in nodes)
                {
                    node.Value.ResizeUpdate();
                }
            }
        }
        protected virtual void InternalResizeUpdate()
        {

        }
        public void Dispose()
        {
            this.RecDispose();
        }
        protected virtual void InternalDispose()
        {
            throw new NotImplementedException();
        }
    }
    public class RenderNode<Attributes, Uniforms> : RenderNode, IDisposable where Attributes : struct where Uniforms : struct
    {
        
        public RenderNode<Attributes, Uniforms> Parent => (RenderNode<Attributes, Uniforms>)base.Parent;
        public virtual Shader<Attributes, Uniforms> Shader => Parent == null ? null : Parent.Shader;
        public virtual Layout<Attributes, Uniforms> Layout => Parent == null ? null : Parent.Layout;
        public virtual Texture Texture => Parent == null ? null : Parent.Texture;
        public RenderNode(RenderNode<Attributes, Uniforms> parent, bool createNodes = true, bool createActionQueue = true) : base(parent,createNodes, createActionQueue)
        {
            
        }
        public RenderNode(WindowNode window, bool createNodes = true, bool createActionQueue = true) : base(window,createNodes, createActionQueue)
        {
            
        }
        
        protected override void NodeLoadCheck()
        {
            if (Window == null) { throw new Exception("Window is not attached to nodeTree"); }
        }
        
        
    }
}
