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
        private Transform2D _parent;
        public Transform2D Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if(this._parent != null)
                {
                    this._parent._children.Remove(this);
                }

                this._parent = value;
                if (this._parent != null) 
                {
                    this._parent._children.Add(value);
                }
                Update();
            }
        }

        private List<Transform2D> _children = new List<Transform2D>();

        private WindowNode _windowNode;
        public PositionRelativeType _positionRelativeType;
        public PositionRelativeType PositionRelativeType { get { return _positionRelativeType; } set { _positionRelativeType = value; Update(); } }


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
                Update();
                
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
        
        
        private Vector2 _pixelPositon;
        public Vector2 PixelPostion => _pixelPositon;

        
        private Vector2 _windowPosition;
        public Vector2 WindowPosition => _windowPosition;

        
        private void UpdatePosition()
        {
            _relativeWindowPosition = _relativePixelPosition / _windowNode.Size * 2f;
            _pixelPositon = Vector2.Zero;
            var parentPixelPosition = Parent == null ? Vector2.Zero : Parent.PixelPostion;
            var parentPixelSize = Parent == null ? Vector2.Zero : Parent.PixelSize;
            switch (PositionRelativeType)
            {
                case PositionRelativeType.TopLeft:   _pixelPositon = parentPixelPosition + parentPixelSize * new Vector2(  0f,   1f) + new Vector2(                     0, _windowNode.Size.Y    ) + new Vector2( RelativePixelPosition.X, -RelativePixelPosition.Y) - new Vector2(              0, PixelSize.Y     ); break;
                case PositionRelativeType.Top:       _pixelPositon = parentPixelPosition + parentPixelSize * new Vector2(0.5f,   1f) + new Vector2(_windowNode.Size.X / 2, _windowNode.Size.Y    ) + new Vector2( RelativePixelPosition.X, -RelativePixelPosition.Y) - new Vector2(PixelSize.X / 2, PixelSize.Y     ); break;
                case PositionRelativeType.TopRight:  _pixelPositon = parentPixelPosition + parentPixelSize * new Vector2(  1f,   1f) + new Vector2(_windowNode.Size.X    , _windowNode.Size.Y    ) + new Vector2(-RelativePixelPosition.X, -RelativePixelPosition.Y) - new Vector2(PixelSize.X    , PixelSize.Y     ); break; 
                case PositionRelativeType.Left:      _pixelPositon = parentPixelPosition + parentPixelSize * new Vector2(  0f, 0.5f) + new Vector2(                     0, _windowNode.Size.Y / 2) + RelativePixelPosition                                           - new Vector2(              0, PixelSize.Y / 2f); break;
                case PositionRelativeType.Center:    _pixelPositon = parentPixelPosition + parentPixelSize * new Vector2(0.5f, 0.5f) + new Vector2(_windowNode.Size.X / 2, _windowNode.Size.Y / 2) + RelativePixelPosition                                           - new Vector2(PixelSize.X / 2, PixelSize.Y / 2f); break;
                case PositionRelativeType.Right :    _pixelPositon = parentPixelPosition + parentPixelSize * new Vector2(  1f, 0.5f) + new Vector2(_windowNode.Size.X    , _windowNode.Size.Y / 2) + new Vector2(-RelativePixelPosition.X,  RelativePixelPosition.Y) - new Vector2(PixelSize.X    , PixelSize.Y / 2f); break;
                case PositionRelativeType.DownLeft:  _pixelPositon = parentPixelPosition + parentPixelSize * new Vector2(  0f,   0f) + new Vector2(                     0,                      0) + RelativePixelPosition                                           - new Vector2(              0,                0); break;
                case PositionRelativeType.Down:      _pixelPositon = parentPixelPosition + parentPixelSize * new Vector2(0.5f,   0f) + new Vector2(_windowNode.Size.X / 2,                      0) + RelativePixelPosition                                           - new Vector2(PixelSize.X / 2,                0); break;
                case PositionRelativeType.DownRight: _pixelPositon = parentPixelPosition + parentPixelSize * new Vector2(  1f,   0f) + new Vector2(_windowNode.Size.X    ,                      0) + new Vector2(-RelativePixelPosition.X,  RelativePixelPosition.Y) - new Vector2(PixelSize.X    ,                0); break;
            }
            _windowPosition = (_pixelPositon / _windowNode.Size - Vector2.One * 0.5f) * 2f;
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
                Update();
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
        private void UpdateSize()
        {
            _windowSize = _pixelSize / _windowNode.Size * 2f;
        }
        
        public void Update()
        {
            UpdatePosition();
            UpdateSize();
            foreach(var child in _children)
            {
                child.Update();
            }
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
