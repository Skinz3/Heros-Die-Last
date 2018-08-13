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
        public SpriteFont SpriteFont
        {
            get;
            set;
        }

        private string m_text;

        public new string Text // lol
        {
            get
            {
                return m_text;
            }
            set
            {
                m_text = value;
                UpdateSize();
            }
        }

        public Color Color;

        public float Scale
        {
            get;
            set;
        }

        private void UpdateSize()
        {
            this.Size = SpriteFont.MeasureString(Text).ToPoint() * new Vector2(Scale, Scale).ToPoint();
        }
        public GText(Vector2 position, SpriteFont font, string text, Color color, float scale) : base(position, new Point(50, 50))
        {
            this.SpriteFont = font;
            this.Scale = scale;
            this.Text = text;
            this.Color = color;
        }

        public override void OnDraw(GameTime time)
        {
            Debug.SpriteBatch.DrawString(SpriteFont, Text, Position, Color, 0f, new Vector2(), Scale, SpriteEffects.None, 0f);
        }


        public override void OnUpdate(GameTime time)
        {
          
        }
        public override void OnInitializeComplete()
        {

        }

        public override void OnInitialize()
        {
        }

        public override void OnDispose()
        {
           
        }
    }
}
