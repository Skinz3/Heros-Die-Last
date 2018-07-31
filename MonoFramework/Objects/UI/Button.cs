using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework.Objects.Abstract;
using MonoFramework.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects.UI
{
    public class Button : PositionableObject
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
        private string FontName
        {
            get;
            set;
        }
        private string Text
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
        public Button(Vector2 position, Point size, string spriteName, string spriteNamePressed,  string fontName, string text, Color color) : base(position, size)
        {
            this.SpriteName = spriteName;
            this.SpriteNamePressed = spriteNamePressed;
            this.FontName = fontName;
            this.Text = text;
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
        }

        public override void OnDraw(GameTime time)
        {
            Debug.SpriteBatch.Draw(this.Sprite.Texture, Rectangle, Color.White);
        }
    }
}
