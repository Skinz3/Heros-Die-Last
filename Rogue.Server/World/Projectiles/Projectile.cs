using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Collisions;
using Rogue.Server.World.Entities;
using Rogue.Server.World.Maps;

namespace Rogue.Server.World.Projectiles
{
    public abstract class Projectile : ServerObject
    {
        protected string AnimationName
        {
            get;
            set;
        }
        public abstract ProjectileTypeEnum Type
        {
            get;
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
        public MapInstance MapInstance
        {
            get;
            private set;
        }

        public Entity Owner
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
        public int SizeF
        {
            get;
            private set;
        }
        protected float LifeTime
        {
            get;
            set;
        }
        protected float LifeTimeMax
        {
            get;
            set;
        }
        protected Projectile(int id, Vector2 position, int size, float speed, Vector2 direction, MovableEntity owner, string animationName, float lifeTime) : base(position, new Point(size, size))
        {
            this.Direction = direction;
            this.SizeF = size;
            this.Owner = owner;
            this.Speed = speed;
            this.Id = id;
            this.AnimationName = animationName;
            this.LifeTime = lifeTime;
            this.LifeTimeMax = lifeTime;
            this.Collider = new ProjectileCollider2D(this);
        }

        public virtual void OnUpdate(long deltaTime)
        {
            if (LifeTime != float.MaxValue)
            {
                LifeTime -= deltaTime;

                if (LifeTime <= 0)
                {
                    WaitingForDispose = true;
                    return;
                }
            }

            Collider.UpdateMovements(deltaTime);
        }

        public override void DefineMapInstance(MapInstance instance)
        {
            this.MapInstance = instance;
            this.MapInstance.AddProjectile(this);
        }
        public override void LeaveMapInstance()
        {
            if (MapInstance != null)
            {
                this.WaitingForDispose = true;
                MapInstance = null;
            }
        }
        public bool Validate()
        {
            var rect = new Rectangle((int)Position.X, (int)Position.Y, SizeF, SizeF);
            var cells = Owner.MapInstance.Record.Grid.GetCells(rect);
            return !cells.Any(x => x.Walkable == false);
        }
        public virtual ProjectileCreateMessage GetProjectileCreateMessage()
        {
            return new ProjectileCreateMessage(Id, Type, Owner.Id, Owner.Center, Speed, AnimationName, Direction, SizeF);
        }

        /// <summary>
        /// bool = did we continue to check collisions?
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public abstract bool OnCollide(ServerObject obj, CollisionMask mask);

        public virtual void Dispose()
        {
            MapInstance = null;
            Collider = null;
            Owner = null;
        }
    }
}
