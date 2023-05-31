using Microsoft.Xna.Framework;
using Rogue.Animations;
using Rogue.Core;
using Rogue.Core.Collisions;
using Rogue.Core.Geometry;
using Rogue.Core.Objects;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Scenes;
using Rogue.Objects;
using Rogue.Objects.Entities;
using Rogue.Protocol.Enums;
using Rogue.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Scripts
{
    public class DashScript : IScript
    {
        private Vector2 InitialPosition
        {
            get;
            set;
        }
        private Vector2 EndPosition
        {
            get;
            set;
        }
        public bool Ended
        {
            get
            {
                return Ratio >= 1;
            }
        }
        private float Time
        {
            get;
            set;
        }
        private float Ratio
        {
            get;
            set;
        }

        private MovableEntity Target
        {
            get;
            set;
        }
        private float DashSpeed
        {
            get;
            set;
        }
        private int Distance
        {
            get;
            set;
        }
        private GMap Map
        {
            get
            {
                return SceneManager.GetCurrentScene<MapScene>().Map;
            }
        }
        private string Animation
        {
            get;
            set;
        }
        public DashScript(float dashSpeed, Vector2 endPosition, string animation)
        {
            this.DashSpeed = dashSpeed;
            this.EndPosition = endPosition;
            this.Animation = animation;
        }

        public void Initialize(GameObject target)
        {
            this.Target = (MovableEntity)target;
            this.InitialPosition = Target.Center;
            this.Target.State = EntityStateEnum.DASHING;

            this.Target.MovementEngine.Direction = (EndPosition - InitialPosition).GetDirection();

            this.Target.Animator.CurrentAnimation = this.Animation;

        }
        private void OnEnd()
        {
            Target.GetScript<EntityInterpolationScript>()?.Restore(Target.Position, Target.MovementEngine.Direction, Target.MouseRotation);
            Target.RemoveScript(this);
            Target.State = EntityStateEnum.MOVING;
            AnimationController.OnDashEnd(Target);
        }
        public void Update(GameTime time)
        {
            Time += Debug.TargetElapsedTime.Milliseconds / time.ElapsedGameTime.Milliseconds;

            var distance = (EndPosition - InitialPosition).Length();

            Ratio = (DashSpeed * Time) / distance;

            if (Ended)
            {
                OnEnd();
            }
            else
            {
                var newPosition = Vector2.Lerp(InitialPosition, EndPosition, Ratio) - (Target.Rectangle.Size / new Point(2)).ToVector2();
                if (Target.Collider.CanMove(newPosition, Target.MovementEngine.Direction, false) == null)
                {
                    Target.Position = newPosition;
                }
                else
                    OnEnd();
            }
        }

        public void Dispose()
        {
            OnEnd();
        }

        public void OnRemove()
        {

        }
    }
}
