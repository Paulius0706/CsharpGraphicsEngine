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

        public Uniform(Shader<Atributes, Uniforms> shader)
        {
            this.shader = shader;
        }
    }
}
