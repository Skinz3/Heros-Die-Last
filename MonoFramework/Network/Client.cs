using LiteNetLib;
using LiteNetLib.Utils;
using Rogue.Core.Network.Protocol;
using Rogue.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Network
{
    public abstract class Client : AbstractClient
    {
        public const string SERVER_KEY = "ask3948r9SGcd";

        private EventBasedNetListener Listener
        {
            get;
            set;
        }
        NetManager NetManager
        {
            get;
            set;
        }
        /// <summary>
        /// Etat dans lequel ce trouve ce client
        /// </summary>
        private Frame Frame
        {
            get;
            set;
        }
        public bool Connecting
        {
            get;
            private set;
        }
        /// <summary>
        /// Utilisé par le client
        /// </summary>
        /// <param name="baseFrame"></param>
        public Client(Frame baseFrame)
        {
            Listener = new EventBasedNetListener();
            NetManager = new NetManager(Listener, SERVER_KEY);
            LoadFrame(baseFrame);
            Connecting = false;
        }
        /// <summary>
        /// Utilisé par serveur
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="baseFrame"></param>
        public Client(NetPeer target, Frame baseFrame)
        {
            Target = target;
            Listener = new EventBasedNetListener();
            NetManager = new NetManager(Listener, SERVER_KEY);
            LoadFrame(baseFrame);
        }
        public void Send(Message message, SendOptions method = SendOptions.ReliableOrdered)
        {
            LittleEndianWriter writer = new LittleEndianWriter();

            message.Pack(writer);
            var packet = writer.Data;
            this.Target.Send(packet, method);
            OnMessageSended(message);
            if (ProtocolManager.ShowProtocolMessage)
                ProtocolManager.logger.Write("Send " + message.ToString(), MessageState.INFO2);
        }
        /// <summary>
        /// Only used by client
        /// </summary>
        public void PollEvents()
        {
            NetManager.PollEvents();
        }
        public void Connect(string ip, int port)
        {
            NetManager.Start();
            Listener.NetworkReceiveEvent += Listener_NetworkReceiveEvent;
            Listener.PeerConnectedEvent += Listener_PeerConnectedEvent;
            Listener.PeerDisconnectedEvent += Listener_PeerDisconnectedEvent;
            Connecting = true;
            NetManager.Connect(ip, port);
        }

        private void Listener_NetworkReceiveEvent(NetPeer peer, LittleEndianReader reader)
        {
            OnDataArrival(reader.Data);
        }

        private void Listener_PeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            if (Connecting)
            {
                Connecting = false;
                OnConnectionFailed();
            }
            else
            {
                OnConnectionLost();
            }


        }

        private void Listener_PeerConnectedEvent(NetPeer peer)
        {
            Target = peer;
            Connecting = false;
            OnConnectionSucceed();
        }

        public override void OnDataArrival(byte[] buffer)
        {
            Message message = ProtocolManager.BuildMessage(buffer);

            if (message != null)
            {
                OnMessageReceived(message);
            }

            OnDataReceived(buffer);
        }
        protected override bool OnMessageReceived(Message message)
        {
            if (Frame == null)
            {
                ProtocolManager.logger.Write("Cannot handle client message, while not in a frame.", MessageState.ERROR);
                return false;
            }
            if (message == null)
            {
                if (ProtocolManager.ShowProtocolMessage)
                    ProtocolManager.logger.Write("Cannot build datas from client " + Ip, MessageState.WARNING);
                //    Disconnect(); ???
                return false;
            }

            var handler = Frame.GetHandler(message.MessageId);

            if (handler != null)
            {
                {
                    if (ProtocolManager.ShowProtocolMessage)
                        ProtocolManager.logger.Write("Receive " + message.ToString(), MessageState.INFO);
                    try
                    {
                        handler.DynamicInvoke(null, message, this);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        if (ProtocolManager.ShowProtocolMessage)
                            ProtocolManager.logger.Write(string.Format("Unable to handle message {0} {1} : '{2}'", message.ToString(), handler.Method.Name, ex.InnerException.ToString()), MessageState.WARNING);
                        return false;
                    }
                }
            }
            else
            {
                if (ProtocolManager.ShowProtocolMessage)
                    ProtocolManager.logger.Write(string.Format("No Handler: ({0}) {1}", message.MessageId, message.ToString()), MessageState.IMPORTANT_INFO);
                return true;
            }
        }
        public virtual void LoadFrame(Frame frame)
        {
            if (this.Frame != null)
            {
                this.Frame.OnLeave();
            }

            this.Frame = frame;
            this.Frame.Enter();
        }
        public T GetFrame<T>() where T : Frame
        {
            return (T)this.Frame;
        }
        public void Dispose()
        {
            NetManager.Stop();

        }
    }
}
