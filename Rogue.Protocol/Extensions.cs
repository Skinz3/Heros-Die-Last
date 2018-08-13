using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using MonoFramework.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol
{
    public static class Extensions
    {
        public static Vector2 DeserializeVector2(NetDataReader reader)
        {
            Vector2 vector = new Vector2();
            vector.X = reader.GetFloat();
            vector.Y = reader.GetFloat();
            return vector;
        }
        public static void Serialize(this Vector2 vector, NetDataWriter writer)
        {
            writer.Put(vector.X);
            writer.Put(vector.Y);
        }
        public static void Serialize(this Point point, NetDataWriter writer)
        {
            writer.Put(point.X);
            writer.Put(point.Y);
        }
        public static Point DeserializePoint(NetDataReader reader)
        {
            Point point = new Point();
            point.X = reader.GetInt();
            point.Y = reader.GetInt();
            return point;
        }
    }
}
