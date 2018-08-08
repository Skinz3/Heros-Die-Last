using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
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
        public TestObject(Vector2 position, Point size, string[] spriteNames, float delay, bool loop) : base(position, size, spriteNames, delay, loop)
        {
        }

        public override void OnInitialize()
        {
            speed = 3f;
        }
        public override void OnUpdate(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                Position.Y -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position.Y += speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position.X += speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Position.X -= speed;
            }
            base.OnUpdate(time);
        }
    }
}
