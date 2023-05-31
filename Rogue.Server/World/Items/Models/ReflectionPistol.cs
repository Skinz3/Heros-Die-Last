using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Core.Collisions;
using Rogue.Core.Geometry;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;
using Rogue.Server.World.Projectiles;

namespace Rogue.Server.World.Items.Models
{
    [ItemHandler(104)]
    public class ReflectionPistol : Item
    {
        public ReflectionPistol(ItemRecord record, Player owner, int count) : base(record, owner, count)
        {
        }

        public override void OnAcquired()
        {
        }

        protected override bool OnUse(Vector2 position)
        {
            var direction = position - Owner.Center;
            direction.Normalize();

            int size = 25;

            var startPos = Owner.Center - new Vector2(size / 2f, size / 2f);

            ReflectionProjectile projectile = new ReflectionProjectile(Owner.MapInstance.GetNextEntityId(), startPos, size, 10f, direction, Owner, "fireBlue", 3000f);

            if (projectile.Validate() == false)
            {
                projectile.Dispose();
                return false;
            }
            projectile.DefineMapInstance(Owner.MapInstance);
            Owner.MapInstance.Send(projectile.GetProjectileCreateMessage());
            return true;
        }
    }
}
