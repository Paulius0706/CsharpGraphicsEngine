using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Render.UI
{
    public struct UIAtributes
    {
        public readonly Vector3 aPosition;
        public readonly Vector2 aUV;
        public UIAtributes(Vector3 aPosition, Vector2 aUV) { this.aPosition = aPosition; this.aUV = aUV; }
    }
    public struct UIUniforms
    {
        public Vector4 UVPositionSize;
        public Matrix4 Matrix;
        public Vector4 Color;
        public float Depth;
    }
}
