using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;

namespace Rogue.Server.World.Items.Models
{
    [ItemHandler(282)]
    public class Dash : Item
    {
        public Dash(ItemRecord record, Player owner, int count) : base(record, owner, count)
        {

        }

        public override void OnAcquired()
        {
            Owner.DefineAura(new Color(Color.DarkOrange, 80), 150F, 0.02f);
        }

        protected override bool OnUse(Vector2 position)
        {
            if (!Owner.Dashing)
            {
                Owner.Dash(position, 15f, 300, "item282");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
