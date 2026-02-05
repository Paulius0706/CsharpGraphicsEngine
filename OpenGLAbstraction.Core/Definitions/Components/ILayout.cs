using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.Components
{
    public interface ILayout : IComponent
    {
        public void Render();
    }
}
