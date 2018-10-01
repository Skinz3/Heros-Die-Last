using Microsoft.Xna.Framework;
using MonoFramework;
using MonoFramework.Objects.Abstract;
using Rogue.Objects.Entities;
using Rogue.Protocol.Enums;
using System;

namespace Rogue.World.Entities.Projectiles
{
    public class KunaiProjectile : Projectile
    {
        public KunaiProjectile(int id, Vector2 position, Point size,
            Color color, Vector2 direction, MovableEntity owner) :
            base(id, position, size, color, direction, owner)
        {

        }

        public override void OnInitializeComplete()
        {

        }

        public override void OnDispose()
        {

        }

        public override void OnCollide(GameObject obj, CollisionMask mask)
        {
            if (mask == CollisionMask.ENTITIES)
            {
                if (obj is MovableEntity)
                {
                    var movableEntity = (MovableEntity)obj;
                    movableEntity.InflictDamage(Owner, 100);
                }
            }

            if (mask == CollisionMask.WALL)
            {
                Console.WriteLine("collide");
                Direction = -Direction;
                Direction = new Vector2(Direction.X, -Direction.Y);
                UpdateRotation();
            }
        }
        public override void OnDraw(GameTime time)
        {
            Debug.DrawRectangle(Rectangle, Color.PaleVioletRed);
            base.OnDraw(time);
        }
        protected override string SpriteName => "108_cast";
        public override CollisionMask CollisionMask => CollisionMask.ENTITIES | CollisionMask.WALL;
        public override float Speed => 5.0f;
        public override bool CrossEntities => false;
        protected override float Offset => 25.0f;

        public override bool CrossWalls => true;
    }
}