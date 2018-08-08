using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework;
using MonoFramework.Collisions;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
using Rogue.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Objects
{
    public class TestObject : AnimableObject
    {
        public float speed;

        private Collider2D Collider
        {
            get;
            set;
        }
        public TestObject(Vector2 position, Point size, string[] spriteNames, float delay, bool loop) : base(position, size, spriteNames, delay, loop)
        {
            this.Collider = new PlayerCollider(this, (SceneManager.CurrentScene as TestScene).map);
        }

        public override void OnInitialize()
        {
            speed = 3f;
        }
        public override void OnDraw(GameTime time)
        {
            Debug.DrawRectangle(Collider.HitBox, Color.Red);

     

            base.OnDraw(time);
        }
        public override void OnUpdate(GameTime time)
        {
            DirectionEnum direction = DirectionEnum.None;

            Vector2 input = new Vector2();
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
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
                input = input * new Vector2(speed);

                var newPosition = Position + input;

                if (Collider.CanMove(newPosition, direction))
                {
                    Position = newPosition;
                }
                else
                {
                    Console.WriteLine("Cannot move");
                }

            }
            base.OnUpdate(time);
        }
    }
}
