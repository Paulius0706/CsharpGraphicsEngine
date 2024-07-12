using OpenGLAbstraction.Core;
using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using CsharpGameReforged.Render.UI.Textures;

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
        public Vector2 WindowSize;
        public Vector2 TextureSize;
        public Vector4 PositionSize;
        public Vector4 UVPositionSize;
    }
    public class UIShaderNode : AbstractShaderNode<UIAtributes, UIUniforms>
    {
        public BasicTextureNode BasicTextureNode { get; set; }
        public UIShaderNode(WindowNode parentWindow) : base(parentWindow) 
        {
            shader = new Shader<UIAtributes, UIUniforms>("Render/UI/UI.vert", "Render/UI/UI.frag");
            BasicTextureNode = new BasicTextureNode(this);
        }
        protected override void LoadStaticUniforms()
        {
            shader.SetUniform(nameof(StaticUniforms.WindowSize), new Vector2(Window.Size.X, Window.Size.Y));
            //shader.SetUniform(nameof(StaticUniforms.TextureSize), new Vector2(1, 1));
        }
    }
}
