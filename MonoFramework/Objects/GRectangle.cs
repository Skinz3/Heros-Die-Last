using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue.Core.Cameras;
using Rogue.Core.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Objects
{
    public class GRectangle : SingleTextureObject
    {
        public int Thickness
        {
            get;
            set;
        }
        public GRectangle(Vector2 position, Point size, Color color,int thickness) : base(position, size, color)
        {
            this.Thickness = thickness;
        }

        public override void OnDraw(GameTime time)
        {
            Debug.SpriteBatch.Draw(Texture, new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Thickness), Color);
            Debug.SpriteBatch.Draw(Texture, new Rectangle(Rectangle.Left, Rectangle.Bottom, Rectangle.Width, Thickness), Color);
            Debug.SpriteBatch.Draw(Texture, new Rectangle(Rectangle.Left, Rectangle.Top, Thickness, Rectangle.Height), Color);
            Debug.SpriteBatch.Draw(Texture, new Rectangle(Rectangle.Right, Rectangle.Top, Thickness, Rectangle.Height + 1), Color);

           
        }
        public override Texture2D CreateTexture(GraphicsDevice graphicsDevice)
        {
            return Debug.DummyTexture;
        }

        public override void OnUpdate(GameTime time)
        {

        }

        public override void OnDispose()
        {
          
        }
    }
}
