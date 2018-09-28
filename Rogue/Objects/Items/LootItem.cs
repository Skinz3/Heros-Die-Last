using Microsoft.Xna.Framework;
using MonoFramework;
using MonoFramework.Geometry;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using Rogue.World.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Objects.Items
{
    public class LootItem : ColorableObject
    {
        private Item Item
        {
            get;
            set;
        }
        private GCell Cell
        {
            get;
            set;
        }
        public LootItem(Item item, GCell cell, Color color) : base(new Vector2(), new Point(), Color.White)
        {
            this.Item = item;
            this.Cell = cell;
            this.Size = new Point(32, 32);
            this.Position = cell.Position;
            this.Align(cell.Rectangle, RectangleOrigin.Center);
        }

        public override void OnDispose()
        {

        }

        public override void OnDraw(GameTime time)
        {
            Item.Draw(Rectangle, Color);

            if (Item.Quantity > 1)
                Debug.DrawText(Rectangle.Location.ToVector2(), Item.Quantity.ToString(), Color);
        }

        public override void OnInitialize()
        {

        }

        public override void OnInitializeComplete()
        {

        }

        public override void OnUpdate(GameTime time)
        {
            int sizeIncrement = 2;
            int sizeMaxMul = 10;
            Color = Color * 0.99f;

            if (Size.X < (Item.Sprite.Texture.Bounds.Size + new Point(sizeIncrement) * new Point(sizeMaxMul)).X)
            {
                Size += new Point(sizeIncrement, sizeIncrement);
                Position -= new Vector2(sizeIncrement / 2, sizeIncrement / 2);
            }

            Position += new Vector2(0, -1);
        }
    }
}
