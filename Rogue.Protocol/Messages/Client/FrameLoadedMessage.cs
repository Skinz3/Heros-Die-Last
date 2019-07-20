using LiteNetLib.Utils;
using Rogue.Core.Network.Protocol;
using Rogue.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Client
{
    public class FrameLoadedMessage : Message
    {
        public const ushort Id = 5;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }
        public FrameEnum frame;

        public FrameLoadedMessage()
        {

        }
        public FrameLoadedMessage(FrameEnum frame)
        {
            this.frame = frame;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.frame = (FrameEnum)reader.GetByte();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put((byte)frame);
        }
    }
}
