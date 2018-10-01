using System;
using Microsoft.Xna.Framework;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
using MonoFramework.Sprites;
using Rogue.Collisions;
using Rogue.Network;
using Rogue.Objects.Entities;
using Rogue.Protocol.Enums;
using Rogue.Scenes;
using Rogue.World.Maps;

namespace Rogue.World.Entities.Projectiles
{
    public abstract class Projectile : ColorableObject
    {
        public const float PROJECTILE_SPRITE_ROTATON_OFFSET = 45.0f;

        protected abstract string SpriteName
        {
            get;
        }

        public abstract float Speed
        {
            get;
        }

        protected abstract float Offset
        {
            get;
        }

        public Vector2 Direction
        {
            get;
            set;
        }
        public GMap Map
        {
            get
            {
                return SceneManager.GetCurrentScene<MapScene>().Map;
            }
        }
        private Sprite Sprite
        {
            get;
            set;
        }

        public MovableEntity Owner
        {
            get;
            private set;
        }
        protected ProjectileCollider2D Collider
        {
            get;
            private set;
        }
        public abstract CollisionMask CollisionMask
        {
            get;
        }

        public abstract bool CrossEntities
        {
            get;
        }
        public abstract bool DestroyOnWalls
        {
            get;
        }
        public int Id
        {
            get;
            private set;
        }
        protected void UpdateRotation()
        {
            var angle = Math.Abs(MathHelper.ToDegrees((float)Math.Atan2(Direction.X, Direction.Y)) - 180); // Value between -180 and 180 transformed to 0 to 360
            angle += PROJECTILE_SPRITE_ROTATON_OFFSET; // Due to sprite orientation
            Rotation = angle;
        }
        protected Projectile(int id, Vector2 position, Point size, Color color, Vector2 direction, MovableEntity owner) : base(position, size, color)
        {
            this.Direction = direction;
            this.Owner = owner;
            this.Id = id;
        }

        public override void OnInitialize()
        {
            this.Sprite = SpriteManager.GetSprite(SpriteName);
            this.Collider = new ProjectileCollider2D(this);
            this.Position += Direction * Offset;
            this.UpdateRotation();
        }

        public override void OnUpdate(GameTime time)
        {
            Collider.UpdateMovements(time);
        }

        public override void OnDraw(GameTime time)
        {
            Sprite.Draw(Rectangle, Color, Rotation);
        }

        public abstract void OnCollide(GameObject obj, CollisionMask mask);
    }
}