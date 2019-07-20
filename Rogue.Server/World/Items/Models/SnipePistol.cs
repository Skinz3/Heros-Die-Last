using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Core.Collisions;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;

namespace Rogue.Server.World.Items.Models
{
    [ItemHandler(103)]
    public class SnipePistol : Item
    {
        public SnipePistol(ItemRecord record, Player owner, int count) : base(record, owner, count)
        {
        }

        public override void OnAcquired()
        {
            Owner.DefineWeaponAnimation("item103");

        }

        protected override bool OnUse(Vector2 position)
        {
            Owner.MapInstance.Send(new HitscanHitMessage(Owner.Id, position));

            var results = Owner.MapInstance.Raycast<MovableEntity>(new Ray2D(Owner.Position, position));

            foreach (var target in results)
            {
                var distance = Owner.GetDistance(target);
                target.InflictDamage(Owner, 800);
            }

            return true;
        }
    }
}
