using Microsoft.Xna.Framework;
using Rogue.Core.Objects;
using Rogue.Server.World.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.ServerView.Objects
{
    public class ProjectileRepresentation : GCircle
    {
        private Projectile Projectile
        {
            get;
            set;
        }
        public ProjectileRepresentation(Projectile projectile, Color color) : base(projectile.Center, projectile.SizeF, color)
        {
            this.Projectile = projectile;
        }
        public override void OnUpdate(GameTime time)
        {
            Position = Projectile.Center - new Vector2(Projectile.SizeF / 2, Projectile.SizeF / 2);
            base.OnUpdate(time);
        }
    }
}
