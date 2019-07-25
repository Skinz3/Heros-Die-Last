using System;
using Microsoft.Xna.Framework;
using Rogue.Core.Objects;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Scenes;
using Rogue.Core.Sprites;
using Rogue.Collisions;
using Rogue.Network;
using Rogue.Objects.Entities;
using Rogue.Protocol.Enums;
using Rogue.Scenes;
using Rogue.World.Maps;
using Rogue.Core.Animations;
using Rogue.Core;
using Rogue.Core.Geometry;

namespace Rogue.World.Projectiles
{
    public abstract class Projectile : ColorableObject
    {
        protected string AnimationName
        {
            get;
            set;
        }

        public float Speed
        {
            get;
            private set;
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
        private Animation Animation
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

        public abstract bool DestroyOnWalls
        {
            get;
        }
        public int Id
        {
            get;
            private set;
        }
        public int SizeF
        {
            get;
            private set;
        }
        protected void UpdateRotation()
        {
            Rotation = Direction.GetAngle();
        }
        protected Projectile(int id, Vector2 position, int size, float speed, Color color, Vector2 direction, MovableEntity owner, string animationName) : base(position, new Point(size, size), color)
        {
            this.Direction = direction;
            this.SizeF = size;
            this.Owner = owner;
            this.Speed = speed;
            this.Id = id;
            this.AnimationName = animationName;
        }

        public override void OnInitialize()
        {
            this.Animation = AnimationManager.GetAnimation(AnimationName);
            this.Collider = new ProjectileCollider2D(this);
            this.UpdateRotation();
        }

        public override void OnUpdate(GameTime time)
        {
            Animation.Update(time);
            Collider.UpdateMovements(time);

        }
       
        public override void OnDraw(GameTime time)
        {
            var rectangle = new Rectangle(Rectangle.Location, Rectangle.Size);

            rectangle.X += Rectangle.Size.X / 2; // due to rotation origin fk this system?...
            rectangle.Y += Rectangle.Size.Y / 2;

            Animation.Draw(rectangle, Color, Rotation, new Vector2(Animation.TheoricalDimensions.X / 2, Animation.TheoricalDimensions.Y / 2));
        }
        public override void Draw(GameTime time)
        {
            base.Draw(time);
        }
        /// <summary>
        /// bool = did we continue to check collisions?
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public abstract bool OnCollide(PositionableObject obj, CollisionMask mask);
    }
}