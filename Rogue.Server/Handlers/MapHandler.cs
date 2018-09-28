﻿using MonoFramework.Network.Protocol;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Frames;
using Rogue.Server.Network;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rogue.Server.Handlers
{
    class MapHandler
    {
        
        [MessageHandler]
        public static void HandleGameEntitiesRequestMessage(GameEntitiesRequestMessage message, RogueClient client)
        {
            client.Send(new MapInformationMessage(client.Player.MapInstance.GetProtocolMapObjects(),client.Player.MapInstance.Record.GetProtocolMapLights()));
            client.Send(client.Player.MapInstance.GetGameEntitiesMessage());
        }
        [MessageHandler]
        public static void HandleGameEntityOkRequestMessage(GameEntityOKRequestMessage message, RogueClient client)
        {
            client.Player.Teleporting = false;
            client.GetFrame<ServerFrame>().OnEntitiesOK();

        }
        [MessageHandler]
        public static void HandleEntityDispositionRequestMessage(EntityDispositionRequestMessage message, RogueClient client)
        {
            client.Player.OnReceivePosition(message.position, message.direction);
        }
    }
}
