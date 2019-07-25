using Microsoft.Xna.Framework;
using Rogue.Core;
using Rogue.Core.Geometry;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Scenes;
using Rogue.Protocol.Enums;
using Rogue.Server.World.Entities;
using Rogue.Server.World.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Collisions
{
    public class ProjectileCollider2D
    {
        public Projectile Projectile
        {
            get;
            private set;
        }
        public bool CanMove
        {
            get;
            set;
        }
        public ProjectileCollider2D(Projectile projectile)
        {
            this.Projectile = projectile;
        }
        public void UpdateMovements(long deltaTime)
        {
            var oldCan = this.CanMove;

            this.CanMove = true;

            var futurePosition = Projectile.Position + Projectile.Direction * Projectile.Speed;

            var futureCenter = Projectile.Center + Projectile.Direction * Projectile.Speed;

            //var rect = new Rectangle(futurePosition.ToPoint(), Projectile.Size);

            if (Projectile.CollisionMask.HasFlag(CollisionMask.ENTITIES))
            {
                var entities = Projectile.MapInstance.GetEntities<MovableEntity>();
                foreach (var entity in entities)
                {
                    if (entity != Projectile.Owner)
                    {
                        if (GeometryExtensions.CircleRectangleCollide(futureCenter, (Projectile.SizeF / 2), entity.Collider.EntityHitBox))
                        {
                            CanMove = false;
                            Projectile.OnCollide(entity, CollisionMask.ENTITIES);
                            if (!Projectile.CrossEntities)
                            {
                                Projectile.WaitingForDispose = true;
                                break;
                            }
                        }
                    }
                }
            }

            if (Projectile.CollisionMask.HasFlag(CollisionMask.WALL))
            {
                foreach (var wall in Projectile.MapInstance.Record.Grid.GetCells()) 
                {
                    if (!wall.Walkable && GeometryExtensions.CircleRectangleCollide(futureCenter, (Projectile.SizeF / 2), wall.Rectangle))
                    {

                        CanMove = false;

                        if (Projectile.OnCollide(wall, CollisionMask.WALL))
                        {
                            break;
                        }

                        if (Projectile.DestroyOnWalls)
                        {
                            Projectile.WaitingForDispose = true;
                            break;
                        }
                    }
                }
            }

            if (CanMove)
            {
                Projectile.Position = futurePosition;
            }

        }

    }
}
