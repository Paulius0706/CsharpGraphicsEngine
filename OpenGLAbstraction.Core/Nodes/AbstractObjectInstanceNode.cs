using OpenGLAbstraction.Core.Components;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenGLAbstraction.Core.Nodes
{
    public class AbstractObjectInstanceNode<Atributes, Uniforms> : RenderNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        public AbstractObjectInstanceNode(RenderNode<Atributes, Uniforms> parent) : base(parent,false, false)
        {

        }
        protected sealed override void NodeLoadCheck()
        {
            base.NodeLoadCheck();
            if (Layout == null) { throw new Exception("Layout is not loaded for node"); }
            if (Shader == null) { throw new Exception("Shader is not loaded for node"); }
        }
        public sealed override void Render(FrameEventArgs args)
        {
            lock (lockObject)
            {
                try
                {
                    LoadUniforms();
                    Layout.Render();
                }
                catch
                {

                }
            }
        }
        public virtual void LoadUniforms()
        {
            throw new NotImplementedException();
        }
        protected override void InternalDispose()
        {
            
        }
    }
}
