using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Server.World.Maps;

namespace Rogue.Server.World.Entities.Projectiles
{
    public abstract class Projectile : ServerObject
    {
        public abstract string SpriteName
        {
            get;
        }
        public Projectile(Vector2 position, Point size) : base(position, size)
        {

        }
      
        public override MapCell GetCell()
        {
            throw new NotImplementedException();
        }

        public override MapInstance GetMapInstance()
        {
            throw new NotImplementedException();
        }
    }
}
