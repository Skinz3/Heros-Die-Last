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
    [ItemHandler(315)]
    public class Heal : Item
    {
        public Heal(ItemRecord record, int count) : base(record, count)
        {
        }

        public override bool Use(Player owner, Vector2 position)
        {
            owner.InflictDamage(owner, -1000);
            return true;
        }
    }
}
