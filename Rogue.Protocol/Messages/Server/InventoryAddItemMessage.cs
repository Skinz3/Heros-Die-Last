using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class InventoryAddItemMessage : Message
    {
        public const ushort Id = 42;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int itemId;
        public int quantity;
        public byte slot;

        public InventoryAddItemMessage()
        {

        }
        public InventoryAddItemMessage(int itemId,int quantity,byte slot)
        {
            this.itemId = itemId;
            this.quantity = quantity;
            this.slot = slot;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.itemId = reader.GetInt();
            this.quantity = reader.GetInt();
            this.slot = reader.GetByte();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(itemId);
            writer.Put(quantity);
            writer.Put(slot);
        }
    }
}
