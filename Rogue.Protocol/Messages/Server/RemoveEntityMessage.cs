using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;

namespace Rogue.Protocol.Messages.Server
{
    public class RemoveEntityMessage : Message
    {
        public const ushort Id = 11;

        public override ushort MessageId => Id;

        public int entityId;

        public RemoveEntityMessage(int entityId)
        {
            this.entityId = entityId;
        }
        public RemoveEntityMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.entityId = reader.GetInt();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(entityId);
        }
    }
}
