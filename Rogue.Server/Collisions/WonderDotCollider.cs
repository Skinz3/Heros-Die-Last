using Microsoft.Xna.Framework;
using MonoFramework.Geometry;
using Rogue.Server.World;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Collisions
{
    public class WonderDotCollider : Collider2D
    {
        public WonderDotCollider(ServerObject gameObject) : base(gameObject)
        {

        }

        public override Rectangle CalculateEntityHitBox(Vector2 position)
        {
            return new Rectangle(position.ToPoint(), GameObject.Size)
               .Divide(3, 3, RectangleOrigin.Center);
        }

        public override Rectangle CalculateMovementHitBox(Vector2 position)
        {
            return new Rectangle(position.ToPoint(), GameObject.Size)
                .Divide(3, 3, RectangleOrigin.Center)
                .Divide(1, 1, RectangleOrigin.CenterBottom) // 1,2
                .Scale(0.7f, 0.7f, RectangleOrigin.CenterBottom);
        }

        public override ServerObject CollideEntity(Rectangle futureHitBox)
        {
            var movables = GameObject.GetMapInstance().GetEntities<MovableEntity>();

            foreach (var movable in movables)
            {
                if (movable != GameObject && movable.Collider.EntityHitBox.Intersects(futureHitBox))
                {
                    return movable;
                }
            }
            return null;
        }
    }
}
