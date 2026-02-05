using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;
using OpenGLAbstraction.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.Nodes
{
    public interface ITextUINode : IUINode
    {
        string Content { get; set; }
        bool InLine { get; set; }
        float LineHeight { get; set; }
        int FontSize { get; }

        void LoadUniforms(ILetterObjectInstanceRenderNode letter);
        ILetterObjectInstanceRenderNode CreateLetter(IRenderNode renderNode, ITextUINode text, char character, Transform2D transform, int size);
    }
}
