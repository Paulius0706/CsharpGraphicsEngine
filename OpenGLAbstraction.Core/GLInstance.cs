using OpenTK.Compute.OpenCL;
using OpenTK.Graphics.OpenGL;

namespace OpenGLAbstraction.Core
{
    public class GLInstance<T> where T : WindowNode
    {
        protected T Window { get; set; }
        private Thread thread;
        public GLInstance()
        {

        }
        public void Start()
        {
            thread = new Thread(new ThreadStart(ThreadedStart));
            thread.Start();
        }
        private void ThreadedStart()
        {
            Window = Activator.CreateInstance<T>();
            Window.Run();
            Window.Dispose();
        }
    }
}
