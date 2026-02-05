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

        private Matrix4 _matrixInWindows;
        public Matrix4 MatrixInWindows => _matrixInWindows;

        private Matrix4 _matrixInPixels;
        public Matrix4 MatrixInPixels => _matrixInPixels;


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
            var parentSizeinPixels = _parent == null ? _windowNode.Size : _parent.SizeInPixels;
            var parentWindowPosition = _parent == null ? -Vector2.One : _parent.PositionInWindows;
            var parentSizeinWindow = _parent == null ? Vector2.One * 2f : _parent.SizeInWindows;
            var parentRotation = _parent == null ? 0 : _parent.RotationInRadians;
            Vector2 relativePositioninPixels = Vector2.Zero;
            switch (_positionRelativeType)
            {
                case PositionRelativeType.TopLeft:   relativePositioninPixels = new Vector2( _relativePositionInPixels.X, -_relativePositionInPixels.Y) - new Vector2(                  0, _sizeInPixels.Y     ) + new Vector2(                       0, parentSizeinPixels.Y     ); break;
                case PositionRelativeType.Top:       relativePositioninPixels = new Vector2( _relativePositionInPixels.X, -_relativePositionInPixels.Y) - new Vector2(_sizeInPixels.X / 2, _sizeInPixels.Y     ) + new Vector2(parentSizeinPixels.X / 2, parentSizeinPixels.Y     ); break;
                case PositionRelativeType.TopRight:  relativePositioninPixels = new Vector2(-_relativePositionInPixels.X, -_relativePositionInPixels.Y) - new Vector2(_sizeInPixels.X    , _sizeInPixels.Y     ) + new Vector2(parentSizeinPixels.X    , parentSizeinPixels.Y     ); break;
                case PositionRelativeType.Left:      relativePositioninPixels = new Vector2( _relativePositionInPixels.X,  _relativePositionInPixels.Y) - new Vector2(                  0, _sizeInPixels.Y / 2f) + new Vector2(                       0, parentSizeinPixels.Y / 2f); break;
                case PositionRelativeType.Center:    relativePositioninPixels = new Vector2( _relativePositionInPixels.X,  _relativePositionInPixels.Y) - new Vector2(_sizeInPixels.X / 2, _sizeInPixels.Y / 2f) + new Vector2(parentSizeinPixels.X / 2, parentSizeinPixels.Y / 2f); break;
                case PositionRelativeType.Right:     relativePositioninPixels = new Vector2(-_relativePositionInPixels.X,  _relativePositionInPixels.Y) - new Vector2(_sizeInPixels.X    , _sizeInPixels.Y / 2f) + new Vector2(parentSizeinPixels.X    , parentSizeinPixels.Y / 2f); break;
                case PositionRelativeType.DownLeft:  relativePositioninPixels = new Vector2( _relativePositionInPixels.X,  _relativePositionInPixels.Y) - new Vector2(                  0,                    0) + new Vector2(                       0,                         0); break;
                case PositionRelativeType.Down:      relativePositioninPixels = new Vector2( _relativePositionInPixels.X,  _relativePositionInPixels.Y) - new Vector2(_sizeInPixels.X / 2,                    0) + new Vector2(parentSizeinPixels.X / 2,                         0); break;
                case PositionRelativeType.DownRight: relativePositioninPixels = new Vector2(-_relativePositionInPixels.X,  _relativePositionInPixels.Y) - new Vector2(_sizeInPixels.X    ,                    0) + new Vector2(parentSizeinPixels.X    ,                         0); break;
            }
            _matrixInPixels =
                Matrix4.Identity
                * Matrix4.CreateScale(new Vector3(_sizeInPixels.X, _sizeInPixels.Y, 1))
                * Matrix4.CreateTranslation(new Vector3(-_sizeInPixels.X/2f, -_sizeInPixels.Y/2f, 0))
                * Matrix4.CreateRotationZ(_rotation)
                * Matrix4.CreateTranslation(new Vector3(_sizeInPixels.X / 2f, _sizeInPixels.Y / 2f, 0))
                * Matrix4.CreateTranslation(new Vector3(relativePositioninPixels.X, relativePositioninPixels.Y, 0))
                * Matrix4.CreateRotationZ(parentRotation)
                * Matrix4.CreateTranslation(parentPixelPosition.X, parentPixelPosition.Y, 0)
                ;
            _matrixInWindows = _matrixInPixels * Matrix4.CreateScale(2f / _windowNode.Size.X, 2f / _windowNode.Size.Y, 1) * Matrix4.CreateTranslation(-1,-1,0);
            var pos1 = _matrixInPixels.ExtractTranslation();
            _postionInPixels = new Vector2(pos1.X, pos1.Y);
            var pos2 = _matrixInWindows.ExtractTranslation();
            _positionInWindows = new Vector2(pos2.X,pos2.Y);
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

        public Transform2D(Transform2D parent, Vector2 pixelPosition, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft)
            : this(parent._windowNode, pixelPosition, Vector2.Zero, 0f, positionRelativeType) { this.Parent = parent; }
        public Transform2D(Transform2D parent, Vector2 pixelPosition, Vector2 pixelSize, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft)
            : this(parent._windowNode, pixelPosition, pixelSize, 0f, positionRelativeType) { this.Parent = parent; }
        public Transform2D(Transform2D parent, Vector2 pixelPosition, Vector2 pixelSize, float rotation, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft)
            : this(parent._windowNode, pixelPosition, pixelSize, rotation, positionRelativeType) { this.Parent = parent; }


        public Transform2D(WindowNode windowNode, Vector2 pixelPosition, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft) 
            : this(windowNode, pixelPosition, Vector2.Zero, 0f, positionRelativeType) { }
        public Transform2D(WindowNode windowNode, Vector2 pixelPosition, Vector2 pixelSize, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft) 
            : this(windowNode, pixelPosition, pixelSize, 0f, positionRelativeType) { }
        public Transform2D(WindowNode windowNode, Vector2 pixelPosition, Vector2 pixelSize, float rotation, PositionRelativeType positionRelativeType = PositionRelativeType.DownLeft) 
        {
            _windowNode = windowNode;
            _positionRelativeType = positionRelativeType;
            _sizeInPixels = pixelSize;
            _relativePositionInPixels = pixelPosition;
            _rotation = rotation;
            Update();
        }
    }
}
