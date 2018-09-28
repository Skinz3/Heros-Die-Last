using Microsoft.Xna.Framework;
using MonoFramework.Collisions;
using MonoFramework.Input;
using MonoFramework.Objects;
using MonoFramework.Scenes;
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
        protected Animator Animator
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

        public void InflictDamage(Entity source, int amount)
        {

            var obj = new DamageText(this, amount);
            obj.Initialize();
            SceneManager.CurrentScene.AddObject(obj, LayerEnum.First);
            Stats.LifePoints -= amount;

            if (Stats.LifePoints <= 0)
                OnDead(source);
        }



        protected virtual void OnDead(Entity source)
        {

        }

        public MovableEntity(ProtocolMovableEntity entity) : base(entity)
        {
            this.Collider = CreateCollider();
            this.Stats = entity.Stats;
            this.MovementEngine = new MovementEngine(Collider, this, 2.5f);
            this.Animator = new Animator(entity.Animations);
        }

        public virtual void OnPositionReceived(Vector2 position, DirectionEnum direction)
        {
            if (EntityInterpolationScript.UseInterpolation)
            {
                GetScript<EntityInterpolationScript>().OnPositionReceived(position, direction);
            }
            else
            {
                if (!Dashing)
                {
                    Position = position;
                    MovementEngine.Direction = direction;
                }
            }

        }
        public abstract Collider2D CreateCollider();

        public override void OnDraw(GameTime time)
        {
            Animator.Draw(time, this);
        }
        public void Dash(float speed, int distance, DirectionEnum direction)
        {
            if (Dashing)
            {
                RemoveScript(GetScript<DashScript>());
            }

            this.AddScript(new DashScript(speed, distance, direction));
        }

        public Vector2 GetCellPosition(GCell cell)
        {
            return cell.GetCenterPosition(Size.X, Size.Y);
        }

        public override void OnInitialize()
        {
            AddChild(new EntityInformations());
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

            Collider.Update();

            if (Controlable)
                MovementEngine.UpdateInputs(time);
            Animator.Update(time, this);
        }

    }
}
