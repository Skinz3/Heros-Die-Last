using Microsoft.Xna.Framework;
using Rogue.Core.Collisions;
using Rogue.Core.Input;
using Rogue.Core.Objects;
using Rogue.Core.Scenes;
using Rogue.Animations;
using Rogue.Collisions;
using Rogue.Inputs;
using Rogue.Objects.UI;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Types;
using Rogue.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core.DesignPattern;

namespace Rogue.Objects.Entities
{
    public abstract class MovableEntity : Entity
    {
        public Collider2D Collider
        {
            get;
            private set;
        }
        public MovementEngine MovementEngine
        {
            get;
            private set;
        }
        public Animator Animator
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
        public Stats Stats
        {
            get;
            set;
        }
        public virtual bool Controlable
        {
            get
            {
                return false;
            }
        }
        public virtual bool CanMove
        {
            get
            {
                return !Dashing;
            }
        }
        public virtual bool UseInterpolation
        {
            get
            {
                return true;
            }
        }
        protected GCircle Aura
        {
            get;
            private set;
        }
        [InDeveloppement]
        private EntityInformations EntityInformations
        {
            get;
            set;
        }
        public void InflictDamage(Entity source, int amount)
        {

            var obj = new DamageText(this, amount);
            obj.Initialize();
            SceneManager.CurrentScene.AddObject(obj, LayerEnum.First);
            Stats.LifePoints -= amount;

            if (Stats.LifePoints <= 0)
                OnDead(source);
        }


        public void DefineAura(Color color, float radius, float sharpness)
        {
            Aura = new GCircle(new Vector2(), radius, color, sharpness);
            Aura.Initialize();
        }
        public void RemoveAura()
        {
            Aura = null;
        }
        protected virtual void OnDead(Entity source)
        {

        }

        public MovableEntity(ProtocolMovableEntity entity) : base(entity)
        {
            this.Collider = CreateCollider();
            this.Stats = entity.Stats;
            this.MovementEngine = new MovementEngine(Collider, this, entity.Stats.Speed);
            this.Animator = new Animator(entity.Animations, entity.IdleAnimation, entity.MovementAnimation);
            this.Animator.CurrentAnimation = "moving";

            if (entity.Aura != null)
            {
                DefineAura(entity.Aura.Color, entity.Aura.Radius, entity.Aura.Sharpness);
            }
        }

        public virtual void OnPositionReceived(Vector2 position, DirectionEnum direction, float mouseRotation)
        {
            if (EntityInterpolationScript.UseInterpolation)
            {
                GetScript<EntityInterpolationScript>().OnPositionReceived(position, direction);
            }
            else
            {
                if (!Dashing)
                {
                     Animator.SetMovementAnimation();
                    Position = position;
                    MovementEngine.Direction = direction;
                }
            }

        }
        public abstract Collider2D CreateCollider();

        public override void OnDraw(GameTime time)
        {
            Animator.Draw(time, this);

            if (Aura != null)
            {
                Aura.Draw(time);
            }
            EntityInformations.Draw(time);
        }
        public void Dash(float speed, int distance, DirectionEnum direction, string animation)
        {
            if (Dashing)
            {
                RemoveScript(GetScript<DashScript>());
            }

            this.AddScript(new DashScript(speed, distance, direction, animation));
        }

        public Vector2 GetCellPosition(GCell cell)
        {
            return cell.GetCenterPosition(Size.X, Size.Y);
        }

        public override void OnInitialize()
        {
            EntityInformations = new EntityInformations(this);
            EntityInformations.Initialize();
        }
        public override void Dispose()
        {
            Animator.Dispose();
            base.Dispose();
        }
        public override void OnInitializeComplete()
        {
            if (UseInterpolation)
            {
                AddScript(new EntityInterpolationScript());
            }
            Animator.Initialize();
        }
        public override GCell GetCell()
        {
            return Collider.CurrentCell;
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            EntityInformations.Update(time);
            Collider.Update();

            if (Aura != null)
            {
                Aura.Position = this.Center - new Vector2(Aura.Radius / 2, Aura.Radius / 2);
            }
            if (Controlable)
                MovementEngine.UpdateInputs(time);
            Animator.Update(time, this);
        }

    }
}
