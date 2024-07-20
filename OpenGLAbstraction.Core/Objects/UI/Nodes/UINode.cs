using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Objects.UI.Nodes
{
    public abstract class UINode
    {
        public static float MinDepth {  get; set; } = -100f;
        public static float MaxDepth { get; set; } = 100f;
        public static float MinDepthLayer { get; set; } = 0.9f;
        public static float MaxDepthLayer { get; set; } = 1f;


        protected Dictionary<string,UINode> nodes = new Dictionary<string, UINode>();
        protected readonly UINode Parent = null;
        private float _depth { get; set; }
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
        public readonly Transform2D Transform;
        public UINode(UINode parent, Transform2D transform) 
        {
            this.Transform = transform;
            this.Parent = parent;
        }
        public void Update()
        {
            InternalUpdate();
            var nodes = this.nodes.Values;
            foreach (var node in nodes)
            {
                node.Update();
            }
        }

        protected abstract void InternalUpdate();
        public void OnResize()
        {
            InternalOnResize();
            var nodes = this.nodes.Values.ToList();
            foreach(var node in nodes)
            {
                node.OnResize();
            }
        }
        protected abstract void InternalOnResize();
    }
}
