using Microsoft.Xna.Framework;
using MonoFramework.Collisions;
using MonoFramework.Objects.Abstract;
using MonoFramework.Objects.Entities;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Animations
{
    public class Animator
    {
        private Dictionary<DirectionEnum, Animation> Animations
        {
            get;
            set;
        }
        public Animator(DirectionalAnimation[] animations)
        {
            Animations = new Dictionary<DirectionEnum, Animation>();
            foreach (var animation in animations)
            {
                Animations.Add(animation.Direction, new Animation(animation.Sprites, animation.Delay, true, animation.FlipVertically, animation.FlipHorizontally));
            }
        }

        public void Update(GameTime time, MovableEntity entity)
        {
            if (Animations.ContainsKey(entity.MovementEngine.Direction))
                Animations[entity.MovementEngine.Direction].Update(time);
        }

        public void Initialize()
        {
            foreach (var animation in Animations.Values)
            {
                animation.Initialize();
            }
        }

        public void Draw(GameTime time, MovableEntity entity)
        {
            if (Animations.ContainsKey(entity.MovementEngine.Direction))
                Animations[entity.MovementEngine.Direction].Draw(entity.Rectangle, entity.Color);
        }

        public void Dispose()
        {
            foreach (var animation in Animations.Values)
            {
                animation.Dispose();
            }
        }
    }
}
