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
    public class GRectangle : TextureOwnerObject
    {
        public Rectangle Rectangle;

        public GRectangle(Vector2 position, Point size, Color color) : base(position, color)
        {
            this.Rectangle = new Rectangle(Position.ToPoint(), size);
        }

        public override Point Size => Rectangle.Size;

        public override void OnDraw(GameTime time)
        {
            Debug.SpriteBatch.Draw(Texture, new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, 1), Color);
            Debug.SpriteBatch.Draw(Texture, new Rectangle(Rectangle.Left, Rectangle.Bottom, Rectangle.Width, 1), Color);
            Debug.SpriteBatch.Draw(Texture, new Rectangle(Rectangle.Left, Rectangle.Top, 1, Rectangle.Height), Color);
            Debug.SpriteBatch.Draw(Texture, new Rectangle(Rectangle.Right, Rectangle.Top, 1, Rectangle.Height + 1), Color);
        }
        public override Texture2D CreateTexture(GraphicsDevice graphicsDevice)
        {
            return Debug.DummyTexture;
        }

        public override void OnUpdate(GameTime time)
        {

        }
    }
}
