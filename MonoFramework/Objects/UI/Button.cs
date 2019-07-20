using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue.Core.Geometry;
using Rogue.Core.Input;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Objects.UI
{
    public class Button : ColorableObject
    {
        private string SpriteName
        {
            get;
            set;
        }
        private string SpriteNamePressed
        {
            get;
            set;
        }
        private Sprite Sprite
        {
            get;
            set;
        }
        private Sprite SpritePressed
        {
            get;
            set;
        }
        private Sprite CurrentSprite
        {
            get;
            set;
        }
        private Action<PositionableObject> OnClick
        {
            get;
            set;
        }
        public bool UseSpriteSize
        {
            get;
            set;
        }
        public Button(Vector2 position, Point size, string spriteName, string spriteNamePressed, string text, Action<PositionableObject> onClicked) : base(position, size, Color.White)
        {
            this.SpriteName = spriteName;
            this.SpriteNamePressed = spriteNamePressed;
            this.OnClick = onClicked;
            this.OnMouseLeftDown += Button_OnMouseLeftDown;
            this.OnMouseLeftClick += onClicked;
            this.SetText(text, Color.Black, RectangleOrigin.Center);
            MouseManager.OnLeftButtonPressed += MouseManager_OnLeftButtonPressed;
        }

        private void MouseManager_OnLeftButtonPressed()
        {
            if (!MouseIn)
            {
                this.CurrentSprite = Sprite;
            }
        }

        private void Button_OnMouseLeftDown(PositionableObject obj)
        {
            this.CurrentSprite = SpritePressed;
        }

        public override void OnUpdate(GameTime time)
        {

        }

        public override void OnInitialize()
        {

        }

        public override void OnInitializeComplete()
        {
            this.Sprite = SpriteManager.GetSprite(SpriteName);
            this.SpritePressed = SpriteManager.GetSprite(SpriteNamePressed);
            this.CurrentSprite = Sprite;
        }

        public override void OnDispose()
        {
            this.OnMouseLeftClick -= OnClick;
        }
        public override void OnDraw(GameTime time)
        {
            if (UseSpriteSize)
            {
                CurrentSprite.Draw(CurrentSprite.Texture.Bounds, Color);
            }
            else
                CurrentSprite.Draw(Rectangle, Color);
        }

       
    }
}
