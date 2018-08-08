using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework.Collisions;
using MonoFramework.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework
{
    public static class GeometryExtensions
    {
        /// <summary>
        /// rien a faire ici, dans un systeme de collider (peut être meme un calcul constant pour un certain type d'entitée?)
        /// Enfait c'est l'offset quand le personnage se déplace vers le bas
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Vector2 GetCollidePointForDownDirection(this Rectangle rectangle)
        {
            return new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Bottom);
        }
        public static Rectangle Divide(this Rectangle rectangle, int dividerWidth, int dividerHeight)
        {
            int newWidth = (rectangle.Width / dividerWidth);
            int newHeight = (rectangle.Height / dividerHeight);
            return new Rectangle(rectangle.X + (dividerWidth == 1 ? 0 : newWidth), rectangle.Y + (dividerHeight == 1 ? 0 : newHeight), newWidth, newHeight);
        }
        public static Rectangle FlipHorizontallyAndVertically(this Rectangle rectangle)
        {
            rectangle.Width = -rectangle.Width;
            rectangle.Height = -rectangle.Height;
            rectangle.X -= rectangle.Width;
            rectangle.Y -= rectangle.Width;
            return rectangle;
        }
        public static Vector2 GetInputVector(this DirectionEnum direction)
        {
            Vector2 input = new Vector2();

            switch (direction)
            {
                case DirectionEnum.None:
                    break;
                case DirectionEnum.Right:
                    input.X++;
                    break;
                case DirectionEnum.Left:
                    input.X--;
                    break;
                case DirectionEnum.Up:
                    input.Y--;
                    break;
                case DirectionEnum.Down:
                    input.Y++;
                    break;
            }
            return input;
        }

    }
}
