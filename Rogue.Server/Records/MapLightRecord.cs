using Microsoft.Xna.Framework;
using Rogue.ORM.Attributes;
using Rogue.ORM.Interfaces;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Records
{
    [Table("maplights")]
    public class MapLightRecord : ITable, IProtocolable<ProtocolMapLight>
    {
        public static List<MapLightRecord> MapLights = new List<MapLightRecord>();

        [Primary]
        public int Id;

        public int MapId;

        public int CellId;

        public int Radius;

        public byte R;

        public byte G;

        public byte B;

        public byte A;

        public float Sharpness;

        [Ignore]
        public Color Color;

        public MapLightRecord(int id, int mapId, int cellId, int radius, byte r, byte g, byte b, byte a, float sharpness)
        {
            this.Id = id;
            this.MapId = mapId;
            this.CellId = cellId;
            this.Radius = radius;
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
            this.Sharpness = sharpness;
            this.Color = new Color(r, g, b, a);
        }

        public ProtocolMapLight GetProtocolObject()
        {
            return new ProtocolMapLight(CellId, Radius, Color, Sharpness);
        }

        public static MapLightRecord[] GetMapLights(int mapId)
        {
            return MapLights.FindAll(x => x.MapId == mapId).ToArray();
        }
    }
}
