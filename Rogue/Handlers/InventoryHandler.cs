using MonoFramework.Network.Protocol;
using MonoFramework.Sprites;
using Rogue.Network;
using Rogue.Protocol.Messages.Server;
using Rogue.World.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Handlers
{
    class InventoryHandler
    {
        [MessageHandler]
        public static void HandleInventoryAddItemMessage(InventoryAddItemMessage message, RogueClient client)
        {
            client.Inventory.AddItem(Item.CreateInstance(message.itemId, message.quantity), message.slot);
        }
        [MessageHandler]
        public static void HandleInventoryUpdateQuantityMessage(InventoryUpdateQuantityMessage message, RogueClient client)
        {
            client.Inventory.UpdateQuantity(message.slot, message.quantity);
        }
        [MessageHandler]
        public static void HandleInventoryRemoveItemMessage(InventoryRemoveItemMessage message, RogueClient client)
        {
            client.Inventory.RemoveItem(message.slot);
        }
    }
}
