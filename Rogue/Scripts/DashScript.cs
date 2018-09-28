﻿using Microsoft.Xna.Framework;
using MonoFramework;
using MonoFramework.Collisions;
using MonoFramework.Geometry;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
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
        private GMap Map
        {
            get
            {
                return SceneManager.GetCurrentScene<MapScene>().Map;
            }
        }
        private DirectionEnum Direction
        {
            get;
            set;
        }
        public DashScript(float dashSpeed, int distance,DirectionEnum direction)
        {
            this.DashSpeed = dashSpeed;
            this.Distance = distance;
            this.Direction = direction;
        }

        public void Initialize(GameObject target)
        {
            this.Target = (MovableEntity)target;
            this.InitialPosition = Target.Rectangle.Center.ToVector2();
            this.Target.State = EntityStateEnum.DASHING;
          //  this.Target.MovementEngine.Direction = Direction;// (TargetPosition - InitialPosition).GetDirection();
            this.TargetPosition = InitialPosition + (Target.MovementEngine.Direction.GetInputVector() * Distance);

        }
        private void OnEnd()
        {
            Target.GetScript<EntityInterpolationScript>()?.Restore(Target.Position, Direction);
            Target.RemoveScript(this);
            Target.State = EntityStateEnum.MOVING;
        }
        public void Update(GameTime time)
        {
            Time += Debug.TargetElapsedTime.Milliseconds / time.ElapsedGameTime.Milliseconds;

            var distance = (TargetPosition - InitialPosition).Length();

            Ratio = (DashSpeed * Time) / distance;

            if (Ended)
            {
                OnEnd();
            }
            else
            {
                var newPosition = Vector2.Lerp(InitialPosition, TargetPosition, Ratio) - (Target.Rectangle.Size / new Point(2)).ToVector2();
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
