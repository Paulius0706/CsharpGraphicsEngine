using OpenGLAbstraction.Core.Components;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes
{
    public class AbstractLayoutNode<Attributes, Uniforms> : RenderNode<Attributes, Uniforms> where Attributes : struct where Uniforms : struct
    {
        protected Layout<Attributes, Uniforms> layout;
        public override Layout<Attributes, Uniforms> Layout => layout;
        public AbstractLayoutNode(RenderNode<Attributes, Uniforms> parent) : base(parent) {}
        protected sealed override void NodeLoadCheck()
        {
            base.NodeLoadCheck();
            if (Layout == null) { throw new Exception("Layout is not loaded for node"); }
            if (Shader == null) { throw new Exception("Shader is not loaded for node"); }
        }
        public sealed override void Render(FrameEventArgs args)
        {
            layout.Use();
            base.Render(args);
            layout.UnUse();
        }
        protected override void InternalDispose()
        {
            layout.Dispose();
        }
    }
}
