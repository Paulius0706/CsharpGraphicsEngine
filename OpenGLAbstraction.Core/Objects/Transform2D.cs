using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
        //public string GeneratedId => "GEN-" + counter++;


        private Transform2D _parent;
        public Transform2D Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (value.GetHashCode() == this.GetHashCode()) return;
                if (this._allChildren.Contains(value)) return;
                if(this._parent != null)
                {
                    this._parent._children.Remove(this);
                }

                this._parent = value;
                if (this._parent != null) 
                {
                    this._parent._children.Add(this);
                }
                Update();
            }
        }
        private List<Transform2D> _allChildren => new List<Transform2D>(_children).Union(_children.SelectMany(o => o._allChildren)).ToList();

        private List<Transform2D> _children = new List<Transform2D>();

        private WindowNode _windowNode;
        public PositionRelativeType _positionRelativeType;
        public PositionRelativeType PositionRelativeType { get { return _positionRelativeType; } set { _positionRelativeType = value; Update(); } }

        private Matrix4 _matrix;
        public Matrix4 Matrix => _matrix;

        private float _rotation;
        public float RotationInRadians{ get { return _rotation; } set { _rotation = value; Update(); } }

        private Vector2 _relativePositionInPixels = Vector2.Zero;
        public Vector2 RelativePositionInPixels { get { return _relativePositionInPixels; }  set { _relativePositionInPixels = value; Update(); } }
        
        
        private Vector2 _relativePositionInWindows;
        public Vector2 RelativePositionInWindows { get { return _relativePositionInWindows; } set { RelativePositionInPixels = value * _windowNode.Size / 2f; } }
        
        
        private Vector2 _postionInPixels;
        public Vector2 PostionInPixels => _postionInPixels;

        
        private Vector2 _positionInWindows;
        public Vector2 PositionInWindows => _positionInWindows;

        
        private void UpdatePosition()
        {
            _relativePositionInWindows = _relativePositionInPixels / _windowNode.Size * 2f;
            var parentPixelPosition = _parent == null ? Vector2.Zero : _parent.PostionInPixels;
            var parentPixelSize = _parent == null ? _windowNode.Size : _parent.SizeInPixels;
            var parentWindowPosition = _parent == null ? -Vector2.One : _parent.PositionInWindows;
            var parentSizeinWindow = _parent == null ? Vector2.One * 2f : _parent.SizeInWindows;
            var parentRotation = _parent == null ? 0 : _parent.RotationInRadians;
            Vector2 relativePosition = Vector2.Zero;
            switch (_positionRelativeType)
            {
                case PositionRelativeType.TopLeft:   relativePosition = new Vector2( _relativePositionInWindows.X, -_relativePositionInWindows.Y) - new Vector2(                   0, _sizeInWindows.Y     ) + new Vector2(                       0, parentSizeinWindow.Y     ); break;
                case PositionRelativeType.Top:       relativePosition = new Vector2( _relativePositionInWindows.X, -_relativePositionInWindows.Y) - new Vector2(_sizeInWindows.X / 2, _sizeInWindows.Y     ) + new Vector2(parentSizeinWindow.X / 2, parentSizeinWindow.Y     ); break;
                case PositionRelativeType.TopRight:  relativePosition = new Vector2(-_relativePositionInWindows.X, -_relativePositionInWindows.Y) - new Vector2(_sizeInWindows.X    , _sizeInWindows.Y     ) + new Vector2(parentSizeinWindow.X    , parentSizeinWindow.Y     ); break;
                case PositionRelativeType.Left:      relativePosition = new Vector2( _relativePositionInWindows.X,  _relativePositionInWindows.Y) - new Vector2(                   0, _sizeInWindows.Y / 2f) + new Vector2(                       0, parentSizeinWindow.Y / 2f); break;
                case PositionRelativeType.Center:    relativePosition = new Vector2( _relativePositionInWindows.X,  _relativePositionInWindows.Y) - new Vector2(_sizeInWindows.X / 2, _sizeInWindows.Y / 2f) + new Vector2(parentSizeinWindow.X / 2, parentSizeinWindow.Y / 2f); break;
                case PositionRelativeType.Right:     relativePosition = new Vector2(-_relativePositionInWindows.X,  _relativePositionInWindows.Y) - new Vector2(_sizeInWindows.X    , _sizeInWindows.Y / 2f) + new Vector2(parentSizeinWindow.X    , parentSizeinWindow.Y / 2f); break;
                case PositionRelativeType.DownLeft:  relativePosition = new Vector2( _relativePositionInWindows.X,  _relativePositionInWindows.Y) - new Vector2(                   0,                     0) + new Vector2(                       0,                         0); break;
                case PositionRelativeType.Down:      relativePosition = new Vector2( _relativePositionInWindows.X,  _relativePositionInWindows.Y) - new Vector2(_sizeInWindows.X / 2,                     0) + new Vector2(parentSizeinWindow.X / 2,                         0); break;
                case PositionRelativeType.DownRight: relativePosition = new Vector2(-_relativePositionInWindows.X,  _relativePositionInWindows.Y) - new Vector2(_sizeInWindows.X    ,                     0) + new Vector2(parentSizeinWindow.X    ,                         0); break;
            }
            _matrix =  
                Matrix4.Identity 
                * Matrix4.CreateScale(new Vector3(_sizeInWindows.X, _sizeInWindows.Y, 1))
                * Matrix4.CreateTranslation(new Vector3(relativePosition.X, relativePosition.Y, 0)) 
                * Matrix4.CreateRotationZ(parentRotation + _rotation)
                * Matrix4.CreateTranslation(parentWindowPosition.X, parentWindowPosition.Y, 0);
            var pos = _matrix.ExtractTranslation();
            _positionInWindows = new Vector2(pos.X,pos.Y);
            _postionInPixels = ((_positionInWindows / 2f) + Vector2.One * 0.5f) * _windowNode.Size;
        }

        private Vector2 _sizeInPixels = Vector2.Zero;
        public Vector2 SizeInPixels 
        {
            get 
            {
                return _sizeInPixels;
            } 
            set 
            {
                _sizeInPixels = value;
                Update();
            } 
        }
        
        
        private Vector2 _sizeInWindows;
        public Vector2 SizeInWindows
        {
            get
            {
                return _sizeInWindows;
            }
            set
            {
                SizeInPixels = value * _windowNode.Size / 2f;
            }
        }
        private void UpdateSize()
        {
            _sizeInWindows = _sizeInPixels / _windowNode.Size * 2f;
        }
        
        public void Update()
        {
            UpdateSize();
            UpdatePosition();
            foreach(var child in _children)
            {
                child.Update();
            }
        }


        public Transform2D(WindowNode windowNode, Vector2 pixelPosition, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft) 
            : this(windowNode, pixelPosition, Vector2.Zero, positionRelativeType) { }
        public Transform2D(WindowNode windowNode, Vector2 pixelPosition, Vector2 pixelSize, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft) 
            : this(windowNode, pixelPosition, pixelSize, 0f, positionRelativeType) { }
        public Transform2D(WindowNode windowNode, Vector2 pixelPosition, Vector2 pixelSize, float rotation, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft) 
        {
            _windowNode = windowNode;
            PositionRelativeType = positionRelativeType;
            SizeInPixels = pixelSize;
            RelativePositionInPixels = pixelPosition;
        }
    }
}
