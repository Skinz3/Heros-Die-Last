using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using Rogue.Protocol.Types;

namespace Rogue.Protocol.Messages.Server
{
    public class ShowEntityMessage : Message
    {
        public const ushort Id = 10;

        public ProtocolEntity entity;

        public ShowEntityMessage()
        {

        }
        public ShowEntityMessage(ProtocolEntity entity)
        {
            this.entity = entity;
        }

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }
        

        public override void Deserialize(NetDataReader reader)
        {
            this.entity = ProtocolTypeManager.GetInstance<ProtocolEntity>(reader.GetShort());
            entity.Deserialize(reader);
        }

        public override void Serialize(NetDataWriter writer)
        {
            writer.Put(entity.TypeIdProp);
            this.entity.Serialize(writer);
        }
    }
}
