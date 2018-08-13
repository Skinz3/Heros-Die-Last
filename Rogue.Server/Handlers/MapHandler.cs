using MonoFramework.Network.Protocol;
using Rogue.Protocol.Messages.Client;
using Rogue.Server.Frames;
using Rogue.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Handlers
{
    class MapHandler
    {
        [MessageHandler]
        public static void HandleGameEntitiesRequestMessage(GameEntitiesRequestMessage message, RogueClient client)
        {
            client.Send(client.Player.MapInstance.GetGameEntitiesMessage());
            client.Player.Teleporting = false;
        }
        [MessageHandler]
        public static void HandleGameEntityOkRequestMessage(GameEntityOKRequestMessage message, RogueClient client)
        {
            client.GetFrame<ServerFrame>().OnEntitiesOK();
        }
        [MessageHandler]
        public static void HandleEntityDispositionRequestMessage(EntityDispositionRequestMessage message, RogueClient client)
        {
            client.Player.OnReceivePosition(message.position, message.direction);
        }
    }
}
