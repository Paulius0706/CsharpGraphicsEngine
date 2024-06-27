using OpenGLAbstraction.Core.Components;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes
{
    public class LayoutNode<Attributes, Uniforms> : RenderNode where Attributes : struct where Uniforms : struct
    {
        protected Shader<Attributes, Uniforms> shader;
        protected Layout<Attributes, Uniforms> layout;
        
        public LayoutNode(Shader<Attributes, Uniforms> shader) : base() 
        {
            this.shader = shader;
        }
        public sealed override void Render(FrameEventArgs args)
        {
            if (layout == null) { throw new Exception("Layout is not loaded for node"); }
            if (shader == null) { throw new Exception("Shader is not loaded for node"); }
            layout.Use();
            base.Render(args);
            layout.UnUse();
        }
    }
}
