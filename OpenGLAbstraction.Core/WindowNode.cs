using OpenGLAbstraction.Core.Nodes;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core
{
    public class WindowNode : GameWindow
    {
        private int counter = 0;
        public string GeneratedId => "GEN-" + counter++;
        public bool Loaded { get; private set; } = false;
        public bool Closed { get; private set; } = false;
        public readonly object lockObject = new();

        public Dictionary<string, RenderNode> Nodes;
        public WindowNode() : this(800,600,"No Title") { }
        public WindowNode(string title) : this(800, 600, title) { }
        public WindowNode(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {
            Nodes = new Dictionary<string, RenderNode>();
        }
        protected sealed override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            Load();
            foreach (var node in Nodes.Values)
            {
                node.LoadCheck();
            }
            Loaded = true;
        }
        protected virtual void Load()
        {
        }
        protected sealed override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            List<string> nodeKeys = Nodes.Keys.Select(o => o).ToList();
            foreach(var node in nodeKeys)
            {
                try
                {
                    Nodes[node].Render(args);
                }
                catch
                {

                }
            }

            SwapBuffers();
        }

        [System.ComponentModel.EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Method must only be invoked by the base class.", true)]
        protected sealed override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }
        protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
        {
            base.OnFramebufferResize(args); 
            GL.Viewport(0, 0, args.Width, args.Height);
        }
        public override void Dispose()
        {
            Closed = true;
            lock (lockObject)
            {
                foreach (var node in Nodes)
                {
                    node.Value.Dispose();
                }
            }
            base.Dispose();
        }
    }
}
