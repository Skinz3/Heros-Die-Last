using Microsoft.Xna.Framework;
using Rogue.Core.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Collisions
{
    public class Raycast
    {
        private Ray2D Ray
        {
            get;
            set;
        }
        public Raycast(Ray2D ray)
        {
            this.Ray = ray;
        }
        /// <summary>
        /// Todo
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<PositionableObject> Cast(PositionableObject[] sources)
        {
            List<PositionableObject> results = new List<PositionableObject>();

            foreach (var source in sources)
            {
                if (Ray.Intersects(source.Rectangle))
                    yield return source;
            }
        }
    }
}
