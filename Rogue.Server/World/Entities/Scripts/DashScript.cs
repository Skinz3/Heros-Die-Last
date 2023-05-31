using Microsoft.Xna.Framework;
using Rogue.Core.Collisions;
using Rogue.Core.DesignPattern;
using Rogue.Core.Geometry;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Utils;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Entities.Scripts
{
    public class DashScript : IScript
    {
        private Vector2 InitialPosition
        {
            get;
            set;
        }
        public Vector2 TargetPosition
        {
            get;
            private set;
        }
        private Vector2 Direction
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
        private MapGrid Map
        {
            get
            {
                return Target.MapInstance.Record.Grid;
            }
        }
        private List<Entity> Collides
        {
            get;
            set;
        }
        private Action OnDashEnd
        {
            get;
            set;
        }
        private Action<Entity> OnCollideEntity
        {
            get;
            set;
        }
        public DashScript(Vector2 direction, float dashSpeed, int distance, Action<Entity> onCollideEntity, Action onDashEnd)
        {
            this.Direction = direction;
            this.DashSpeed = dashSpeed;
            this.Distance = distance;
            this.Collides = new List<Entity>();
            this.OnDashEnd = onDashEnd;
            this.OnCollideEntity = onCollideEntity;
        }

        public void Initialize(Entity target)
        {
            this.Target = (MovableEntity)target;
            this.InitialPosition = Target.Center;
            this.Target.State = EntityStateEnum.DASHING;
            this.TargetPosition = InitialPosition + (Direction * Distance);
        }
        private void OnEnd()
        {
            Target.RemoveScript(this);
            Target.State = EntityStateEnum.MOVING;
            OnDashEnd?.Invoke();
        }
        public void Update(long deltaTime)
        {
            Time += deltaTime / MapInstance.REFRESH_RATE;

            var distance = (TargetPosition - InitialPosition).Length();

            Ratio = (DashSpeed * Time) / distance;

            if (Ended)
            {
                OnEnd();
            }
            else
            {
                var newPosition = Vector2.Lerp(InitialPosition, TargetPosition, Ratio) - (Target.Rectangle.Size / new Point(2)).ToVector2();
                if (Target.Collider.CanMove(newPosition, Target.Direction, false) == null)
                {
                    Target.Position = newPosition;

                    var target = Target.Collider.CollideEntity(newPosition) as Entity;

                    if (target != null && !Collides.Contains(target))
                    {
                        Collides.Add(target);
                        OnCollideEntity?.Invoke(target);
                    }
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
