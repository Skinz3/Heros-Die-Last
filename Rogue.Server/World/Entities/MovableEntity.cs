using MonoFramework.Collisions;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Entities
{
    public abstract class MovableEntity : Entity
    {
        public Stats Stats
        {
            get;
            private set;
        }
        public DirectionEnum Direction
        {
            get;
             set;
        }
        public MovableEntity(Stats stats)
        {
            this.Stats = stats;
        }
    }
}
