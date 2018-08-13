using MonoFramework.DesignPattern;
using MonoFramework.Network.Protocol;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Network;
using Rogue.Server.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Handlers
{
    public class AuthentificationHandler
    {
        [InDeveloppement(InDeveloppementState.TODO, "Hash password, and other serialization system btw?")]
        [MessageHandler]
        public static void HandleAuthentificationRequestMessage(AuthentificationRequestMessage message, RogueClient client)
        {
            AccountRecord account = AccountRecord.GetAccountByUsername(message.Username);

            if (account == null || account.Password != message.Password) // le compte n'existe pas ou, le mot de passe est inccorect
            {
                client.Send(new AuthentificationFailedMessage(AuthentificationFailureEnum.BAD_CREDENTIALS)); // on envoit une réponse au client
            }
            else if (account.Banned) // si le compte est banni
            {
                client.Send(new AuthentificationFailedMessage(AuthentificationFailureEnum.BANNED));
            }
            else if (Program.Server.IsClientConnected(message.Username))
            {
                client.Send(new AuthentificationFailedMessage(AuthentificationFailureEnum.CONNECTED));
            }
            else // Si les informations sont correctes
            {
                client.DefineAccount(account); // Alors on attribue au client son compte

                client.Send(new AuthentificationSuccesMessage(account.Id, account.CharacterName, account.Email, account.Iron, account.Gold,
                    account.LeaveRatio, account.FriendList.ToArray(), Configuration.Self.DiagnosticsEnabled)); // on lui fournis le reste des informations de son compte

                client.OpenMenu();
            }
        }
    }
}