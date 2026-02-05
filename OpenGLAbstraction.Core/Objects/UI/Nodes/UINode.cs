using OpenGLAbstraction.Core.Definitions.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Objects.UI.Nodes
{

    public abstract class UINode : IUINode
    {
        private int counter = 0;
        public string GeneratedId => "UIGEN-" + counter++;
        public static float MinDepth { get; set; } = -100f;
        public static float MaxDepth { get; set; } = 100f;
        public static float MinDepthLayer { get; set; } = 0.0f;
        public static float MaxDepthLayer { get; set; } = 1f;

        public IUINode Parent { get; private set; } = null;

        public Dictionary<string, IUINode> _nodes { get; private set; } = new Dictionary<string, IUINode>();
        private float _depth { get; set; } = 0.5f;

        public float Depth
        {
            get
            {
                return (_depth - MinDepthLayer) / (MaxDepthLayer - MinDepthLayer) * (MaxDepth - MinDepth) - MinDepth;
            }
            set
            {
                _depth = (value + MinDepth) / (MaxDepth - MinDepth) * (MaxDepthLayer - MinDepthLayer) + MinDepthLayer;
            }
        }
        public float WindowdDepth => _depth;
        private Transform2D _transform;
        public Transform2D Transform => _transform;
        public UINode(IUINode parent, Transform2D transform)
        {
            if (parent != null) transform.Parent = parent.Transform;
            this._transform = transform;
            this.Parent = parent;
            if (Parent != null)
            {
                Parent._nodes.Add(Parent.GeneratedId, this);
                this.Depth = Parent.Depth - 1;
            }
        }
        public void Update()
        {
            InternalUpdate();
            var nodes = this._nodes.Values;
            foreach (var node in nodes)
            {
                node.Update();
            }
        }

        protected abstract void InternalUpdate();
        //public void OnResize()
        //{
        //    InternalOnResize();
        //    var nodes = this.nodes.Values.ToList();
        //    foreach(var node in nodes)
        //    {
        //        node.OnResize();
        //    }
        //}
        //protected abstract void InternalOnResize();
    }
}
