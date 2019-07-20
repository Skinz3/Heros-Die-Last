using Microsoft.Xna.Framework;
using Rogue.Core.Animations;
using Rogue.Core.Collisions;
using Rogue.Collisions;
using Rogue.Objects.Entities;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core.Geometry;

namespace Rogue.Animations
{
    public class Animator
    {
        private Dictionary<string, Dictionary<DirectionEnum, Animation>> Animations
        {
            get;
            set;
        }
        public string CurrentAnimation
        {
            get;
            set;
        }
        private string IdleAnimation
        {
            get;
            set;
        }
        private string MovementAnimation
        {
            get;
            set;
        }
        public Animator(string[] animations, string idleAnimation, string movementAnimation)
        {
            Animations = new Dictionary<string, Dictionary<DirectionEnum, Animation>>();
            IdleAnimation = idleAnimation;
            MovementAnimation = movementAnimation;
            Animations = AnimationManager.GetAnimations(animations);
        }
        public void Update(GameTime time, MovableEntity entity)
        {
            DirectionEnum direction = entity.MovementEngine.Direction.Restrict4Direction();

            if (Animations.ContainsKey(CurrentAnimation))
            {
                if (Animations[CurrentAnimation].ContainsKey(direction))
                {
                    Animations[CurrentAnimation][direction].Update(time);
                }

            }
        }

        public void Initialize()
        {
            foreach (var dic in Animations.Values)
            {
                foreach (var animation in dic.Values)
                {
                    animation.Initialize();
                }
            }
        }

        public void Draw(GameTime time, MovableEntity entity)
        {
            DirectionEnum direction = entity.MovementEngine.Direction.Restrict4Direction();

            if (Animations.ContainsKey(CurrentAnimation))
            {
                if (Animations[CurrentAnimation].ContainsKey(direction))
                    Animations[CurrentAnimation][direction].Draw(entity.Rectangle, entity.Color);
            }
        }

        public void SetMovementAnimation()
        {
            CurrentAnimation = MovementAnimation;
        }
        public void SetIdleAnimation() // check aiming?
        {
            CurrentAnimation = IdleAnimation;
        }
        public void Dispose()
        {
            foreach (var dic in Animations.Values)
            {
                foreach (var animation in dic.Values)
                {
                    animation.Dispose();
                }
            }
        }
    }
}
