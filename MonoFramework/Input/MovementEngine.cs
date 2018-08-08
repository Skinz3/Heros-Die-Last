using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Collisions;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Input
{
    public class MovementEngine
    {
        private Collider2D Collider
        {
            get;
            set;
        }
        private PositionableObject Target
        {
            get;
            set;
        }
        public float Speed
        {
            get;
            set;
        }
        public MovementEngine(Collider2D collider, PositionableObject target, float speed)
        {
            this.Collider = collider;
            this.Target = target;
            this.Speed = speed;
        }
        public void Update(GameTime time)
        {
            DirectionEnum direction = DirectionEnum.None;

            Vector2 input = new Vector2();
            if (Keyboard.GetState().IsKeyDown(Keys.Z)) // ! Pouvoir configurer les touches !
            {
                input.Y -= 1;
                direction = DirectionEnum.Up;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                input.Y += 1;
                direction = DirectionEnum.Down;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                input.X += 1;
                direction = DirectionEnum.Right;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                input.X -= 1;
                direction = DirectionEnum.Left;
            }

            if (input != new Vector2(0, 0))
            {
                input.Normalize();
                input = input * new Vector2(Speed);

                var newPosition = Target.Position + input;

                if (Collider.CanMove(newPosition, direction))
                {
                    Target.Position = newPosition;
                }
                /*  else
                  {
                       On ne peut pas bouger ! 
                  } */

            }
        }
    }
}
