using Microsoft.Xna.Framework.Input;
using Rogue.Core.Network.Protocol;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;
using Rogue.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Handlers
{
    class InputHandler
    {
        [MessageHandler]
        public static void HandleKeyInputMessage(KeyInputMessage message, RogueClient client)
        {
            if (message.key == Keys.LeftShift)
            {
                client.Player.Inventory.UseItem(client.Player.Position,2);
            }
            if (message.key == Keys.A)
            {
                client.Player.Inventory.UseItem(client.Player.Position, 3);
            }
        }
        [MessageHandler]
        public static void HandleClickMessage(ClickMessage message, RogueClient client)
        {
            var cell = client.Player.MapInstance.Record.Grid.GetCell(message.position.ToVector2());

            if (cell != null)
            {
                var obj = client.Player.MapInstance.GetFirstMapObject(cell.Id);

                bool map = false;
                if (obj != null)
                {
                    if (cell.Adjacents.Contains(client.Player.GetCell()))
                    {
                        map = true;
                        obj.Trigger?.OnClicked(client.Player);
                    }
                }
                if (map)
                {
                    return;
                }
                if (cell != null)
                    Console.WriteLine(cell.Id);
            }

            client.Player.Position = message.playerPosition;


            if (message.clickType == ClickTypeEnum.Left)
            {
                client.Player.Inventory.UseItem(message.position.ToVector2(), 0);
            }
            if (message.clickType == ClickTypeEnum.Right)
            {
                client.Player.Inventory.UseItem(message.position.ToVector2(), 1);
            }

        }
    }
}
