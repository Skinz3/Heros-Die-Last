using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;

namespace Rogue.Server.Frames
{
    public class MenuFrame : ServerFrame
    {
        public MenuFrame(string sceneName = "") : base(sceneName)
        {
        }

        public override FrameEnum FrameEnum => FrameEnum.MENU;

        public override ushort[] HandledMessages => new ushort[]
        {
               FrameLoadedMessage.Id,
               PlayRequestMessage.Id,
        };

        public override void OnEntitiesOK()
        {
            throw new NotImplementedException();
        }

        public override void OnLeave()
        {
        }
    }
}
