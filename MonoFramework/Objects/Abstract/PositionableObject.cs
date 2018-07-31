using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects.Abstract
{
    public abstract class PositionableObject : GameObject
    {
        public Vector2 Position;

        public Point Size;


        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(Position.ToPoint(), Size);
            }
        }
        public PositionableObject(Vector2 position,Point size)
        {
            this.Position = position;
            this.Size = size;
        }
    }
}
