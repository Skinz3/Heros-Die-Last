using Microsoft.Xna.Framework;
using MonoFramework.Animations;
using MonoFramework.Collisions;
using MonoFramework.Input;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects.Entities
{
    public abstract class MovableEntity : ColorableObject
    {
        private Collider2D Collider
        {
            get;
            set;
        }
        public MovementEngine MovementEngine
        {
            get;
            private set;
        }
        private Animator Animator
        {
            get;
            set;
        }

        public MovableEntity(Vector2 position, GMap map, Point size, Animator animator) : base(position, size, Color.White)
        {
            this.Collider = CreateCollider(map);
            this.MovementEngine = new MovementEngine(Collider, this, 2.5f);
            this.Animator = animator;
        }

        public abstract Collider2D CreateCollider(GMap map);

        public override void OnDraw(GameTime time)
        {
            Animator.Draw(time, this);
        }

        public override void OnInitialize()
        {

        }

        public override void OnInitializeComplete()
        {
            Animator.Initialize();
        }

        public override void OnUpdate(GameTime time)
        {
            MovementEngine.Update(time);
            Animator.Update(time, this);
        }
    }
}
