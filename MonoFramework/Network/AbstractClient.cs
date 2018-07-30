using LiteNetLib;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Network
{
    public abstract class AbstractClient
    {
        /// <summary>
        /// Coté serveur, 'Socket' corréspond au socket d'écoute.
        /// Coté client, il s'agit du socket connecté au serveur.
        /// </summary>
        public NetPeer Target
        {
            get;
            protected set;
        }
        public string Ip
        {
            get
            {
                return Target.EndPoint.ToString();
            }
        }
        public Action OnConnectionSucceed
        {
            get;
            set;
        }
        public Action OnConnectionFailed
        {
            get;
            set;
        }
        public Action OnConnectionLost
        {
            get;
            set;
        }
        public abstract void OnDataArrival(byte[] buffer);

        protected abstract bool OnMessageReceived(Message message);

        public abstract void OnMessageSended(Message message);

        protected abstract void OnDataReceived(byte[] buffer);


    }
}
