using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue.Core.Collisions;
using Rogue.Core.Input;
using Rogue.Core.Objects;
using Rogue.Core.Scenes;
using Rogue.Core.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Geometry
{
    public static class GeometryExtensions
    {

        public static bool CircleRectangleCollide(Vector2 center, float radius,
        Rectangle rectangle)
        {
            float xVal = center.X;
            if (xVal < rectangle.Left) xVal = rectangle.Left;
            if (xVal > rectangle.Right) xVal = rectangle.Right;

            float yVal = center.Y;
            if (yVal < rectangle.Top) yVal = rectangle.Top;
            if (yVal > rectangle.Bottom) yVal = rectangle.Bottom;

            Vector2 direction = new Vector2(center.X - xVal, center.Y - yVal);
            float distance = direction.Length();

            if ((distance > 0) && (distance < radius))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Vector2 GetVector2(this float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)-Math.Sin(angle));
        }
        public static float GetAngle(this Vector2 normalized)
        {
            return MathHelper.ToDegrees((float)Math.Atan2(-normalized.X, normalized.Y));
        }

        public static Vector2 ReflectVector(Vector2 direction, Vector2 center, Vector2 targetCenter)
        {
            var objDir = (targetCenter - center);

            objDir.Normalize();

            var oldDirection = direction;

            direction = new Vector2(-direction.X, -direction.Y);

            if (oldDirection.X > 0 && oldDirection.Y > 0)
            {
                if (objDir.X > 0.5f) // such cancer x)))
                {
                    direction = new Vector2(direction.X, -direction.Y);
                }
                else
                {
                    direction = new Vector2(-direction.X, direction.Y);
                }
            }

            if (oldDirection.X < 0 && oldDirection.Y > 0)
            {

                if (objDir.Y > 0.5f) // such cancer x)))
                {
                    direction = new Vector2(-direction.X, direction.Y);

                }
                else
                {
                    direction = new Vector2(direction.X, -direction.Y);
                }
            }
            if (oldDirection.X < 0 && oldDirection.Y < 0)
            {
                if (objDir.X > -0.5f) // such cancer x)))
                {
                    direction = new Vector2(-direction.X, direction.Y);

                }
                else
                {
                    direction = new Vector2(direction.X, -direction.Y);
                }
            }
            if (oldDirection.X > 0 && oldDirection.Y < 0)
            {
                if (objDir.Y < -0.5f) // such cancer x)))
                {
                    direction = new Vector2(-direction.X, direction.Y);

                }
                else
                {
                    direction = new Vector2(direction.X, -direction.Y);
                }
            }
            return direction;
        }

        public static float GetMouseRotation(this Vector2 relativeTo)
        {
            var vector = (SceneManager.GetCurrentScene<MapScene>().Map.TranslateToScenePosition(MouseManager.State.Position).ToVector2() - relativeTo);
            vector.Normalize();
            return (float)Math.Atan2(vector.X, -vector.Y);
        }
        public static DirectionEnum Restrict4Direction(this DirectionEnum direction)
        {
            switch (direction)
            {
                case DirectionEnum.None:
                    break;
                case DirectionEnum.Right | DirectionEnum.Up:
                    return DirectionEnum.Right;
                case DirectionEnum.Left | DirectionEnum.Up:
                    return DirectionEnum.Left;
                case DirectionEnum.Right | DirectionEnum.Down:
                    return DirectionEnum.Right;
                case DirectionEnum.Left | DirectionEnum.Down:
                    return DirectionEnum.Left;
            }
            return direction;
        }

        public static bool CellOutflow(this ICell cell, Vector2 center, DirectionEnum direction)
        {
            if (direction == DirectionEnum.Right)
            {
                return center.X >= cell.Rectangle.Center.X;
            }
            return false;
        }
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
        public static Vector2 NormalizeAbsolute(this Vector2 input)
        {
            Vector2 result = new Vector2();
            if (input.X > 0)
            {
                result.X = 1;
            }
            if (input.X < -0.5)
            {
                result.X = -1;
            }
            if (input.Y > 0)
            {
                result.Y = 1;
            }
            if (input.Y < -0.5)
            {
                result.Y = -1;
            }
            return result;
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
        public static DirectionEnum GetDirection(this Vector2 input)
        {
            if (input == Vector2.Zero)
            {
                return DirectionEnum.None;
            }
            float tolerance = 20;
            float angle = MathHelper.ToDegrees((float)Math.Atan2(input.X, -input.Y));

            if (angle < 0)
            {
                angle = 360 + angle;
            }

            if (angle.InBand(0 - tolerance, tolerance))
            {
                return DirectionEnum.Up;
            }
            if (angle.InBand(tolerance, 90 - tolerance))
            {
                return DirectionEnum.Right | DirectionEnum.Up;
            }
            if (angle.InBand(90 - tolerance, 90 + tolerance))
            {
                return DirectionEnum.Right;
            }
            if (angle.InBand(90 + tolerance, 180 - tolerance))
            {
                return DirectionEnum.Right | DirectionEnum.Down;
            }
            if (angle.InBand(180 - tolerance, 180 + tolerance))
            {
                return DirectionEnum.Down;
            }
            if (angle.InBand(180 + tolerance, 270 - tolerance))
            {
                return DirectionEnum.Left | DirectionEnum.Down;
            }
            if (angle.InBand(270 - tolerance, 270 + tolerance))
            {
                return DirectionEnum.Left;
            }
            if (angle.InBand(270 + tolerance, 360 - tolerance))
            {
                return DirectionEnum.Left | DirectionEnum.Up;
            }
            if (angle.InBand(360 - tolerance, 360))
            {
                return DirectionEnum.Up;
            }
            return DirectionEnum.None;

        }
        public static bool InBand(this float value, float minValue, float maxValue)
        {
            return value >= minValue && value <= maxValue;
        }
        public static DirectionEnum GetDirectionNormalized(this Vector2 normalizedInput)
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
                    input.X += 0.5f;
                    input.Y += 0.5f;
                    break;
                case DirectionEnum.Right | DirectionEnum.Up:
                    input.X += 0.5f;
                    input.Y -= 0.5f;
                    break;
                case DirectionEnum.Left | DirectionEnum.Down:
                    input.X -= 0.5f;
                    input.Y += 0.5f;
                    break;
                case DirectionEnum.Left | DirectionEnum.Up:
                    input.X -= 0.5f;
                    input.Y -= 0.5f;
                    break;
            }
            return input;
        }

    }
}
