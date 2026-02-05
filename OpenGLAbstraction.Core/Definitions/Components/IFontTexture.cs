using OpenGLAbstraction.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Definitions.Components
{
    public interface IFontTexture : ITexture
    {
        public IReadOnlyDictionary<char, TransformUV> LettersUVs { get; }
        public int SpaceWidth { get; }
        public int SpaceHeight { get; }
    }
}
