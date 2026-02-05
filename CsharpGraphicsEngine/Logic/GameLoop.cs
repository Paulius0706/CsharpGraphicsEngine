
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenGLAbstraction.Core.Objects;
using CsharpGameReforged.Render.UI;
using OpenGLAbstraction.Core.Objects.UI.Nodes;
using CsharpGameReforged.Addons;
using OpenGLAbstraction.Core.Objects.UI.Nodes.Compound;
using OpenGLAbstraction.Core.Definitions.Nodes;
namespace CsharpGameReforged.Logic
{
    public class GameLoop : IDisposable
    {
        private bool _disposed = false;
        private bool firstFrame = true;
        public Thread LoopThread { get; private set; }
        public Dictionary<string, IUINode> UIElements { get; private set; } = new Dictionary<string, IUINode>();
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
                    try
                    {
                        Load();
                    }
                    catch
                    {
                        throw new NotImplementedException();
                    }
                    firstFrame = false;
                }
                try
                {
                    Update();
                }
                catch
                {

                }
            }
        }
        public virtual void Load()
        {
            //!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~
            //lines["3"] = new UITextBox("abcdefghijklmnopqrstuvwxyz", new Vector2(50, 150), 50);
            //lines["2"] = new UITextBox("abcdefghijklmnopqrstuvw", new Vector2(50, 100), 50);
            //lines["1"] = new UITextBox("Hello World!", new Vector2(50, 50), 50);
            //lines["3"] = new UITextBox(null,"abcdefghijklmnopqrstuvwxyz", new Transform2D(Program.Window, new Vector2(50,150)), 50);
            //lines["2"] = new UITextBox(null,"abcdefghijklmnopqrstuvw", new Transform2D(Program.Window, new Vector2(50, 100)), 50);

            //lines["1"] = new UIText(null, 
            //    new TextOptions(
            //        "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. ", 
            //        new Transform2D(Program.Window, new Vector2(50, 200), new Vector2(600, 200), Extensions.ToRadians(45f), PositionRelativeType.TopLeft), 
            //        50));
            //
            //boxes["1"] = new UIBox(
            //    null,
            //    new BoxOptions(
            //        new Transform2D(
            //            Program.Window, 
            //            new Vector2(50, 50), 
            //            new Vector2(100, 100), 
            //            PositionRelativeType.TopRight)
            //        , Color4.Aqua));
            //boxes["1"].Depth += 2;
            UIElements["UITextLabel"] = new UITextLabel(null, new Transform2D(Program.Window, new Vector2(50, 50), new Vector2(300, 300)), 
                new BoxOptions() 
                {
                    Color = Color4.Red//.Sub(new Color4(0, 0, 0, 0.5f))
                }, 
                new TextOptions() 
                {
                    Text = "SSsbdicbasbdci kasbdcbasda iasndcinasd",
                    Size = 20
                });
            UIElements["UIBorderedBox"] = new UIBorderedBox(null, new Transform2D(Program.Window, new Vector2(400, 50), new Vector2(200, 200)), new BorderedBoxOptions()
            {
                Color = Color4.Aqua,
                BorderColor = Color4.DarkGray
            });

        }
        public virtual void Update()
        {

            Thread.Sleep(100);
            ((UITextLabel)UIElements["UITextLabel"]).Content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }


        public void Dispose()
        {
            _disposed = true;
        }
    }
}
