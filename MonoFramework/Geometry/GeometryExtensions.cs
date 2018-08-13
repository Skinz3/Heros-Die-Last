using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework.Collisions;
using MonoFramework.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Geometry
{
    public static class GeometryExtensions
    {
        public static Point GetOrigin(this Rectangle rectangle, RectangleOrigin origin)
        {
            Point org = new Point();

            switch (origin)
            {
                case RectangleOrigin.TopRight:
                    org = new Point(rectangle.X + rectangle.Width, rectangle.Y);
                    break;
                case RectangleOrigin.TopLeft:
                    org = rectangle.Location;
                    break;
                case RectangleOrigin.BottomRight:
                    org = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
                    break;
                case RectangleOrigin.BottomLeft:
                    org = new Point(rectangle.X, rectangle.Y + rectangle.Height);
                    break;
                case RectangleOrigin.Center:
                    org = rectangle.Center;
                    break;
                case RectangleOrigin.CenterRight:
                    org = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height / 2);
                    break;
                case RectangleOrigin.CenterLeft:
                    org = new Point(rectangle.X, rectangle.Y + rectangle.Height / 2);
                    break;
                case RectangleOrigin.CenterTop:
                    org = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y);
                    break;
                case RectangleOrigin.CenterBottom:
                    org = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height);
                    break;
                default:
                    break;
            }
            return org;
        }
        /// <summary>
        ///  Thanks!
        ///  https://stackoverflow.com/questions/1945100/resizing-a-rectangle-from-different-origins
        ///  </summary>
        public static Rectangle Scale(this Rectangle rectangle, float widthScale, float heightScale, RectangleOrigin origin)
        {
            float x = 0;
            float y = 0;

            Point top = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y);
            Point center = rectangle.Center;

            Point org = GetOrigin(rectangle, origin);

            x = org.X + (top.X - org.X) * widthScale - rectangle.Width * widthScale / 2;
            y = org.Y + (top.Y - org.Y) * heightScale;

            float width = rectangle.Width * widthScale;
            float height = rectangle.Height * heightScale;
            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }
        public static Rectangle Divide(this Rectangle rectangle, int widthDivider, int heightDivider, RectangleOrigin origin)
        {
            return Scale(rectangle, 1f / widthDivider, 1f / heightDivider, origin);
        }
        public static Rectangle FlipHorizontallyAndVertically(this Rectangle rectangle)
        {
            rectangle.Width = -rectangle.Width;
            rectangle.Height = -rectangle.Height;
            rectangle.X -= rectangle.Width;
            rectangle.Y -= rectangle.Width;
            return rectangle;
        }
        public static bool NormalizedBand(this Vector2 normalizedInput, Vector2 reference)
        {
            if (normalizedInput.X > 0)
            {
                normalizedInput.X = 1;
            }
            if (normalizedInput.X < -0.5)
            {
                normalizedInput.X = -1;
            }
            if (normalizedInput.Y > 0)
            {
                normalizedInput.Y = 1;
            }
            if (normalizedInput.Y < -0.5)
            {
                normalizedInput.Y = -1;
            }
            return normalizedInput == reference;
        }
        public static DirectionEnum GetDirection(this Vector2 normalizedInput)
        {

            if (normalizedInput.NormalizedBand(new Vector2(1, 0)))
            {
                return DirectionEnum.Right;
            }
            else if (normalizedInput.NormalizedBand(new Vector2(-1, 0)))
            {
                return DirectionEnum.Left;
            }
            else if (normalizedInput.NormalizedBand(new Vector2(0, 1)))
            {
                return DirectionEnum.Down;
            }
            else if (normalizedInput.NormalizedBand(new Vector2(0, -1)))
            {
                return DirectionEnum.Up;
            }
            else if (normalizedInput.NormalizedBand(new Vector2(1, 1)))
            {
                return DirectionEnum.Right | DirectionEnum.Down;
            }
            else if (normalizedInput.NormalizedBand(new Vector2(1, -1)))
            {
                return DirectionEnum.Right | DirectionEnum.Up;
            }
            else if (normalizedInput.NormalizedBand(new Vector2(-1, 1)))
            {
                return DirectionEnum.Left | DirectionEnum.Down;
            }
            else if (normalizedInput.NormalizedBand(new Vector2(-1, -1)))
            {
                return DirectionEnum.Left | DirectionEnum.Up;
            }
            return DirectionEnum.None;
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
                case DirectionEnum.Right | DirectionEnum.Down:
                    input.X++;
                    input.Y++;
                    break;
                case DirectionEnum.Right | DirectionEnum.Up:
                    input.X++;
                    input.Y--;
                    break;
                case DirectionEnum.Left | DirectionEnum.Down:
                    input.X--;
                    input.Y++;
                    break;
                case DirectionEnum.Left | DirectionEnum.Up:
                    input.X--;
                    input.Y--;
                    break;
            }
            return input;
        }

    }
}
