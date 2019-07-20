using Microsoft.Xna.Framework;
using Rogue.Core.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Lightning
{
    public class ColorGradiant
    {
        public const float SIZE = 255;

        public ColorableObject Target
        {
            get;
            set;
        }
        private Color StartColor
        {
            get;
            set;
        }
        private Color EndColor
        {
            get;
            set;
        }
        private bool Flag
        {
            get;
            set;
        }
        private int Index
        {
            get;
            set;
        }
        private int Speed
        {
            get;
            set;
        }
        public ColorGradiant(ColorableObject target, Color start, Color end, int speed)
        {
            this.Target = target;
            this.StartColor = start;
            this.EndColor = end;
            Speed = speed;
        }
        public void Update(GameTime time)
        {
            if (Flag)
            {
                Index += Speed;
            }
            else
            {
                Index -= Speed;
            }

            if (Index >= SIZE)
            {
                Flag = false;
            }
            if (Index <= 0)
            {
                Flag = true;
            }

            var rAverage = StartColor.R + (int)((EndColor.R - StartColor.R) * Index / SIZE);
            var gAverage = StartColor.G + (int)((EndColor.G - StartColor.G) * Index / SIZE);
            var bAverage = StartColor.B + (int)((EndColor.B - StartColor.B) * Index / SIZE);
            var aAverage = StartColor.A + (int)((EndColor.A - StartColor.A) * Index / SIZE);

            Target.Color = new Color(rAverage, gAverage, bAverage, aAverage);
        }
    }
}
