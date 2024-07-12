using OpenGLAbstraction.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Natives
{
    public class ObjectinstanceNode<Atributes, Uniforms> : AbstractObjectInstanceNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        private Action<ObjectinstanceNode<Atributes, Uniforms>> loadUniformsAction = null;
        public ObjectinstanceNode(RenderNode<Atributes, Uniforms> parent) : base(parent)
        {
        }
        public override void LoadUniforms()
        {
            if (loadUniformsAction != null)
            {
                loadUniformsAction.Invoke(this);
            }
        }
    }
}
