using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework;

namespace Rogue.Protocol.Types
{
    public class ProtocolEntityAura : MessageType
    {
        public const short Id = 12;

        public override short TypeIdProp => Id;

        public Color Color
        {
            get;
            set;
        }

        public float Radius
        {
            get;
            set;
        }

        public float Sharpness
        {
            get;
            set;
        }

        public ProtocolEntityAura(Color color, float radius, float sharpness)
        {
            this.Color = color;
            this.Radius = radius;
            this.Sharpness = sharpness;
        }
        public ProtocolEntityAura()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.Color = new Color(reader.GetByte(), reader.GetByte(), reader.GetByte(), reader.GetByte());
            this.Radius = reader.GetFloat();
            this.Sharpness = reader.GetFloat();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(Color.R);
            writer.Put(Color.G);
            writer.Put(Color.B);
            writer.Put(Color.A);
            writer.Put(Radius);
            writer.Put(Sharpness);
        }
    }
}
