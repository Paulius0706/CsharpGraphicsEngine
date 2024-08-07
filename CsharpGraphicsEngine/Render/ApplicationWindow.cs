﻿using CsharpGameReforged.Render.UI;
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
        public UIShaderNode UIRender { get; set; }
        public TextureNode<UIAtributes,UIUniforms> MainTextureNode { get; set; }
        public ApplicationWindow(string title) : base(title)
        {

        }

        protected override void Load()
        {
            UIRender = new UIShaderNode(this);
            //Nodes.Add(GeneratedId, UIShaderNode);
        }
        public override void ResizeEvent()
        {
            foreach (var item in Program.GameLoop.lines.Values.ToList())
            {
                item.Update();
            }
            foreach (var item in Program.GameLoop.boxes.Values.ToList())
            {
                item.Update();
            }
        }
    }
}
