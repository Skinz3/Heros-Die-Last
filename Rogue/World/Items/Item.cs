using Microsoft.Xna.Framework;
using Rogue.Core.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.World.Items
{
    public class Item
    {
        public Sprite Sprite
        {
            get;
            private set;
        }
        public int Quantity
        {
            get;
            set;
        }
        public float CooldownCurrent
        {
            get;
            set;
        }
        public float CooldownTotal
        {
            get;
            set;
        }
        public Item(Sprite sprite, int quantity)
        {
            this.Sprite = sprite;
            this.Quantity = quantity;
        }
        public void Draw(Rectangle rectangle, Color color)
        {
            Sprite.Draw(rectangle, color);
        }
        public static Item CreateInstance(int itemId, string icon, int quantity)
        {
            var sprite = SpriteManager.GetSprite(icon);
            return new Item(sprite, quantity);
        }
    }
}
