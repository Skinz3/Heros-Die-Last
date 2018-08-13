using Microsoft.Xna.Framework;
using MonoFramework.Collisions;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoFramework.Geometry;
using MonoFramework;
using System.Threading.Tasks;

namespace Rogue.Collisions
{
    public class PlayerCollider : Collider2D
    {
        public PlayerCollider(PositionableObject gameObject) : base(gameObject)
        {

        }

        public override Rectangle CalculateHitBox(Vector2 position)
        {
            return new Rectangle(position.ToPoint(), GameObject.Size)
                .Divide(3, 3, RectangleOrigin.Center)
                .Divide(1, 2, RectangleOrigin.CenterBottom)
                .Scale(0.7f, 0.7f, RectangleOrigin.CenterBottom);
        }
    }
}
