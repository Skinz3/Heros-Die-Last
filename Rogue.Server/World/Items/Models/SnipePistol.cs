using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoFramework.Collisions;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;

namespace Rogue.Server.World.Items.Models
{
    [ItemHandler(103)]
    public class SnipePistol : Item
    {
        public SnipePistol(ItemRecord record, int count) : base(record, count)
        {
        }

        public override bool Use(Player owner, Vector2 position)
        {
            owner.MapInstance.Send(new HitscanHitMessage(owner.Id, position));

            var results = owner.MapInstance.Raycast<MovableEntity>(new Ray2D(owner.Position, position));

            foreach (var target in results)
            {
                var distance = owner.GetDistance(target);
                target.InflictDamage(owner, (int)distance * 2);
            }
          
            return true;
        }
    }
}
