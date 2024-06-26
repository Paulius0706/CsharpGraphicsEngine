using OpenGLAbstraction.Core.Nodes;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core
{
    public class WindowNode : GameWindow
    {
        protected Dictionary<string, RenderNode> Nodes;
        public WindowNode() : this(800,600,"No Title") { }
        public WindowNode(string title) : this(800, 600, title) { }
        public WindowNode(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            //Code goes here
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            //Code goes here.

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
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

    }
}
