using LiteNetLib;
using Microsoft.Xna.Framework;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;
using Rogue.Protocol.Types;
using Rogue.Server.Collisions;
using Rogue.Server.World.Entities.Scripts;
using Rogue.Server.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Entities
{
    public abstract class MovableEntity : Entity
    {
        public Stats Stats
        {
            get;
            private set;
        }
        public Rogue.Core.Collisions.DirectionEnum Direction
        {
            get;
            set;
        }
        public Collider2D Collider
        {
            get;
            private set;
        }
        public bool Dashing
        {
            get
            {
                return State == EntityStateEnum.DASHING;
            }
        }
        public EntityStateEnum State
        {
            get;
            set;
        }

        protected ProtocolEntityAura Aura
        {
            get;
            private set;
        }
        public Vector2 GetCellPosition(MapCell cell)
        {
            return cell.GetCenterPosition(Size.X, Size.Y);
        }
        public MovableEntity(Vector2 position, Point size, Stats stats) : base(position, size)
        {
            this.Collider = CreateCollider();
            this.Stats = stats;
        }
        public void Dash(Vector2 position, float speed, int distance, string animation)
        {
            AddScript(new DashScript(position, speed, distance));
            MapInstance.Send(new DashMessage(Id, speed, Direction, distance, animation));
        }
        public void DefineAura(Color color, float radius, float sharpness)
        {
            this.Aura = new ProtocolEntityAura(color, radius, sharpness);
            MapInstance.Send(new DefineEntityAuraMessage(Id, Aura));
        }
        public override void Update(long deltaTime)
        {
            Collider.Update();
            base.Update(deltaTime);
        }
        public void SendPosition(float mr)
        {
            this.MapInstance.Send(new EntityDispositionMessage(Id, Position, Direction, mr), Id, SendOptions.Sequenced);
        }
        public void InflictDamage(Entity source, int value)
        {
            Stats.LifePoints -= value;
            MapInstance.Send(new InflictDamageMessage(Id, value));

            if (Stats.LifePoints <= 0)
                OnDead(source);
        }
        protected virtual void OnDead(Entity source)
        {
            WaitingForDispose = true;
        }
        public override MapCell GetCell()
        {
            return Collider.CurrentCell;
        }
        public abstract Collider2D CreateCollider();
    }
}
