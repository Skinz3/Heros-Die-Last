using MonoFramework.Network.Protocol;
using Rogue.Auth;
using Rogue.Network;
using Rogue.Protocol.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Handlers
{
    class AuthentificationHandler
    {
        [MessageHandler]
        public static void HandleAuthentificationFailedMessage(AuthentificationFailedMessage message, RogueClient client)
        {
            client.Dispose();   
            ClientHost.DestroyClient();
        }
        [MessageHandler]
        public static void HandleAuthentificationSuccesMessage(AuthentificationSuccesMessage message, RogueClient client)
        {
            client.DefineAccount(new Account(message.accountId,message.characterName, message.email, message.iron, message.gold, message.leaveRatio, message.friendList));
        }
    }
}
