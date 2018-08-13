using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
using MonoFramework.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects
{
    public class GCursor : SingleTextureObject
    {
        public bool Active
        {
            get
            {
                return Sprite != null;
            }
        }
        public Sprite Sprite
        {
            get;
            set;
        }
        public GCursor(Color color, Sprite sprite, Point textureSize) : base(new Vector2(), textureSize, color)
        {
            this.Sprite = sprite;
        }

        public override Texture2D CreateTexture(GraphicsDevice graphicsDevice)
        {
            return null;
        }
        public override void OnDraw(GameTime time)
        {
            if (Active)
                Debug.SpriteBatch.Draw(Sprite.Texture, new Rectangle(Position.ToPoint(), Size), Color);
        }
        public override void OnUpdate(GameTime time)
        {
            if (Active)
            {
                var mousePosition = Mouse.GetState().Position.ToVector2();
                this.Position = new Vector2(mousePosition.X - Size.X / 2, mousePosition.Y - Size.Y / 2);
            }
        }

        public override void OnDispose()
        {
           
        }
    }
}
