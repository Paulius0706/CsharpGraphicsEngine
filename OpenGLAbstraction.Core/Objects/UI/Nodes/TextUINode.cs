using OpenGLAbstraction.Core.Definitions.Nodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes;
using OpenGLAbstraction.Core.Definitions.RenderNodes.Helpers;
using OpenGLAbstraction.Core.Nodes;
using OpenGLAbstraction.Core.Objects;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OpenGLAbstraction.Core.Objects.UI.Nodes
{

    public class TextOptions
    {
        public string Text { get; set; } = "";
        public Transform2D Transform { get; set; }
        public int Size { get; set; } = 20;
        public bool InLine { get; set; } = false;
        public float LineHeight { get; set; } = 1.2f;
    }
    

    public abstract class TextUINode : UINode, ITextUINode
    {
        private string _content = "";
        public string Content 
        {
            get => _content; 
            set 
            {
                _content = value;
                UpdateLettersInformation();
            } 
        }
        public int FontSize { get; protected set; }
        public bool InLine { get { return _inLine; } set { _inLine = value; Update(); } }
        private bool _inLine = false;

        private float _lineHeight;
        public float LineHeight { get { return _lineHeight; } set { _lineHeight = value; Update(); } }

        protected abstract IRenderNode LettersRenderNode { get; }
        private List<ILetterObjectInstanceRenderNode> letterNodes = new List<ILetterObjectInstanceRenderNode>();

        public TextUINode(TextOptions textBoxOptions) : this(null, textBoxOptions) { }
        public TextUINode(UINode parent, TextOptions textBoxOptions) : base(parent, textBoxOptions.Transform)
        {
            FontSize = textBoxOptions.Size;
            _inLine = textBoxOptions.InLine;
            _lineHeight = textBoxOptions.LineHeight;
            Content = textBoxOptions.Text;
            //UpdateLettersInformation();
        }
        private void UpdateLettersInformation()
        {
            UpdateLettersNodes();
            UpdateLettersPositions();
        }
        private void UpdateLettersNodes()
        {
            for (int i = 0; i < Content.Length; i++)
            {
                char character = Content[i];
                if (letterNodes.Count > i)
                {
                    if (letterNodes[i].Character == character) continue;
                    letterNodes[i].Character = character;
                    continue;
                }
            }
            if(Content.Length > letterNodes.Count)
            {
                LettersRenderNode.NodeThreadAction(() => 
                {
                    var nodesCount = letterNodes.Count;
                    for(int i= nodesCount; i< Content.Length; i++)
                    {
                        char character = Content[i];
                        Transform2D transform = new Transform2D(LettersRenderNode.Window, Vector2.Zero, PositionRelativeType.TopLeft);
                        transform.Parent = this.Transform;
                        letterNodes.Add(CreateLetter(LettersRenderNode, this, character, transform, FontSize));
                    }
                });
            }
            if(Content.Length < letterNodes.Count)
            {
                LettersRenderNode.NodeThreadAction(() =>
                {
                    var deletedNodes = letterNodes.Skip(Content.Length).ToList();
                    foreach(var node in deletedNodes)
                    {
                        node.Dispose();
                    }
                    letterNodes = letterNodes.Take(Content.Length).ToList();
                });
            }
        }
        private void UpdateLettersPositions()
        {
            float Xoffset = 0;
            float Yoffset = 0;
            foreach (var node in letterNodes.ToList())
            {
                var characterWidth = node.Character == ' ' ? node.FontTexture.SpaceWidth : node.Transform.SizeInPixels.X;
                //var Xoffset1 = node.Character == ' ' ? Xoffset + node.FontTexture.SpaceWidth :Xoffset + node.Transform.SizeInPixels.X;
                if (Transform.SizeInPixels.X < Xoffset + characterWidth && !InLine)
                {
                    Xoffset = 0;
                    Yoffset += (float)FontSize * LineHeight;
                }
                Vector2 pixelPosition = Vector2.UnitX * Xoffset + Vector2.UnitY * Yoffset;
                node.Transform.RelativePositionInPixels = pixelPosition;
                //node.Transform.Update();
                Xoffset = Xoffset + characterWidth;
            }
        }
        protected sealed override void InternalUpdate()
        {
            Transform.Update();
            UpdateLettersPositions();
        }
        public abstract void LoadUniforms(ILetterObjectInstanceRenderNode letter);
        public abstract ILetterObjectInstanceRenderNode CreateLetter(IRenderNode renderNode, ITextUINode text, char character, Transform2D transform, int size);
    }
}
