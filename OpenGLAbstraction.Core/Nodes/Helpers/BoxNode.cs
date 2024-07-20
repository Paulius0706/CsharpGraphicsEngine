using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Objects.UI.Nodes;
using OpenGLAbstraction.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Helpers
{
    public class BoxNode<Atributes, Uniforms> : AbstractObjectInstanceNode<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        public readonly Transform2D Transform;
        private Box<BoxNode<Atributes, Uniforms>, Atributes, Uniforms> _box;
        public BoxNode(RenderNode<Atributes, Uniforms> parent, Box<BoxNode<Atributes, Uniforms>, Atributes, Uniforms> box, Transform2D transform) : base(parent)
        {
            Transform = transform;
            _box = box;
        }
        public override void LoadUniforms()
        {
            _box?.LoadUniform(this);
        }
    }
}
