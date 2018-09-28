using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class ItemLootChestMessage : Message
    {
        public const ushort Id = 41;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int itemId;

        public int cellId;

        public int quantity;

        public ItemLootChestMessage()
        {

        }
        public ItemLootChestMessage(int itemId, int cellId, int quantity)
        {
            this.itemId = itemId;
            this.cellId = cellId;
            this.quantity = quantity;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.itemId = reader.GetInt();
            this.cellId = reader.GetInt();
            this.quantity = reader.GetInt();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(itemId);
            writer.Put(cellId);
            writer.Put(quantity);
        }
    }
}
