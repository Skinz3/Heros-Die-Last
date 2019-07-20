using Microsoft.Xna.Framework;
using Rogue.Collisions;
using Rogue.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rogue;
using System.Threading.Tasks;
using Rogue.Network;
using Rogue.Objects.Entities;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Collisions;
using Rogue.Core.Geometry;

namespace Rogue.Collisions
{
    public class WonderDotCollider : Collider2D
    {
        public WonderDotCollider(PositionableObject gameObject) : base(gameObject)
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
                .Divide(1, 2, RectangleOrigin.CenterBottom) // 1,2  or 1,1
                .Scale(0.7f, 0.7f, RectangleOrigin.CenterBottom);
        }

        public override GameObject CollideEntity(Rectangle futureHitBox)
        {
            var movables = ClientHost.Client.Player.MapInstance.GetEntities<MovableEntity>();

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
