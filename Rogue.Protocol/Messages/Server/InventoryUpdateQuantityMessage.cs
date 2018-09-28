using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class InventoryUpdateQuantityMessage : Message
    {
        public const ushort Id = 44;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }
        public byte slot;
        public int quantity;

        public InventoryUpdateQuantityMessage()
        {

        }
        public InventoryUpdateQuantityMessage(byte slot, int quantity)
        {
            this.quantity = quantity;
            this.slot = slot;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.quantity = reader.GetInt();
            this.slot = reader.GetByte();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(quantity);
            writer.Put(slot);
        }
    }
}
