using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;
using Rogue.Protocol.Messages.Server;
using Rogue.Server.World.Maps;

namespace Rogue.Server.Frames
{
    public class HubFrame : ServerFrame
    {
        public HubFrame(string sceneName) : base(sceneName)
        {

        }

        public override FrameEnum FrameEnum => FrameEnum.HUB;

        public override ushort[] HandledMessages => new ushort[]
        {
            FrameLoadedMessage.Id,
            GameEntitiesRequestMessage.Id,
            GameEntityOKRequestMessage.Id,
            EntityDispositionRequestMessage.Id,
            ClickMessage.Id,
            KeyInputMessage.Id,
        };

        public override void Enter()
        {
            MapsManager.JoinFreeMapInstance(Client.Player, SceneName);
            base.Enter();
        }
        public override void OnEntitiesOK()
        {

        }

        public override void OnLeave()
        {

        }
    }
}
