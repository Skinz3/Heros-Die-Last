using Microsoft.Xna.Framework;
using MonoFramework;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
using MonoFramework.Sprites;
using Rogue.World.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Objects.UI
{
    public class Inventory : ColorableObject
    {
        public const byte SLOTS = 4;

        public const int SIZE_MULTIPLIER = 4;

        public const string SPRITE_NAME = "inventory";

        private Item[] Items
        {
            get;
            set;
        }
        private Rectangle[] Slots
        {
            get;
            set;
        }
        Sprite Sprite
        {
            get;
            set;
        }
        public Inventory() : base(new Vector2(), new Point(), Color.White * 0.5f)
        {
            this.Sprite = SpriteManager.GetSprite(SPRITE_NAME);
            Size = Sprite.Texture.Bounds.Size * new Point(SIZE_MULTIPLIER);
            this.Position = new Vector2((Debug.ScreenSize.X / 2) - Size.X / 2, 700);
            this.Items = new Item[SLOTS];
            GenerateSlots();
        }

        public void RemoveItem(byte slot)
        {
            this.Items[slot] = null;
        }

        private void GenerateSlots()
        {
            Slots = new Rectangle[SLOTS];

            for (int i = 0; i < 4; i++)
            {
                Slots[i] = new Rectangle(Rectangle.X + ((18 * SIZE_MULTIPLIER) * i) + ((3 * SIZE_MULTIPLIER) * i), Rectangle.Y, 18 * SIZE_MULTIPLIER, 18 * SIZE_MULTIPLIER);
            }
        }
        public override void OnDispose()
        {

        }

        public override void OnDraw(GameTime time)
        {
            Sprite.Draw(Rectangle, Color);

            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] != null)
                {
                    Items[i].Draw(Slots[i], Color.White);
                    Debug.DrawText(Slots[i].Location.ToVector2(), Items[i].Quantity.ToString(), Color.White);
                }
            }
        }

        public void UpdateQuantity(byte slot, int quantity)
        {
            Items[slot].Quantity = quantity;
        }
        public void AddItem(Item item, int slot)
        {
            this.Items[slot] = item;
        }
        public override void OnInitialize()
        {

        }

        public override void OnInitializeComplete()
        {

        }

        public override void OnUpdate(GameTime time)
        {

        }
        public override void Dispose()
        {

        }
    }
}
