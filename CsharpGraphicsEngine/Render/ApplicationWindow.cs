using CsharpGameReforged.Render.UI;
using OpenGLAbstraction.Core;
using OpenGLAbstraction.Core.Nodes.Natives;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Render
{
    public class ApplicationWindow : WindowNode
    {
        public UIShaderNode UIShaderNode { get; set; }
        public TextureNode<UIAtributes,UIUniforms> MainTextureNode { get; set; }
        public ApplicationWindow(string title) : base(title)
        {

        }

        protected override void Load()
        {
            UIShaderNode = new UIShaderNode(this);
            //Nodes.Add(GeneratedId, UIShaderNode);
        }
    }
}
