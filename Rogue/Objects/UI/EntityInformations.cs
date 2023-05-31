using Microsoft.Xna.Framework;
using Rogue.Core;
using Rogue.Core.Geometry;
using Rogue.Core.Objects;
using Rogue.Core.Objects.Abstract;
using Rogue.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Objects.UI
{
    public class EntityInformations : PositionableObject
    {
        public static Point SIZE = new Point(60, 5);

        public MovableEntity Entity
        {
            get;
            set;
        }
        public EntityInformations(MovableEntity entity) : base(new Vector2(), SIZE)
        {
            this.Entity = entity;
        }
        public override void OnInitialize()
        {
           SetText(Entity.Name, new Color(Color.White, 0.9f), RectangleOrigin.CenterTop, 1);
        }

        public override void OnDraw(GameTime time)
        {
            if (Entity.Stats.LifeRatio >= 1)
            {
                return;
            }
            int width = 70;
            int height = 8;
            int thick = 2;

            var rect = new Rectangle((int)Center.X - width / 2, (int)(Center.Y - height / 2) + 15, width, height);
            Debug.DrawRectangle(rect, new Color(Color.Black, 0.4f), thick);

            var stats = Entity.Stats;
            float ratio = (float)stats.LifePoints / (float)stats.MaxLifePoints;

            rect.Width = (int)(width * ratio);

            rect.Width -= thick;

            rect.Height -= thick;

            rect.X += thick;
            rect.Y += thick;

            Debug.FillRectangle(rect, new Color(Color.Red,80f));
        }

        public override void OnInitializeComplete()
        {

        }

        public override void OnUpdate(GameTime time)
        {
            var rect = Entity.Collider.EntityHitBox;
            rect.Location -= new Point(0, rect.Height / 2);
            this.Align(rect, RectangleOrigin.CenterTop);
            //Text.Position.Y -= 10 / 2;
        }

        public override void OnDispose()
        {
        }
    }
}
