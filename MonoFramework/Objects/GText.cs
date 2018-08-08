using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects
{
    public class GText : PositionableObject
    {
        private SpriteFont SpriteFont
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }

        public Color Color;

        public float Scale
        {
            get;
            set;
        }

        public GText(Vector2 position,SpriteFont font, string text, Color color, float scale) : base(position, new Point(50, 50))
        {
            this.SpriteFont = font;
            this.Text = text;
            this.Color = color;
            this.Scale = scale;
        }
     
        public override void OnDraw(GameTime time)
        {
            Debug.SpriteBatch.DrawString(SpriteFont, Text, Position, Color, 0f, new Vector2(), 1f, SpriteEffects.None, 1f);
        }


        public override void OnUpdate(GameTime time)
        {
            this.Size = SpriteFont.MeasureString(Text).ToPoint() * new Vector2(Scale, Scale).ToPoint();
        }

        public override void OnInitializeComplete()
        {

        }

        public override void OnInitialize()
        {
         
        }
    }
}
