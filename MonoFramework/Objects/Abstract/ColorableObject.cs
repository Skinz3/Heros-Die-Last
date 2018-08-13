using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoFramework.Objects.Abstract
{
    public abstract class ColorableObject : PositionableObject
    {
        public Color DefaultColor
        {
            get;
            private set;
        }
        public Color Color
        {
            get;
            set;
        }
        public ColorableObject(Vector2 position, Point size, Color color) : base(position, size)
        {
            this.Color = color;
            this.DefaultColor = color;
        }
        public void SetTransparency(float value)
        {
            if (value < 0 || value > 1)
                throw new Exception("Invalid transparency value :" + value + ", (0 < transparency < 1)");
            this.Color = new Color(Color, value);
        }


    }
}
