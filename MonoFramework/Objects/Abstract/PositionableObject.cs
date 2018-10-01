using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Cameras;
using MonoFramework.DesignPattern;
using MonoFramework.Geometry;
using MonoFramework.Input;
using MonoFramework.PhysX;
using MonoFramework.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects.Abstract
{
    public abstract class PositionableObject : GameObject
    {
        public event Action<PositionableObject> OnMouseEnter;
        public event Action<PositionableObject> OnMouseLeave;
        public event Action<PositionableObject> OnMouseIn;

        public event Action<PositionableObject> OnMouseRightClick;
        public event Action<PositionableObject> OnMouseRightDown;
        /// <summary>
        /// Pas bon! un seul event souris, pas plusieurs 
        /// </summary>
        public event Action<PositionableObject> OnMouseLeftClick;
        public event Action<PositionableObject> OnMouseLeftDown;

        public Vector2 Position;

        public Point Size;

        public bool MouseIn;


        public Vector2 Center
        {
            get
            {
                return Rectangle.Center.ToVector2();
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(Position.ToPoint(), Size);
            }
        }
        public GText Text
        {
            get;
            private set;
        }
        public RectangleOrigin TextOrigin
        {
            get;
            set;
        }
        private List<Force2D> Forces
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;
        }
        public bool IntersectsPoint(Point point)
        {
            return Rectangle.Intersects(new Rectangle(point, new Point(1)));
        }
        public PositionableObject(Vector2 position, Point size, float rotation = 0)
        {
            this.Position = position;
            this.Size = size;
            this.MouseIn = false;
            this.Forces = new List<Force2D>();
            this.Rotation = rotation;
            MouseManager.OnLeftButtonPressed += OnLeftButtonPressed;
            MouseManager.OnRightButtonPressed += OnRightButtonPressed;
            MouseManager.OnLeftButtonDown += OnLeftButtonDown;
            MouseManager.OnRightButtonDown += OnRightButtonDown;
        }
        public Point TranslateToScenePosition(Point point)
        {
            if (Layer != LayerEnum.UI)
            {
                float tX = point.X / Camera2D.MainCamera.Zoom;
                float tY = point.Y / Camera2D.MainCamera.Zoom;

                tX += Camera2D.MainCamera.Position.X;
                tY += Camera2D.MainCamera.Position.Y;

                return new Point((int)tX, (int)tY);

            }
            else
            {
                return point;
            }
        }


        private void OnLeftButtonPressed()
        {
            if (MouseIn) // MouseIn?
            {
                OnMouseLeftClick?.Invoke(this);
            }
        }
        private void OnRightButtonPressed()
        {
            if (MouseIn) // MouseIn?
            {
                OnMouseRightClick?.Invoke(this);
            }
        }

        private void OnLeftButtonDown()
        {
            if (MouseIn) // MouseIn?
            {
                OnMouseLeftDown?.Invoke(this);
            }
        }

        private void OnRightButtonDown()
        {
            if (MouseIn) // MouseIn?
            {
                OnMouseRightDown?.Invoke(this);
            }
        }


        public void SetText(string text, Color color, RectangleOrigin origin = RectangleOrigin.Center, float scale = 1f)
        {
            SetText(new GText(Position, SceneManager.CurrentScene.TextRenderer.GetDefaultSpriteFont(),
                text, color, scale), origin);
        }
        public void SetText(GText text, RectangleOrigin origin = RectangleOrigin.Center)
        {
            Text = text;

            if (origin != RectangleOrigin.None)
            {
                TextOrigin = origin;
                Text.Align(Rectangle, origin);
            }
        }

        public void RemoveText()
        {
            Text = null;
        }
        public void AddForce(Force2D force)
        {
            Forces.Add(force);
        }
        public override void Draw(GameTime time)
        {
            base.Draw(time);
            Text?.Draw(time);
        }
        private bool MouseIsIn()
        {
            var state = Mouse.GetState();

            Point location = new Point(state.Position.X, state.Position.Y);

            location = TranslateToScenePosition(location);

            return Rectangle.Intersects(new Rectangle(location, Debug.CURSOR_SIZE));
        }
        public override void Update(GameTime time)
        {
            foreach (var force in Forces)
            {
                force.Apply(this);
            }

            Text?.Update(time);
            if (TextOrigin != RectangleOrigin.None)
                Text?.Align(Rectangle, TextOrigin);

            if (MouseIsIn())
            {
                OnMouseIn?.Invoke(this);

                if (!MouseIn)
                {
                    OnMouseEnter?.Invoke(this);
                }
                this.MouseIn = true;

            }
            else if (this.MouseIn)
            {
                OnMouseLeave?.Invoke(this);
                this.MouseIn = false;
            }

            base.Update(time);
        }
        public override void Dispose()
        {
            MouseManager.OnLeftButtonPressed -= OnLeftButtonPressed;
            MouseManager.OnRightButtonPressed -= OnRightButtonPressed;
            MouseManager.OnLeftButtonDown -= OnLeftButtonDown;
            MouseManager.OnRightButtonDown -= OnRightButtonDown;
            base.Dispose();
        }

        [InDeveloppement(InDeveloppementState.STARTED, "Handle other RectangleOrigin.")]
        public void Align(Rectangle container, RectangleOrigin origin)
        {
            switch (origin)
            {
                case RectangleOrigin.Center:
                    this.Position = new Vector2(container.X + (container.Width / 2) - (Size.X / 2),
                  container.Y + (container.Height / 2) - (Size.Y / 2));
                    break;
                case RectangleOrigin.CenterTop:
                    this.Position = new Vector2(container.X + (container.Width / 2) - (Size.X / 2),
                container.Y);
                    break;
                case RectangleOrigin.CenterBottom:
                    this.Position = new Vector2(container.X + (container.Width / 2) - (Size.X / 2),
                container.Y + (container.Width) - (Size.X / 2));
                    break;
                case RectangleOrigin.CenterRight:
                    this.Position = new Vector2(container.X + (container.Width) - (Size.X),
                  container.Y + (container.Height / 2) - (Size.Y / 2));
                    break;
                case RectangleOrigin.CenterLeft:
                    this.Position = new Vector2(container.X,
                  container.Y + (container.Height / 2) - (Size.Y / 2));
                    break;
                case RectangleOrigin.TopLeft:
                    this.Position = container.Location.ToVector2();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        public override string ToString()
        {
            return GetType().Name + " " + Rectangle;
        }
    }
}
