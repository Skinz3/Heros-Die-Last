using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoFramework.Geometry;
using MonoFramework.Input;
using MonoFramework.Objects.Abstract;

namespace MonoFramework.Objects.UI
{
    public class SimpleButton : UIObject
    {
        private Color PressedColor
        {
            get;
            set;
        }
        private Action<PositionableObject> OnClicked
        {
            get;
            set;
        }
        public bool Fill
        {
            get;
            set;
        }
        public int BorderSize
        {
            get;
            set;
        }
        public Color BorderColor
        {
            get;
            set;
        }
        public bool DrawRectangle
        {
            get;
            set;
        }
        public SimpleButton(Vector2 position, Point size, string text,
            Action<PositionableObject> onClicked) : base(position, size, Color.White)
        {
            this.OnClicked = onClicked;
            this.OnMouseLeftClick += OnClicked;
            this.OnMouseLeftDown += SimpleButton_OnMouseLeftDown;
            MouseManager.OnLeftButtonPressed += MouseManager_OnLeftButtonPressed;
            this.PressedColor = Color.Gray;
            this.SetText(text, Color.Black, RectangleOrigin.Center);
            this.BorderColor = Color.DarkGray;
            this.BorderSize = 2;
            this.DrawRectangle = true;
            this.Fill = true;

        }


        private void MouseManager_OnLeftButtonPressed()
        {

            Color = DefaultColor;
        }

        private void SimpleButton_OnMouseLeftDown(PositionableObject obj)
        {
            Color = PressedColor;
        }

        public override void OnDraw(GameTime time)
        {
            if (Fill && DrawRectangle)
                Debug.FillRectangle(Rectangle, Color);
            else if (DrawRectangle)
                Debug.DrawRectangle(Rectangle, Color, 2);

            if (BorderSize > 0 && Fill)
            {
                var borderRectangle = Rectangle;
                borderRectangle.Location -= new Point(BorderSize / 2);
                Debug.DrawRectangle(borderRectangle, BorderColor, BorderSize);
            }
        }

        public override void OnInitialize()
        {

        }

        public override void OnInitializeComplete()
        {

        }

        public override void OnUpdate(GameTime time)
        {

        }
        public override void OnDispose()
        {
            this.OnMouseLeftClick -= OnClicked;
            MouseManager.OnLeftButtonPressed -= MouseManager_OnLeftButtonPressed;
            this.OnMouseLeftDown -= SimpleButton_OnMouseLeftDown;
        }
    }
}
