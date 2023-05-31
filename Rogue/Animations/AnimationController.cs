using Microsoft.Xna.Framework;
using Rogue.Core.Collisions;
using Rogue.Objects;
using Rogue.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Animations
{
    class AnimationController
    {
        public static void OnMoveUpdated(Vector2 input, MovableEntity entity)
        {
            if (input == new Vector2(0,0))
            {
                entity.Animator.SetIdleAnimation();
            }
            else
            {
                entity.Animator.SetMovementAnimation();
            }

            if (entity is Player && (entity as Player).HoldingWeapon)
            {
                entity.Animator.CurrentAnimation += "Holding";
            }
        }

        public static void OnDashEnd(MovableEntity target)
        {
            target.Animator.SetMovementAnimation();

            if (target is Player && (target as Player).HoldingWeapon)
            {
                target.Animator.CurrentAnimation += "Holding";
            }
        }
    }
}
