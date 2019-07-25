using LiteNetLib.Utils;
using Rogue.Core.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class RemoveProjectileMessage : Message
    {
        public const ushort Id = 52;

        public override ushort MessageId => Id;

        public int id;

        public RemoveProjectileMessage(int id)
        {
            this.id = id;
        }
        public RemoveProjectileMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.id = reader.GetInt();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(id);
        }
    }
}
