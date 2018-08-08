using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Cameras;
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
        public event Action<PositionableObject> OnMouseLeftClick;

        public Vector2 Position;

        public Point Size;

        public bool MouseIn;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(Position.ToPoint(), Size);
            }
        }
        public bool IntersectsPoint(Point point)
        {
            return Rectangle.Intersects(new Rectangle(point, new Point(1)));
        }
        public PositionableObject(Vector2 position, Point size)
        {
            this.Position = position;
            this.Size = size;
            this.MouseIn = false;
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
        public override void Update(GameTime time)
        {
            var state = Mouse.GetState();

            Point location = new Point(state.Position.X, state.Position.Y);

            location = TranslateToScenePosition(location);

            if (Rectangle.Intersects(new Rectangle(location, Debug.CURSOR_SIZE)))
            {
                OnMouseIn?.Invoke(this);

                if (!MouseIn)
                {
                    OnMouseEnter?.Invoke(this);
                }
                this.MouseIn = true;

                if (state.LeftButton == ButtonState.Pressed)
                {
                    OnMouseLeftClick?.Invoke(this);
                }
                if (state.RightButton == ButtonState.Pressed)
                {
                    OnMouseRightClick?.Invoke(this);
                }

            }
            else if (this.MouseIn)
            {
                OnMouseLeave?.Invoke(this);
                this.MouseIn = false;
            }

            base.Update(time);
        }
        public override string ToString()
        {
            return GetType().Name + " " + Rectangle;
        }
    }
}
