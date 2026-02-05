using OpenGLAbstraction.Core.Components;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Nodes.Natives
{
    public class ShaderRenderNode : AbstractShaderRenderNode, IShaderRenderNode
    {
        public ShaderRenderNode(WindowNode parent, string vertexShaderPath, string fragmentShaderPath) : base(parent)
        {
            _shader = new Shader(vertexShaderPath, fragmentShaderPath);
        }
        public ShaderRenderNode(IRenderNode parent, string vertexShaderPath, string fragmentShaderPath) : base(parent)
        {
            _shader = new Shader(vertexShaderPath, fragmentShaderPath);
        }

        protected override void InternalResize()
        {

        }
    }
}
