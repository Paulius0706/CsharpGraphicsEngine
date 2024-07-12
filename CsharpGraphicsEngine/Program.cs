
using CsharpGameReforged.Logic;
using CsharpGameReforged.Render;
using OpenGLAbstraction.Core;

namespace CsharpGameReforged
{
    public class Program
    {
        public static GameLoop GameLoop;
        public static ApplicationWindow Window;
        static void Main(string[] args)
        {
            GameLoop = new GameLoop();
            //create thread here for all other logic
            Console.WriteLine("Hello, World!");
            Window = new ApplicationWindow("Hello");
            Window.Run();
            Window.Dispose();
        }
    }
}
