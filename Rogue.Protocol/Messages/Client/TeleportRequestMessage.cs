using LiteNetLib.Utils;
using Rogue.Core.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Client
{
    public class TeleportRequestMessage : Message
    {
        public const ushort Id = 21;

        public override ushort MessageId => Id;

        public int mapId;

        public TeleportRequestMessage(int mapId)
        {
            this.mapId = mapId;
        }
        public TeleportRequestMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.mapId = reader.GetInt();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(mapId);
        }
    }
}
