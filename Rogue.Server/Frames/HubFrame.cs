using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;

namespace Rogue.Server.Frames
{
    public class HubFrame : ServerFrame
    {
        private string MapName
        {
            get;
            set;
        }
        public HubFrame(string mapName, string sceneName = "GameScene") : base(sceneName)
        {
            this.MapName = mapName;
        }

        public override FrameEnum FrameEnum => FrameEnum.HUB;

        public override ushort[] HandledMessages => new ushort[]
        {
            FrameLoadedMessage.Id,
            GameEntitiesRequestMessage.Id,
            GameEntityOKRequestMessage.Id,
            EntityDispositionRequestMessage.Id,
        };

        public override void Enter()
        {
            base.JoinFreeMapInstance(MapName);
            base.Enter();
        }
        public override void OnEntitiesOK()
        {

        }

        public override void OnLeave()
        {
            throw new NotImplementedException();
        }
    }
}
