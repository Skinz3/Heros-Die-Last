using LiteNetLib;
using MonoFramework.Network.Protocol;
using MonoFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Network
{
    public abstract class Server<T> where T : Client
    {
        /// <summary>
        /// Called when a client connect to the server on the EndPoint
        /// </summary>
        protected abstract void OnClientConnected(NetEndPoint ip);
        /// <summary>
        /// Called when server failed to start on the EndPoint
        /// </summary>
        protected abstract void OnServerFailedToStart();
        /// <summary>
        /// Called When server is started
        /// </summary>
        protected abstract void OnServerStarted();

        public IPEndPoint EndPoint
        {
            get; private set;
        }
        private Dictionary<NetEndPoint, T> Clients;

        public int ClientsCount
        {
            get
            {
                return Clients.Count;
            }
        }
        private NetManager NetManager
        {
            get;
            set;
        }
        private EventBasedNetListener EventListener
        {
            get;
            set;
        }
        public Server()
        {
            Clients = new Dictionary<NetEndPoint, T>();
        }
        /// <summary>
        /// Try to start the server and call OnListenFailed() if the thread catch an exeption.
        /// </summary>
        public void Start(int port)
        {
            this.EventListener = new EventBasedNetListener();
            this.NetManager = new NetManager(EventListener, 200, "coucou");
            this.NetManager.UnsyncedEvents = true;
            EndPoint = new IPEndPoint(IPAddress.Any, port);

            if (NetManager.Start(port))
            {
                EventListener.NetworkReceiveEvent += EventListener_NetworkReceiveEvent;
                EventListener.PeerConnectedEvent += Listener_PeerConnectedEvent;
                EventListener.PeerDisconnectedEvent += EventListener_PeerDisconnectedEvent;
                OnServerStarted();
            }
            else
            {
                OnServerFailedToStart();
            }


        }

        private void EventListener_NetworkReceiveEvent(NetPeer peer, LiteNetLib.Utils.NetDataReader reader)
        {
            Clients[peer.EndPoint].OnDataArrival(reader.Data);
        }
        private void EventListener_PeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            if (Clients.ContainsKey(peer.EndPoint))
                OnClientDisconnected(Clients[peer.EndPoint], disconnectInfo);
            else
                ProtocolManager.logger.Write("Unable to disconnect " + peer.EndPoint.ToString(), MessageState.ERROR);
        }

        public virtual void OnClientDisconnected(T client, DisconnectInfo? disconnectInfo = null) // null = server disconnect
        {
            RemoveClient(client.Target.EndPoint);
        }

        private void Listener_PeerConnectedEvent(NetPeer peer)
        {
            ProcessAccept(peer);
        }

        /// <summary>
        /// Stop the server
        /// </summary>
        public void Stop()
        {
            NetManager.Stop();
        }

        protected abstract T CreateClient(NetPeer ip);

        void ProcessAccept(NetPeer peer)
        {
            if (Clients.ContainsKey(peer.EndPoint))
            {
                Console.WriteLine("Client already here");
            }
            else
            {
                T client = CreateClient(peer);
                AddClient(peer.EndPoint, client);
                OnClientConnected(peer.EndPoint);
            }

        }
        public T[] GetClients()
        {
            return Clients.Values.ToArray();
        }
        public void AddClient(NetEndPoint ip, T client)
        {
            Clients.Add(ip, client);
        }
        public void RemoveClient(NetEndPoint endPoint)
        {
            Clients.Remove(endPoint);
        }
    }
}
