using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework;
using MonoFramework.Collisions;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
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
        public Rectangle HitBox
        {
            get
            {
                return Rectangle.Divide(3, 3).Divide(1, 2);
            }
        }
        public GCell CurrentCell
        {
            get
            {
                return (SceneManager.CurrentScene as TestScene).map.GetCell(HitBox.GetCollidePointForDownDirection());
            }
        }
        public TestObject(Vector2 position, Point size, string[] spriteNames, float delay, bool loop) : base(position, size, spriteNames, delay, loop)
        {
        }

        public override void OnInitialize()
        {
            speed = 3f;
        }
        public override void OnDraw(GameTime time)
        {
            Debug.DrawRectangle(Rectangle, Color.LightGreen);
            Debug.DrawRectangle(HitBox, Color.Red);

            base.OnDraw(time);
        }
        public override void OnUpdate(GameTime time)
        {
            DirectionEnum direction = DirectionEnum.None;


            Vector2 input = new Vector2();
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                input.Y -= speed;
                direction = DirectionEnum.Up;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                input.Y += speed;
                direction = DirectionEnum.Down;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                input.X += speed;
                direction = DirectionEnum.Right;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                input.X -= speed;
                direction = DirectionEnum.Left;
            }

            var map = (SceneManager.CurrentScene as TestScene).map;

            var newPosition = Position + input;

            if (CurrentCell != null)
            {
                var nextCell = CurrentCell.GetNextCells(map, direction, 1)[0];
             //   CurrentCell.FillColor = Color.MediumPurple;

                Position = newPosition;
            }
            else
            {
                Console.WriteLine("Cell is null.");
                Position = newPosition;
            }
           

            base.OnUpdate(time);
        }
    }
}
