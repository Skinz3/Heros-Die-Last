using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Rogue.Core.Lightning;

namespace Rogue.Core.Objects.Abstract
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
        private ColorGradiant Gradiant
        {
            get;
            set;
        }
        protected GCircle Aura
        {
            get;
            private set;
        }

        public ColorableObject(Vector2 position, Point size, Color color, float rotation = 0) : base(position, size, rotation)
        {
            this.Color = color;
            this.DefaultColor = color;
        }
        public void DefineGradiant(Color startColor, Color endColor,int speed)
        {
            this.Gradiant = new ColorGradiant(this, startColor, endColor,speed);
        }
        public void DefineAura(Color color, float radius, float sharpness)
        {
            Aura = new GCircle(new Vector2(), radius, color, sharpness);
            Aura.Initialize();
        }
        public void RemoveAura()
        {
            Aura = null;
        }
        public void SetTransparency(float value)
        {
            if (value > 1)
                value = 1;
            if (value < 0)
                value = 0;
            this.Color = new Color(Color, value);
        }
        public override void Draw(GameTime time)
        {
            base.Draw(time);

            if (Aura != null)
            {
                Aura.Draw(time);
            }
        }
        public override void Update(GameTime time)
        {
            if (Aura != null)
            {
                Aura.Position = this.Center - new Vector2(Aura.Radius / 2, Aura.Radius / 2);
            }

            base.Update(time);
        }
        public override void OnUpdate(GameTime time)
        {
            Gradiant?.Update(time);
        }
        public float GetTransparency()
        {
            return (Color.A / 255f);
        }


    }
}
