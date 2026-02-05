using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGameReforged.Addons
{
    public static class Extensions
    {
        public static float ToRadians(this float x)
        {
            return x * 0.0174532925f;
        }
        public static double ToRadians(this double x)
        {
            return x * 0.0174532925;
        }
        public static float ToDegrees(this float x)
        {
            return x / 0.0174532925f;
        }
        public static double ToDegrees(this double x)
        {
            return x / 0.0174532925;
        }

        public static Color4 Add(this Color4 color1, Color4 color2)
        {
            return new Color4(color1.R + color2.R, color1.G + color2.G, color1.B + color2.B, color1.A + color2.A);
        }
        public static Color4 Sub(this Color4 color1, Color4 color2)
        {
            return new Color4(color1.R - color2.R, color1.G - color2.G, color1.B - color2.B, color1.A - color2.A);
        }
    }
}
