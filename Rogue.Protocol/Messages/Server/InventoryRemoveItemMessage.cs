using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class InventoryRemoveItemMessage : Message
    {
        public const ushort Id = 43;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public byte slot;

        public InventoryRemoveItemMessage()
        {

        }
        public InventoryRemoveItemMessage(byte slot)
        {
            this.slot = slot;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.slot = reader.GetByte();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(slot);
        }
    }
}
