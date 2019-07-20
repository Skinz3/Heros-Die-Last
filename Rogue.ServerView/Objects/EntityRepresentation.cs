using Microsoft.Xna.Framework;
using Rogue.Core.Geometry;
using Rogue.Core.Objects;
using Rogue.Core.Objects.Abstract;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.ServerView.Objects
{
    public class EntityRepresentation : GRectangle
    {
        private Entity Entity
        {
            get;
            set;
        }
        public EntityRepresentation(Entity entity, Color color) : base(entity.Position, entity.GetHitBox().Size, color, 1)
        {
            this.Entity = entity;
            SetText(entity.Name, Color.Red, RectangleOrigin.Center);
        }
        public override void OnUpdate(GameTime time)
        {
            Position = Entity.Position + Entity.GetHitBox().Size.ToVector2() ;
            base.OnUpdate(time);
        }

    }
}
