using Microsoft.Xna.Framework;
using Rogue.Core;
using Rogue.Core.Geometry;
using Rogue.Core.IO.Maps;
using Rogue.Core.Objects;
using Rogue.Core.Objects.Abstract;
using Rogue.Objects.Entities;
using Rogue.Protocol.Enums;
using System;

namespace Rogue.World.Projectiles
{
    public class ReflectionProjectile : Projectile
    {
        public override CollisionMask CollisionMask => CollisionMask.ENTITIES | CollisionMask.WALL;

        public override bool DestroyOnWalls => false;

        public ReflectionProjectile(int id, Vector2 position, int size, float speed,
            Color color, Vector2 direction, MovableEntity owner, string animationName) :
            base(id, position, size, speed, color, direction, owner, animationName)
        {
            if (size > MapTemplate.MAP_CELL_SIZE / 2)
            {
                throw new Exception("Unable to create the reflection projectile! its too big");
            }
        }
        public override void OnInitializeComplete()
        {
        }

        public override void OnDispose()
        {

        }

        public override bool OnCollide(PositionableObject obj, CollisionMask mask)
        {
            if (mask == CollisionMask.WALL)
            {
                var objDir = (obj.Center - this.Center);
                objDir.Normalize();

                Direction = GeometryExtensions.ReflectVector(Direction, Center, obj.Center);

                UpdateRotation();
                return true;
            }

            return false;
        }
        public override void OnDraw(GameTime time)
        {
            base.OnDraw(time);
        }

     
    }
}
