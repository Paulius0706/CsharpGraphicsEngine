using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLAbstraction.Core.Objects
{
    public enum PositionType
    {
        Pixel,
        Window,
    }
    public enum PositionRelativeType
    {
        TopLeft,
        TopRight,
        DownLeft,
        DownRight,
        Left,
        Center,
        Right,
        Top,
        Down,
    }
    public class Transform2D
    {

        private WindowNode _windowNode;
        public PositionRelativeType PositionRelativeType { get; set; } = PositionRelativeType.TopLeft;
        private Vector2 _pixelPosition = Vector2.Zero;
        public Vector2 RelativePixelPosition 
        {
            get 
            {
                return _pixelPosition;
            } 
            set 
            {
                _pixelPosition = value;
            } 
        }
        public Vector2 RelativeWindowPosition
        {
            get
            {
                return RelativePixelPosition / _windowNode.Size * 2f;
            }
            set
            {
                RelativePixelPosition = value * _windowNode.Size / 2f;
            }
        }
        public Vector2 PixelPostion
        {
            get
            {
                switch (PositionRelativeType)
                {
                    case PositionRelativeType.TopLeft:   return new Vector2(                     0, _windowNode.Size.Y    ) + new Vector2( RelativePixelPosition.X, -RelativePixelPosition.Y) - new Vector2(              0, PixelSize.Y     );
                    case PositionRelativeType.Top:       return new Vector2(_windowNode.Size.X / 2, _windowNode.Size.Y    ) + new Vector2( RelativePixelPosition.X, -RelativePixelPosition.Y) - new Vector2(PixelSize.X / 2, PixelSize.Y     );
                    case PositionRelativeType.TopRight:  return new Vector2(_windowNode.Size.X    , _windowNode.Size.Y    ) + new Vector2(-RelativePixelPosition.X, -RelativePixelPosition.Y) - new Vector2(PixelSize.X    , PixelSize.Y     ); 
                    case PositionRelativeType.Left:      return new Vector2(                     0, _windowNode.Size.Y / 2) + RelativePixelPosition                                           - new Vector2(              0, PixelSize.Y / 2f);
                    case PositionRelativeType.Center:    return new Vector2(_windowNode.Size.X / 2, _windowNode.Size.Y / 2) + RelativePixelPosition                                           - new Vector2(PixelSize.X / 2, PixelSize.Y / 2f);
                    case PositionRelativeType.Right :    return new Vector2(_windowNode.Size.X    , _windowNode.Size.Y / 2) + new Vector2(-RelativePixelPosition.X,  RelativePixelPosition.Y) - new Vector2(PixelSize.X    , PixelSize.Y / 2f);
                    case PositionRelativeType.DownLeft:  return new Vector2(                     0,                      0) + RelativePixelPosition                                           - new Vector2(              0, 0);
                    case PositionRelativeType.Down:      return new Vector2(_windowNode.Size.X / 2,                      0) + RelativePixelPosition                                           - new Vector2(PixelSize.X / 2, 0);
                    case PositionRelativeType.DownRight: return new Vector2(_windowNode.Size.X    ,                      0) + new Vector2(-RelativePixelPosition.X,  RelativePixelPosition.Y) - new Vector2(PixelSize.X, 0);
                }
                return RelativePixelPosition;
            }
        }
        public Vector2 WindowPosition
        {
            get
            {
                return (PixelPostion / _windowNode.Size - Vector2.One * 0.5f) * 2f;
            }
        }

        private Vector2 _pixelSize = Vector2.Zero;
        public Vector2 PixelSize 
        {
            get 
            {
                return _pixelSize;
            } 
            set 
            {
                _pixelSize = value;
            } 
        }    
        public Vector2 WindowSize
        {
            get
            {
                return PixelSize / _windowNode.Size * 2f;
            }
            set
            {
                PixelSize = value * _windowNode.Size / 2f;
            }
        }




        public Transform2D(WindowNode windowNode, Vector2 pixelPosition, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft) 
            : this(windowNode, pixelPosition, Vector2.Zero, positionRelativeType) { }
        public Transform2D(WindowNode windowNode, Vector2 pixelPosition, Vector2 pixelSize, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft) 
        {
            _windowNode = windowNode;
            _pixelPosition = pixelPosition;
            _pixelSize = pixelSize;
        }
    }
}
