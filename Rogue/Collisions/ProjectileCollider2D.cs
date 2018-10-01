using Microsoft.Xna.Framework;
using MonoFramework.Objects.Abstract;
using Rogue.Network;
using Rogue.Objects.Entities;
using Rogue.Protocol.Enums;
using Rogue.World.Entities.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Collisions
{
    public class ProjectileCollider2D
    {
        public Projectile Projectile
        {
            get;
            private set;
        }
        public ProjectileCollider2D(Projectile projectile)
        {
            this.Projectile = projectile;
        }
        public void UpdateMovements(GameTime time)
        {
            bool canMove = true;

            var futurePosition = Projectile.Position + Projectile.Direction * Projectile.Speed;

            var rect = new Rectangle(futurePosition.ToPoint(), Projectile.Size);


            if (Projectile.CollisionMask.HasFlag(CollisionMask.ENTITIES))
            {
                var entities = ClientHost.Client.Player.MapInstance.GetEntities<MovableEntity>();
                foreach (var entity in entities)
                {
                    if (entity != Projectile.Owner)
                    {
                        if (entity.Collider.EntityHitBox.Intersects(rect))
                        {
                            canMove = false;
                            Projectile.OnCollide(entity, CollisionMask.ENTITIES);
                            if (!Projectile.CrossEntities)
                            {
                                ClientHost.Client.Player.MapInstance.RemoveProjectile(Projectile.Id);
                                break;
                            }
                        }
                    }
                }
            }
             if (Projectile.CollisionMask.HasFlag(CollisionMask.WALL))
            {
                foreach (var wall in Projectile.Map.Cells)
                {
                    if (!wall.Walkable && rect.Intersects(wall.Rectangle))
                    {
                        canMove = false;

                        Projectile.OnCollide(wall, CollisionMask.WALL);

                        if (!Projectile.CrossWalls)
                        {
                            ClientHost.Client.Player.MapInstance.RemoveProjectile(Projectile.Id);
                            break;
                        }
                    }
                }
            }

            if (canMove)
            {
                Projectile.Position = futurePosition;
            }

        }

    }
}
