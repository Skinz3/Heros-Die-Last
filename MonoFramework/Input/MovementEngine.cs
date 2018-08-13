using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Collisions;
using MonoFramework.DesignPattern;
using MonoFramework.Geometry;
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
            set; // private?
        }
        public bool CanMove
        {
            get;
            set;
        }
        public MovementEngine(Collider2D collider, PositionableObject target, float speed)
        {
            this.Collider = collider;
            this.Target = target;
            this.Speed = speed;
            this.CanMove = true;
        }
        public void Move(Vector2 input)
        {
            if (!CanMove)
                return;

            DirectionEnum oldDirection = Direction;

            Direction = input.GetDirection();

            if (Direction == (DirectionEnum.Right | DirectionEnum.Left) || Direction == (DirectionEnum.Up | DirectionEnum.Down))
            {
                Direction = DirectionEnum.None;
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
        [InDeveloppement(InDeveloppementState.THINK_ABOUT_IT, "(btw bad spelling DevEloppment), Configure key, architecture problem with networking?")]
        public void UpdateInputs(GameTime time)
        {
            Vector2 input = new Vector2();
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Z)) // ! Pouvoir configurer les touches !
            {
                input.Y -= 1;
            }
            if (state.IsKeyDown(Keys.S))
            {
                input.Y += 1;
            }
            if (state.IsKeyDown(Keys.D))
            {
                input.X += 1;
            }
            if (state.IsKeyDown(Keys.Q))
            {
                input.X -= 1;
            }
            Move(input);
        }
    }
}
