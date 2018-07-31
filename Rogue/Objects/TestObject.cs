using Microsoft.Xna.Framework;
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

        }
        public override void OnUpdate(GameTime time)
        {
            Position.Y += speed;
            base.OnUpdate(time);
        }
    }
}
