using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Components
{
    public class Uniform<Atributes, Uniforms> where Atributes : struct where Uniforms : struct
    {
        private readonly Shader<Atributes, Uniforms> shader;
        public bool render { get; set; } = false;


        public Uniform(Shader<Atributes, Uniforms> shader, bool render = false)
        {
            this.shader = shader;
            this.render = render;

        }

        public virtual void Load()
        {

        }
    }
}
