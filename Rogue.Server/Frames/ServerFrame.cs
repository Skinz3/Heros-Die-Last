using MonoFramework.Network;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.Network;
using Rogue.Server.Records;
using Rogue.Server.World.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Frames
{
    public abstract class ServerFrame : Frame
    {
        public abstract FrameEnum FrameEnum
        {
            get;
        }
        protected RogueClient Client
        {
            get;
            private set;
        }
        public bool IsLoading
        {
            get;
            private set;
        }
        public ServerFrame(string sceneName = "") : base(sceneName)
        {
            this.IsLoading = false;
        }
        
        public void Load(RogueClient client)
        {
            this.Client = client;
        }
        public override void Enter()
        {
            if (UseSceneManager)
            {
                IsLoading = true;
                Client.Send(new LoadFrameMessage(FrameEnum, SceneName)); // on dis au client de charger cette frame
            }
        }

     

        public abstract void OnEntitiesOK();

        public virtual void OnClientLoaded()
        {
            IsLoading = false;
        }
    }
}
