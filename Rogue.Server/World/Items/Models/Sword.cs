using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;

namespace Rogue.Server.World.Items.Models
{
    [ItemHandler(76)]
    public class Sword : Item
    {
        public Sword(ItemRecord record, Player owner, int count) : base(record, owner, count)
        {
        }

        public override void OnAcquired()
        {
        }

        protected override bool OnUse(Vector2 position)
        {
            return true;
        }
    }
}
