using LiteNetLib.Utils;
using Rogue.Core.Network.Protocol;
using Rogue.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class LoadFrameMessage : Message
    {
        public const ushort Id = 6;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public FrameEnum frame;

        public string dataName;

        public LoadFrameMessage()
        {

        }
        public LoadFrameMessage(FrameEnum frame, string dataName = "")
        {
            this.frame = frame;
            this.dataName = dataName;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.frame = (FrameEnum)reader.GetByte();
            this.dataName = reader.GetString();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put((byte)frame);
            writer.Put(dataName);
        }
    }
}
