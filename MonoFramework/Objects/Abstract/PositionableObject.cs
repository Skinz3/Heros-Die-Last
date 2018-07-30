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

        public PositionableObject(Vector2 position)
        {
            this.Position = position;
        }
        public override void OnInitializeComplete()
        {
           
        }
    }
}
