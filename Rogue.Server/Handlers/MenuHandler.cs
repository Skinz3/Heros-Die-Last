﻿using MonoFramework.DesignPattern;
using MonoFramework.Network.Protocol;
using Rogue.Protocol.Messages.Client;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Network;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Handlers
{
    class MenuHandler
    {
        [InDeveloppement]
        [MessageHandler]
        public static void HandlePlayRequestMessage(PlayRequestMessage message, RogueClient client)
        {
            client.DefinePlayer(new Player(client, EntityRecord.Players[0]));
            client.OpenHub();
        }
    }
}
