using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.Components
{
    public interface IComponent : IDisposable
    {
        public void Use();
        public void UnUse();
    }
}
