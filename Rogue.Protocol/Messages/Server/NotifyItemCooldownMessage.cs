using LiteNetLib.Utils;
using Rogue.Core.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class NotifyItemCooldownMessage : Message
    {
        public const ushort Id = 48;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public byte slotId;

        public float cooldown;

        public NotifyItemCooldownMessage()
        {

        }
        public NotifyItemCooldownMessage(byte slotId, float cooldown)
        {
            this.slotId = slotId;
            this.cooldown = cooldown;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.slotId = reader.GetByte();
            this.cooldown = reader.GetFloat();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(slotId);
            writer.Put(cooldown);
        }
    }
}
