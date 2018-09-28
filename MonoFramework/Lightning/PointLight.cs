using Microsoft.Xna.Framework;
using MonoFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Lightning
{
    public class PointLight
    {
        private GCircle GCircle
        {
            get;
            set;
        }
        public PointLight(GCell cell, int radius, Color color, float sharpness)
        {
            this.GCircle = new GCircle(cell.Center - new Vector2(radius/2), radius, color, sharpness);
            this.GCircle.Initialize();
        }

        public void Draw(GameTime time)
        {
            this.GCircle.Draw(time);
        }
    }
}
