using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects
{
    public class GCircle : SingleTextureObject
    {
        public float Radius
        {
            get;
            private set;
        }
        public float Diameter
        {
            get
            {
                return Radius * 2;
            }
        }

        public float Sharpness
        {
            get;
            private set;
        }
        public float Thickness
        {
            get;
            set;
        }
        public GCircle(Vector2 position, float radius, Color color, float sharpness = 1f, int thickness = -1) : base(position,new Point((int)radius,(int)radius),color)
        {
            this.Radius = radius;
            this.Sharpness = sharpness;
            this.Thickness = thickness;
            if (thickness == -1)
                Thickness = Radius;
        }

        public override Texture2D CreateTexture(GraphicsDevice graphicsDevice)
        {
            Texture2D circleTexture = new Texture2D(graphicsDevice, (int)Diameter, (int)Diameter, false, SurfaceFormat.Color);
            Color[] colorData = new Color[circleTexture.Width * circleTexture.Height];
            Vector2 center = new Vector2(Radius);
            for (int colIndex = 0; colIndex < circleTexture.Width; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < circleTexture.Height; rowIndex++)
                {
                    Vector2 position = new Vector2(colIndex, rowIndex);
                    float distance = Vector2.Distance(center, position);

                    // hermite iterpolation
                    float x = distance / Diameter;
                    float edge0 = (Radius * Sharpness) / Diameter;
                    float edge1 = Radius / Diameter;
                    float temp = MathHelper.Clamp((x - edge0) / (edge1 - edge0), 0.0f, 1.0f);
                    float result = temp * temp * (3.0f - 2.0f * temp);

                    if (distance >= Radius - Thickness)
                    {
                        colorData[rowIndex * circleTexture.Width + colIndex] = Color * (1f - result);
                    }
                    else
                    {
                        colorData[rowIndex * circleTexture.Width + colIndex] = Color.Transparent;
                    }
                }
            }
            circleTexture.SetData<Color>(colorData);

            return circleTexture;
        }


        public override void OnUpdate(GameTime time)
        {

        }

        public override void OnDispose()
        {
            Texture.Dispose();
        }
    }
}
