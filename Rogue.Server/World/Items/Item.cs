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
    public abstract class Item
    {
        public int ItemId
        {
            get
            {
                return Record.Id;
            }
        }
        protected Player Owner
        {
            get;
            private set;
        }
        public ItemRecord Record
        {
            get;
            private set;
        }
        public int Quantity
        {
            get;
            set;
        }
        public int Slot
        {
            get;
            set;
        }
        public float Cooldown
        {
            get;
            set;
        }
        public Item(ItemRecord record, Player owner, int count)
        {
            this.Record = record;
            this.Owner = owner;
            this.Quantity = count;
        }
        public void Update(long deltaTime)
        {
            if (Cooldown > 0)
            {
                Cooldown -= deltaTime / 1000f;

                if (Cooldown <= 0)
                {
                    Cooldown = 0;
                }
            }
        }
        public bool Use(Vector2 position)
        {
            if (Cooldown == 0)
            {
                bool used = OnUse(position);

                if (used)
                {
                    NotifyCooldown();
                    Cooldown = Record.Cooldown;
                }
                return used;
            }
            else
            {
                return false;
            }
        }
        public void NotifyCooldown()
        {
            Owner.Client.Send(new NotifyItemCooldownMessage((byte)Slot, Record.Cooldown));
        }
        public abstract void OnAcquired();
        protected abstract bool OnUse(Vector2 position);

    }
}
