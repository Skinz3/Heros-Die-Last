using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework;

namespace Rogue.Protocol.Types
{
    public class ProtocolMapLight : MessageType
    {
        public const short Id = 11;

        public override short TypeIdProp => Id;

        public int CellId
        {
            get;
            private set;
        }

        public int Radius
        {
            get;
            private set;
        }

        public Color Color
        {
            get;
            private set;
        }

        public float Sharpness
        {
            get;
            private set;
        }

        public ProtocolMapLight()
        {

        }

        public ProtocolMapLight(int cellId, int radius, Color color, float sharpness)
        {
            this.CellId = cellId;
            this.Radius = radius;
            this.Color = color;
            this.Sharpness = sharpness;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.CellId = reader.GetInt();
            this.Radius = reader.GetInt();
            this.Color = reader.GetColor();
            this.Sharpness = reader.GetFloat();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(CellId);
            writer.Put(Radius);
            writer.Put(Color);
            writer.Put(Sharpness);
        }
    }
}
