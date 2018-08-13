using Microsoft.Xna.Framework;
using MonoFramework.Animations;
using MonoFramework.Collisions;
using MonoFramework.Input;
using MonoFramework.Objects.Abstract;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects.Entities
{
    public abstract class MovableEntity : Entity
    {
        protected Collider2D Collider
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
        public abstract bool Controlable
        {
            get;
        }
        public MovableEntity(ProtocolMovableEntity entity) : base(entity)
        {
            this.Collider = CreateCollider();
            this.MovementEngine = new MovementEngine(Collider, this, 2.5f);
            this.Animator = new Animator(entity.Animations);
        }

        public abstract Collider2D CreateCollider();

        public override void OnDraw(GameTime time)
        {
            Animator.Draw(time, this);
        }

        public override void OnInitialize()
        {

        }
        public override void Dispose()
        {
            Animator.Dispose();
            base.Dispose();
        }
        public override void OnInitializeComplete()
        {
            Animator.Initialize();
        }

        public override void OnUpdate(GameTime time)
        {
            if (Controlable)
                MovementEngine.UpdateInputs(time);
            Animator.Update(time, this);
        }
    }
}
