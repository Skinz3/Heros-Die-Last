using Microsoft.Xna.Framework;
using Rogue.Core.Network.Protocol;
using Rogue.Core.Objects;
using Rogue.Core.Scenes;
using Rogue.Network;
using Rogue.Protocol.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Handlers
{
    class DebugHandler
    {
        [MessageHandler]
        public static void HandleDebugHighlightCellMessage(DebugHighlightCellMessage message, RogueClient client)
        {
            SceneManager.GetCurrentScene<MapScene>().Map.GetCell<GCell>(message.cellId).FillColor = message.color;
        }
        [MessageHandler]
        public static void HandleDebugClearHighlightCellsMessage(DebugClearHighlightCellsMessage message, RogueClient client)
        {
            foreach (var cell in SceneManager.GetCurrentScene<MapScene>().Map.Cells)
            {
                cell.FillColor = Color.Transparent;
            }
        }
    }
}
