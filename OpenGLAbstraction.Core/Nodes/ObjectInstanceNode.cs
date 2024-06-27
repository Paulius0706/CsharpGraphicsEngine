using OpenGLAbstraction.Core.Components;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes
{
    public class ObjectInstanceNode<Atributes, Uniforms> : RenderNode where Atributes : struct where Uniforms : struct
    {
        private Uniforms uniforms;
        public Uniforms Uniform 
        {
            get 
            {
                return uniforms; 
            }
            set
            {
                lock (lockObject)
                {
                    try
                    {
                        uniforms = value;
                    }
                    catch
                    {

                    }
                }
            }
        }
        protected readonly Shader<Atributes, Uniforms> shader;
        protected readonly Layout<Atributes, Uniforms> layout;
        public ObjectInstanceNode(Shader<Atributes, Uniforms> shader, Layout<Atributes, Uniforms> layout, Uniforms uniforms)
        {
            this.shader = shader;
            this.uniforms = uniforms;
            this.layout = layout;
        }
        public sealed override void Render(FrameEventArgs args)
        {
            lock (lockObject)
            {
                try
                {
                    LoadUniforms();
                    layout.Render();
                }
                catch
                {

                }
            }
        }
        public virtual void LoadUniforms()
        {

        }
    }
}
