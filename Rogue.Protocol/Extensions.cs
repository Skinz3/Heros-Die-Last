using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol
{
    public static class Extensions
    {
        public static Vector2 GetVector2(this LittleEndianReader reader)
        {
            Vector2 vector = new Vector2();
            vector.X = reader.GetFloat();
            vector.Y = reader.GetFloat();
            return vector;
        }
        public static void Put(this LittleEndianWriter writer, Vector2 vector)
        {
            writer.Put(vector.X);
            writer.Put(vector.Y);
        }
        public static void Put(this LittleEndianWriter writer, Point point)
        {
            writer.Put(point.X);
            writer.Put(point.Y);
        }

        public static void Put(this LittleEndianWriter writer, Color color)
        {
            writer.Put(color.R);
            writer.Put(color.G);
            writer.Put(color.B);
            writer.Put(color.A);
        }
        public static Color GetColor(this LittleEndianReader reader)
        {
            return new Color(reader.GetByte(), reader.GetByte(), reader.GetByte(), reader.GetByte());
        }

        public static Point GetPoint(this LittleEndianReader reader)
        {
            Point point = new Point();
            point.X = reader.GetInt();
            point.Y = reader.GetInt();
            return point;
        }
    }
}
