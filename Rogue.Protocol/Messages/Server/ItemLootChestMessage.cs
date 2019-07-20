using LiteNetLib.Utils;
using Rogue.Core.Network.Protocol;
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

        public string icon;

        public int cellId;

        public int quantity;

        public ItemLootChestMessage()
        {

        }
        public ItemLootChestMessage(int itemId, string icon, int cellId, int quantity)
        {
            this.itemId = itemId;
            this.icon = icon;
            this.cellId = cellId;
            this.quantity = quantity;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.itemId = reader.GetInt();
            this.icon = reader.GetString();
            this.cellId = reader.GetInt();
            this.quantity = reader.GetInt();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(itemId);
            writer.Put(icon);
            writer.Put(cellId);
            writer.Put(quantity);
        }
    }
}
