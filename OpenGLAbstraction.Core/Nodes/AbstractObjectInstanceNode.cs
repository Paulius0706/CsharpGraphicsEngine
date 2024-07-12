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
        protected Uniforms uniforms;
        public AbstractObjectInstanceNode(RenderNode<Atributes, Uniforms> parent, Uniforms uniforms) : base(parent,false, false)
        {
            this.uniforms = uniforms;
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
        public bool ModifyUniform(Action<Uniforms> uniformChange)
        {
            lock (lockObject) 
            {
                try
                {
                    uniformChange.Invoke(uniforms);
                }
                catch
                {
                    return false;
                }
            }
            return true;
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
