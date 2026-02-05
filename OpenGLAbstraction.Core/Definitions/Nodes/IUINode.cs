using OpenGLAbstraction.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.Nodes
{
    public interface IUINode
    {
        float Depth { get; set; }
        string GeneratedId { get; }
        float WindowdDepth { get; }
        public Transform2D Transform { get; }
        void Update();
        public IUINode Parent { get; }
        public Dictionary<string, IUINode> _nodes { get; }
    }
}
