using CsharpGraphicsEngine.Render;
using OpenGLAbstraction.Core;

namespace CsharpGraphicsEngine
{
    public class Program
    {
        public static ApplicationWindow Window;
        static void Main(string[] args)
        {
            //create thread here for all other logic
            Console.WriteLine("Hello, World!");
            Window = new ApplicationWindow("Hello");
            Window.Run();
            Window.Dispose();
        }
    }
}
