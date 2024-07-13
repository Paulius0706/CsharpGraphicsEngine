
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenGLAbstraction.Core.Objects;
using CsharpGameReforged.Render.UI.Objects;
using CsharpGameReforged.Render.UI;
namespace CsharpGameReforged.Logic
{
    public class GameLoop : IDisposable
    {
        private bool _disposed = false;
        private bool firstFrame = true;
        public Thread LoopThread { get; private set; }
        public Dictionary<string, UITextBox> lines = new Dictionary<string, UITextBox>();
        public GameLoop() 
        {
            LoopThread = new Thread(new ThreadStart(Loop));
            LoopThread.Start();
        }
        private void Loop()
        {
            while (!_disposed)
            {
                if (Program.Window != null && Program.Window.Closed)
                {
                    break;
                }
                if (firstFrame)
                {
                    while (Program.Window == null || !Program.Window.Loaded)
                    {
                        Thread.Sleep(100);
                    }
                    Load();
                    firstFrame = false;
                }
                Update();
            }
        }
        public virtual void Load()
        {
            //!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~
            //lines["3"] = new UITextBox("abcdefghijklmnopqrstuvwxyz", new Vector2(50, 150), 50);
            //lines["2"] = new UITextBox("abcdefghijklmnopqrstuvw", new Vector2(50, 100), 50);
            //lines["1"] = new UITextBox("Hello World!", new Vector2(50, 50), 50);
            lines["3"] = new UITextBox("abcdefghijklmnopqrstuvwxyz", new Transform2D(Program.Window, new Vector2(50,150)), 50);
            lines["2"] = new UITextBox("abcdefghijklmnopqrstuvw", new Transform2D(Program.Window, new Vector2(50, 100)), 50);
            lines["1"] = new UITextBox("Hello World!", new Transform2D(Program.Window, new Vector2(50, 50)), 50);


        }
        public virtual void Update()
        {

            Thread.Sleep(5000);
        }


        public void Dispose()
        {
            _disposed = true;
        }
    }
}
