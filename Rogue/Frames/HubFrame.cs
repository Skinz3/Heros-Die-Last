using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Network;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;
using Rogue.Protocol.Messages.Server;

namespace Rogue.Frames
{
    public class HubFrame : ClientFrame
    {
        public HubFrame(string sceneName = "") : base(sceneName)
        {
        }

        public override FrameEnum FrameEnum => FrameEnum.HUB;

        public override ushort[] HandledMessages => new ushort[]
        {
            GameEntitiesMessage.Id,
            ShowEntityMessage.Id,
            EntityDispositionMessage.Id,
            RemoveEntityMessage.Id,
        };
        protected override void OnFrameLoaded()
        {
            base.OnFrameLoaded();
            ClientHost.Client.Send(new GameEntitiesRequestMessage());
        }
    }
}
