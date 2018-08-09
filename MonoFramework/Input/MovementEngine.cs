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
        public event Action<DirectionEnum> OnDirectionChanged;

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
        public DirectionEnum Direction
        {
            get;
            private set;
        }
        public MovementEngine(Collider2D collider, PositionableObject target, float speed)
        {
            this.Collider = collider;
            this.Target = target;
            this.Speed = speed;
        }
        public void Update(GameTime time)
        {
            DirectionEnum oldDirection = Direction;

            Direction = DirectionEnum.None;

            Vector2 input = new Vector2();
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Z)) // ! Pouvoir configurer les touches !
            {
                input.Y -= 1;
                Direction |= DirectionEnum.Up;
            }
            if (state.IsKeyDown(Keys.S))
            {
                input.Y += 1;
                Direction |= DirectionEnum.Down;
            }
            if (state.IsKeyDown(Keys.D))
            {
                input.X += 1;
                Direction |= DirectionEnum.Right;
            }
            if (state.IsKeyDown(Keys.Q))
            {
                input.X -= 1;
                Direction |= DirectionEnum.Left;
            }

            if (Direction != oldDirection)
            {
                OnDirectionChanged?.Invoke(Direction);
            }

            if (input != new Vector2(0, 0))
            {
                input.Normalize();
                input = input * new Vector2(Speed);

                var newPosition = Target.Position + input;

                if (Collider.CanMove(newPosition, Direction))
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
