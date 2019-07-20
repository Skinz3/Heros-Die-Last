using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Core.Sprites;

namespace Rogue.Core.Objects.UI
{
    public class Background : UIObject
    {
        private string SpriteName
        {
            get;
            set;
        }
        private Sprite Sprite
        {
            get;
            set;
        }
        public Background(string spriteName) : base(new Vector2(), new Point(Debug.GraphicsDevice.Viewport.Width, Debug.GraphicsDevice.Viewport.Height), Color.White)
        {
            this.SpriteName = spriteName;
        }

        public override void OnDraw(GameTime time)
        {
            Sprite.Draw(Rectangle, Color);
        }

        public override void OnInitializeComplete()
        {
            this.Sprite = SpriteManager.GetSprite(SpriteName);
        }
        public override void OnUpdate(GameTime time)
        {
           
        }

        public override void OnInitialize()
        {
           
        }

        public override void OnDispose()
        {
            Sprite.Dispose();
        }

        
    }
}
