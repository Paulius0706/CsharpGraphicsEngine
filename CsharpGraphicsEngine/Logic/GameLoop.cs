
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using CsharpGameReforged.Render.UI.Objects;
using CsharpGameReforged.Render.UI;

namespace CsharpGameReforged.Logic
{
    public class GameLoop : IDisposable
    {
        private bool _disposed = false;
        private bool firstFrame = true;
        public Thread LoopThread { get; private set; }
        public Dictionary<string, UITextLine> lines = new Dictionary<string, UITextLine>();
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
            lines["3"] = new UITextLine("abcdefghijklmnopqrstuvwxyz", new Vector2(50, 150), 50);
            lines["2"] = new UITextLine("abcdefghijklmnopqrstuvw", new Vector2(50, 100), 50);
            lines["1"] = new UITextLine("Hello World!", new Vector2(50, 50), 50);


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
