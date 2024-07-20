using OpenGLAbstraction.Core;
using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenGLAbstraction.Core.Nodes.Natives;
using OpenGLAbstraction.Core.Nodes.Helpers;

namespace CsharpGameReforged.Render.UI
{
    public struct UIAtributes
    {
        public readonly Vector3 aPosition;
        public readonly Vector2 aUV;
        public UIAtributes(Vector3 aPosition, Vector2 aUV) { this.aPosition = aPosition; this.aUV = aUV; }
    }
    public struct UIUniforms
    {
        public Vector4 PositionSize;
        public Vector4 UVPositionSize;
        public Vector4 Color;
        public float Depth;
    }
    public class UIShaderNode : AbstractShaderNode<UIAtributes, UIUniforms>
    {
        public RenderNode<UIAtributes, UIUniforms> LettersRenderNode { get; private set; }
        public RenderNode<UIAtributes, UIUniforms> BoxRenderNode { get; private set; }
        public UIShaderNode(WindowNode parentWindow) : base(parentWindow) 
        {
            shader = new Shader<UIAtributes, UIUniforms>("Render/UI/UI.vert", "Render/UI/UI.frag");
            var letterTexturenode = new LetterTextureNode<UIAtributes, UIUniforms>(this, "Render/UI/Textures/output-seomagnifier(2).png");
            LettersRenderNode = new LetterLayoutNode<UIAtributes, UIUniforms>(letterTexturenode, (pos, uv) => new UIAtributes(pos, uv));
            BoxRenderNode = new BoxLayoutNode<UIAtributes, UIUniforms>(this, (pos, uv) => new UIAtributes(pos, uv));
        }
        protected override void LoadStaticUniforms()
        {
            //shader.SetUniform(nameof(StaticUniforms.WindowSize), new Vector2(Window.Size.X, Window.Size.Y));
        }
    }
}
