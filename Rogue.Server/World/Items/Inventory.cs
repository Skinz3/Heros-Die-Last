using Microsoft.Xna.Framework;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Items
{
    public class Inventory
    {
        public const byte SLOTS = 4;

        private Player Player
        {
            get;
            set;
        }
        public Item[] Items
        {
            get;
            private set;
        }
        public Inventory(Player player)
        {
            this.Player = player;
            this.Items = new Item[SLOTS];
        }
        public bool UseItem(Vector2 position, int slot)
        {
            if (Items[slot] != null)
            {
                bool success = Items[slot].Use(position);

                if (success)
                {
                    Items[slot].Quantity--;

                    if (Items[slot].Quantity <= 0)
                    {
                        RemoveItem(slot);
                    }
                    else
                    {
                        NotifyItemUpdated(slot, Items[slot].Quantity);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        private int FindFreeSlot()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }
        public Item GetItem(int itemId)
        {
            return Items.Where(x => x != null).FirstOrDefault(x => x.ItemId == itemId);
        }
        public void Update(long deltaTime)
        {
            foreach (var item in Items)
            {
                item?.Update(deltaTime);
            }
        }
        public bool AddItem(Item item)
        {
            var item2 = GetItem(item.ItemId);

            if (item2 != null)
            {
                item2.Quantity += item.Quantity;
                NotifyItemUpdated(item2.Slot, item2.Quantity);
                return true;
            }
            int freeSlot = FindFreeSlot();

            if (freeSlot != -1)
            {
                Items[freeSlot] = item;
                item.Slot = freeSlot;
                item.OnAcquired();
                NotifyItemAdded(item, freeSlot);
                return true;
            }
            return false;
        }


        public Item AddItem(int itemId, int quantity)
        {
            var item = ItemManager.GetItemInstance(Player, ItemRecord.GetItem(itemId), quantity);

            if (item.Record.InstantUse)
            {
                if (item.Quantity > 1)
                {
                    throw new Exception("InstantUse? => Quantity = 1");
                }
                item.Use(new Vector2());
                return null;
            }
            AddItem(item);
            return item;
        }
        public void RemoveItem(int slot)
        {
            Items[slot] = null;
            NotifyItemRemoved(slot);
        }
        private void NotifyItemUpdated(int slot, int quantity)
        {
            Player.Client.Send(new InventoryUpdateQuantityMessage((byte)slot, quantity));
        }
        private void NotifyItemAdded(Item item, int slot)
        {
            Player.Client.Send(new InventoryAddItemMessage(item.Record.Id, item.Record.Icon, item.Quantity, (byte)slot));
        }
        private void NotifyItemRemoved(int slot)
        {
            Player.Client.Send(new InventoryRemoveItemMessage((byte)slot));
        }
    }

}
