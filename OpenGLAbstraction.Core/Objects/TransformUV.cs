using OpenGLAbstraction.Core.Components;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Objects
{
    public class TransformUV
    {
        private readonly Texture Texture;

        public readonly int UpperPixel;
        public readonly int LowerPixel;
        public readonly int LeftPixel;
        public readonly int RightPixel;
        public readonly int Height;
        public readonly int Width;
        public readonly Vector2 Position;
        public readonly Vector2 Size;

        public readonly float RealUpperPixel;
        public readonly float RealLowerPixel;
        public readonly float RealLeftPixel;
        public readonly float RealRightPixel;

        public readonly float RealHeight;
        public readonly float RealWidth;
        public readonly Vector2 RealPosition;
        public readonly Vector2 RealSize;


        public TransformUV(Texture texture, int upperPixel, int lowerPixel, int leftPixel, int rightPixel)
        {
            this.Texture = texture;
            UpperPixel = upperPixel;
            LowerPixel = lowerPixel;
            LeftPixel = leftPixel;
            RightPixel = rightPixel;

            Height = UpperPixel - LowerPixel + 1;
            Width = RightPixel - LeftPixel + 1;

            RealUpperPixel = (float)UpperPixel / (float)Texture.Height;
            RealLowerPixel = (float)LowerPixel / (float)Texture.Height;
            RealLeftPixel = (float)LeftPixel / (float)Texture.Width;
            RealRightPixel = (float)RightPixel / (float)Texture.Width;

            RealHeight = (float)Height / (float)Texture.Height;
            RealWidth = (float)Width / (float)Texture.Width;

            Position = new Vector2(LeftPixel, LowerPixel);
            Size = new Vector2(Width, Height);

            RealPosition = new Vector2(RealLeftPixel, RealLowerPixel);
            RealSize = new Vector2(RealWidth, RealHeight);
        }
    }
}
