using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Core.Geometry;
using Rogue.Core.IO.Maps;
using Rogue.Protocol.Enums;
using Rogue.Server.World.Entities;
using Rogue.Server.World.Maps;

namespace Rogue.Server.World.Projectiles
{
    public class ReflectionProjectile : Projectile
    {
        public override CollisionMask CollisionMask => CollisionMask.ENTITIES | CollisionMask.WALL;
        public override bool CrossEntities => false;
        public override bool DestroyOnWalls => true;


        public int BoundsCount
        {
            get;
            set;
        }

        public override ProjectileTypeEnum Type => ProjectileTypeEnum.Reflection;

        public ReflectionProjectile(int id, Vector2 position, int size, float speed,
             Vector2 direction, MovableEntity owner, string animationName, float lifeTime) :
             base(id, position, size, speed, direction, owner, animationName, lifeTime)
        {
            if (size > MapTemplate.MAP_CELL_SIZE / 2)
            {
                throw new Exception("Unable to create the reflection projectile! its too big");
            }
        }


        public override bool OnCollide(ServerObject obj, CollisionMask mask)
        {
            if (mask == CollisionMask.ENTITIES)
            {
                if (obj is MovableEntity)
                {
                    var movableEntity = (MovableEntity)obj;

                    if (BoundsCount > 0) // put this in an action
                        movableEntity.InflictDamage(Owner, (int)((BoundsCount + 1) * 800));
                    return false;
                }
            }

            if (mask == CollisionMask.WALL)
            {
                var objDir = (obj.Center - this.Center);
                objDir.Normalize();

                Direction = GeometryExtensions.ReflectVector(Direction, Center, obj.Center);

                BoundsCount++;

                return true;
            }

            return false;
        }

        public override MapInstance GetMapInstance()
        {
            return MapInstance;
        }

        public override MapCell GetCell()
        {
            return (MapCell)MapInstance.Record.Grid.GetCell(Center);
        }


    }
}
