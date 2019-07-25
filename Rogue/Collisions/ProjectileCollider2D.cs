﻿using Microsoft.Xna.Framework;
using Rogue.Core;
using Rogue.Core.Geometry;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Scenes;
using Rogue.Network;
using Rogue.Objects.Entities;
using Rogue.Protocol.Enums;
using Rogue.World.Projectiles;
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
        public bool CanMove
        {
            get;
            set;
        }
        public ProjectileCollider2D(Projectile projectile)
        {
            this.Projectile = projectile;
        }
        public void UpdateMovements(GameTime time)
        {
            var oldCan = this.CanMove;

            this.CanMove = true;

            var futurePosition = Projectile.Position + Projectile.Direction * Projectile.Speed;

            var futureCenter = Projectile.Center + Projectile.Direction * Projectile.Speed;

            //var rect = new Rectangle(futurePosition.ToPoint(), Projectile.Size);

            if (Projectile.CollisionMask.HasFlag(CollisionMask.WALL))
            {
                foreach (var wall in Projectile.Map.Cells)
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
                            ClientHost.Client.Player.MapInstance.RemoveProjectile(Projectile.Id);
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
