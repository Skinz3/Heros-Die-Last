using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Network
{
    public abstract class Frame
    {
        protected string SceneName
        {
            get;
            private set;
        }
        public bool UseSceneManager
        {
            get
            {
                return SceneName != string.Empty;
            }
        }
        public Frame(string sceneName)
        {
            SceneName = sceneName;
            Handlers = ProtocolManager.GetHandlers(HandledMessages);

        }
        private Dictionary<ushort, Delegate> Handlers
        {
            get;
        }
        public abstract ushort[] HandledMessages
        {
            get;
        }
        public Delegate GetHandler(ushort messageId)
        {
            if (Handlers.ContainsKey(messageId))
            {
                return Handlers[messageId];
            }
            else
            {
                return null;
            }
        }


        public abstract void Enter();

        public abstract void OnLeave();
    }
}
