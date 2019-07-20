using Microsoft.Xna.Framework;
using Rogue.Core.DesignPattern;
using Rogue.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Lightning
{
    public class PointLight
    {
        private GCircle GCircle
        {
            get;
            set;
        }

        [InDeveloppement(InDeveloppementState.TEMPORARY, "cute comment")]
        public PointLight(GCell cell, int radius, Color color, float sharpness)
        {
            this.GCircle = new GCircle(cell.Center - new Vector2(radius / 2), radius, color, sharpness);
            this.GCircle.Initialize();

            // this.GCircle.DefineGradiant(GCircle.DefaultColor, new Color(Color.Purple, 80), 1);
        }
        public void Update(GameTime time)
        {
            GCircle.Update(time);
        }
        public void Draw(GameTime time)
        {
            this.GCircle.Draw(time);
        }
    }
}
