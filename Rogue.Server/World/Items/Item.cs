using Microsoft.Xna.Framework;
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
        public Item(ItemRecord record, int count)
        {
            this.Record = record;
            this.Quantity = count;
        }

        public abstract bool Use(Player owner, Vector2 position);

    }
}
