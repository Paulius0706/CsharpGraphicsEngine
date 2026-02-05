using CsharpGameReforged.Render.UI;
using OpenGLAbstraction.Core;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;
using OpenGLAbstraction.Core.Nodes.Helpers;
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
        public ITextureRenderNode MainTextureRenderNode { get; set; }
        public IBoxLayoutRenderNode BoxLayoutRenderNode { get; private set; }

        public IShaderRenderNode LetterShaderRenderNode { get; private set; }
        public ILetterTextureRenderNode LetterTextureRenderNode { get; private set; }
        public ILetterLayoutRenderNode LetterLayoutRenderNode { get; private set; }

        public ApplicationWindow(string title) : base(title)
        {

        }

        protected override void Load()
        {
            LetterShaderRenderNode = new ShaderRenderNode(this, "Render/UI/UI.vert", "Render/UI/UI.frag");
            BoxLayoutRenderNode = new BoxLayoutRenderNode<UIAtributes>(LetterShaderRenderNode, (pos, uv) => new UIAtributes(pos, uv));
            LetterTextureRenderNode = new LetterTextureRenderNode(LetterShaderRenderNode, "Render/UI/Textures/output-seomagnifier(2).png");
            LetterLayoutRenderNode = new LetterLayoutRenderNode<UIAtributes>(LetterTextureRenderNode, (pos, uv) => new UIAtributes(pos, uv));
            //Nodes.Add(GeneratedId, UIShaderNode);
        }
        public override void ResizeEvent()
        {
            foreach (var item in Program.GameLoop.UIElements.Values.ToList())
            {
                item.Update();
            }
            //foreach (var item in Program.GameLoop.boxes.Values.ToList())
            //{
            //    item.Update();
            //}
            //foreach(var item in Program.GameLoop.labels.Values.ToList())
            //{
            //    item.Update();
            //}
            //foreach (var item in Program.GameLoop.borderBoxes.Values.ToList())
            //{
            //    item.Update();
            //}
        }
    }
}
