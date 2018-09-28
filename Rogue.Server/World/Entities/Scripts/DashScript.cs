using Microsoft.Xna.Framework;
using MonoFramework.Collisions;
using MonoFramework.DesignPattern;
using MonoFramework.Geometry;
using MonoFramework.Utils;
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
        private Vector2 TargetPosition
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
        public DashScript(Vector2 targetPosition, float dashSpeed, int distance)
        {
            this.TargetPosition = targetPosition;
            this.DashSpeed = dashSpeed;
            this.Distance = distance;
            this.Collides = new List<Entity>();
        }

        public void Initialize(Entity target)
        {
            this.Target = (MovableEntity)target;
            this.InitialPosition = Target.Rectangle.Center.ToVector2();
            this.Target.State = EntityStateEnum.DASHING;
            //  this.Target.Color = new Color(Color.MediumVioletRed, 0.2f);
            //    this.Target.Direction = (TargetPosition - InitialPosition).GetDirection();
            this.TargetPosition = InitialPosition + (Target.Direction.GetInputVector() * Distance);

        }
        private void OnEnd()
        {
            Target.RemoveScript(this);
            Target.State = EntityStateEnum.MOVING;
        }
        public void Update(long deltaTime)
        {
            Time += MapInstance.REFRESH_RATE / deltaTime;

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

                    var target = Target.Collider.CollideEntity(newPosition) as MovableEntity;

                    if (target != null && !Collides.Contains(target))
                    {
                        Collides.Add(target);
                        target.InflictDamage(Target, (int)(Ratio * 1500));
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
