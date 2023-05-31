using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue.Core.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Objects
{
    public class GText : ColorableObject
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

        private float m_scale;

        public float Scale
        {
            get
            {
                return m_scale;
            }
            set
            {

                m_scale = value;

                if (Text != null)
                    UpdateSize();
            }
        }

        private void UpdateSize()
        {
            this.Size = SpriteFont.MeasureString(Text).ToPoint() * new Vector2(Scale, Scale).ToPoint();
        }
        public GText(Vector2 position, SpriteFont font, string text, Color color, float scale) : base(position, new Point(), color)
        {
            this.SpriteFont = font;
            this.Text = text;
            this.Scale = scale;
            this.Color = color;
        }

        public override void OnDraw(GameTime time)
        {
            Debug.SpriteBatch.DrawString(SpriteFont, Text, Position, Color, 0f, new Vector2(), Scale, SpriteEffects.None, 0f);
        }


        public override void OnUpdate(GameTime time)
        {
            base.OnUpdate(time);
        }

        public override void OnInitialize()
        {
           
        }

        public override void OnInitializeComplete()
        {
           
        }

        public override void OnDispose()
        {
           
        }
    }
}
