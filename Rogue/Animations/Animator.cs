using Microsoft.Xna.Framework;
using MonoFramework.Animations;
using MonoFramework.Collisions;
using Rogue.Collisions;
using Rogue.Objects.Entities;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Animations
{
    public class Animator
    {
        private Dictionary<EntityStateEnum, Dictionary<DirectionEnum, Animation>> Animations
        {
            get;
            set;
        }
        public Animator(StateAnimations[] animations)
        {
            Animations = new Dictionary<EntityStateEnum, Dictionary<DirectionEnum, Animation>>();

            foreach (var animation in animations)
            {
                Animations.Add(animation.State, GetDirectionableAnimationsDictionary(animation.Animations));
            }
        }
        private Dictionary<DirectionEnum, Animation> GetDirectionableAnimationsDictionary(DirectionalAnimation[] animations)
        {
            Dictionary<DirectionEnum, Animation> result = new Dictionary<DirectionEnum, Animation>();

            foreach (var animation in animations)
            {
                result.Add(animation.Direction, AnimationManager.GetAnimation(animation.AnimationEnum));
            }

            return result;
        }
        public void Update(GameTime time, MovableEntity entity)
        {
            if (Animations.ContainsKey(entity.State))
            {
                if (Animations[entity.State].ContainsKey(entity.MovementEngine.Direction))
                {
                    Animations[entity.State][entity.MovementEngine.Direction].Update(time);
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
            if (Animations.ContainsKey(entity.State))
            {
                if (Animations[entity.State].ContainsKey(entity.MovementEngine.Direction))
                    Animations[entity.State][entity.MovementEngine.Direction].Draw(entity.Rectangle, entity.Color);
            }
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
