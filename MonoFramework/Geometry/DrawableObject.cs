using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects.Abstract
{
    public abstract class DrawableObject : PositionableObject
    {
        public DrawableObject(Vector2 position, Point size, Color color) : base(position, size)
        {
            this.Color = color;
        }
        protected Texture2D Texture
        {
            get;
            private set;
        }
        public Color Color
        {
            get;
            set;
        }

        public override void OnInitialize()
        {
            this.Texture = CreateTexture(Debug.GraphicsDevice);
        }
        public override void OnInitializeComplete()
        {

        }
        public override void OnDraw(GameTime time)
        {
            Debug.SpriteBatch.Draw(Texture, new Rectangle(Position.ToPoint(), Size), Color.White);
        }

        public abstract Texture2D CreateTexture(GraphicsDevice graphicsDevice);
    }
}
