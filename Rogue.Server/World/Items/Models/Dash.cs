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
    [ItemHandler(405)]
    public class Dash : Item
    {
        public Dash(ItemRecord record, int count) : base(record, count)
        {

        }

        public override bool Use(Player owner, Vector2 position)
        {
            if (!owner.Dashing)
            {
                owner.Dash(position, 10f, 300);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
