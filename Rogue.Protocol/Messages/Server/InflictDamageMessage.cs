using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class InflictDamageMessage : Message
    {
        public const ushort Id = 35;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int targetId;

        public int amount;

        public InflictDamageMessage()
        {

        }
        public InflictDamageMessage(int targetId, int amount)
        {
            this.targetId = targetId;
            this.amount = amount;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.targetId = reader.GetInt();
            this.amount = reader.GetInt();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(targetId);
            writer.Put(amount);
        }
    }
}
