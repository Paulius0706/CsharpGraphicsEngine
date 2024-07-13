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
        public PositionRelativeType PositionRelativeType { get; set; }


        private Vector2 _relativePixelPosition = Vector2.Zero;
        public Vector2 RelativePixelPosition 
        {
            get 
            {
                return _relativePixelPosition;
            } 
            set 
            {
                _relativePixelPosition = value;
                UpdateWindowSize();
                UpdateRelativeWindowPosition();
                UpdatePixelPostion();
                UpdateWindowPosition();
            } 
        }
        
        
        private Vector2 _relativeWindowPosition;
        public Vector2 RelativeWindowPosition
        {
            get
            {
                return _relativeWindowPosition;
            }
            set
            {
                RelativePixelPosition = value * _windowNode.Size / 2f;
            }
        }
        private void UpdateRelativeWindowPosition()
        {
            _relativeWindowPosition = RelativePixelPosition / _windowNode.Size * 2f;
        }
        
        
        private Vector2 _pixelPositon;
        public Vector2 PixelPostion
        {
            get
            {
                return _pixelPositon;
            }
        }
        private void UpdatePixelPostion()
        {
            switch (PositionRelativeType)
            {
                case PositionRelativeType.TopLeft:   _pixelPositon = new Vector2(                     0, _windowNode.Size.Y    ) + new Vector2( RelativePixelPosition.X, -RelativePixelPosition.Y) - new Vector2(              0, PixelSize.Y     ); return;
                case PositionRelativeType.Top:       _pixelPositon = new Vector2(_windowNode.Size.X / 2, _windowNode.Size.Y    ) + new Vector2( RelativePixelPosition.X, -RelativePixelPosition.Y) - new Vector2(PixelSize.X / 2, PixelSize.Y     ); return;
                case PositionRelativeType.TopRight:  _pixelPositon = new Vector2(_windowNode.Size.X    , _windowNode.Size.Y    ) + new Vector2(-RelativePixelPosition.X, -RelativePixelPosition.Y) - new Vector2(PixelSize.X    , PixelSize.Y     ); return; 
                case PositionRelativeType.Left:      _pixelPositon = new Vector2(                     0, _windowNode.Size.Y / 2) + RelativePixelPosition                                           - new Vector2(              0, PixelSize.Y / 2f); return;
                case PositionRelativeType.Center:    _pixelPositon = new Vector2(_windowNode.Size.X / 2, _windowNode.Size.Y / 2) + RelativePixelPosition                                           - new Vector2(PixelSize.X / 2, PixelSize.Y / 2f); return;
                case PositionRelativeType.Right :    _pixelPositon = new Vector2(_windowNode.Size.X    , _windowNode.Size.Y / 2) + new Vector2(-RelativePixelPosition.X,  RelativePixelPosition.Y) - new Vector2(PixelSize.X    , PixelSize.Y / 2f); return;
                case PositionRelativeType.DownLeft:  _pixelPositon = new Vector2(                     0,                      0) + RelativePixelPosition                                           - new Vector2(              0,                0); return;
                case PositionRelativeType.Down:      _pixelPositon = new Vector2(_windowNode.Size.X / 2,                      0) + RelativePixelPosition                                           - new Vector2(PixelSize.X / 2,                0); return;
                case PositionRelativeType.DownRight: _pixelPositon = new Vector2(_windowNode.Size.X    ,                      0) + new Vector2(-RelativePixelPosition.X,  RelativePixelPosition.Y) - new Vector2(PixelSize.X    ,                0); return;
            }
        }

        
        private Vector2 _windowPosition;
        public Vector2 WindowPosition
        {
            get
            {
                return _windowPosition;
            }
        }
        private void UpdateWindowPosition()
        {
            _windowPosition = (PixelPostion / _windowNode.Size - Vector2.One * 0.5f) * 2f;
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
                UpdateWindowSize();
            } 
        }
        
        
        private Vector2 _windowSize;
        public Vector2 WindowSize
        {
            get
            {
                return _windowSize;
            }
            set
            {
                PixelSize = value * _windowNode.Size / 2f;
            }
        }
        private void UpdateWindowSize()
        {
            _windowSize = PixelSize / _windowNode.Size * 2f; 
        }
        public void OnResize()
        {
            UpdateWindowSize();
            
            UpdateRelativeWindowPosition();

            UpdatePixelPostion();
            UpdateWindowPosition();
        }




        public Transform2D(WindowNode windowNode, Vector2 pixelPosition, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft) 
            : this(windowNode, pixelPosition, Vector2.Zero, positionRelativeType) { }
        public Transform2D(WindowNode windowNode, Vector2 pixelPosition, Vector2 pixelSize, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft) 
        {
            _windowNode = windowNode;
            PositionRelativeType = positionRelativeType;
            PixelSize = pixelSize;
            RelativePixelPosition = pixelPosition;
        }
    }
}
