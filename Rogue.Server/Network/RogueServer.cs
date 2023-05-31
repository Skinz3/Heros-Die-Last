using LiteNetLib;
using Rogue.Server.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core.Network;
using Rogue.Core.Utils;

namespace Rogue.Server.Network
{
    public class RogueServer : Server<RogueClient>
    {
        public RogueClient[] Clients
        {
            get
            {
                return GetClients();
            }
        }
        private Logger logger = new Logger();

        protected override void OnServerStarted()
        {
            logger.Write("Server Started", MessageState.INFO);
        }
        protected override void OnServerFailedToStart()
        {
            logger.Write("Server failed to start", MessageState.ERROR_FATAL);
        }

        protected override void OnClientConnected(NetEndPoint ip)
        {
            logger.Write("New client connected (" + ip + ")", MessageState.IMPORTANT_INFO);
            Console.Title = ("Rogue.Server Clients: " + Clients.Count());
        }
        public override void OnClientDisconnected(RogueClient client, DisconnectInfo? disconnectInfo = null)
        {
            base.OnClientDisconnected(client, disconnectInfo);
            client.RemoveIdentity();
            logger.Write("Client disconnected", MessageState.IMPORTANT_INFO);
            Console.Title = ("Clients: " + Clients.Count());
        }

        public bool IsClientConnected(string username)
        {
            return Clients.Any(x => x.IsLogged && x.Account.Username == username);
        }

        protected override RogueClient CreateClient(NetPeer ip)
        {
           return new RogueClient(ip, new AuthenticationFrame());
        }
    }
}
