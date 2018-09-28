using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;

namespace Rogue.Frames
{
    public class MenuFrame : ClientFrame
    {
        public MenuFrame(string sceneName = "") : base(sceneName)
        {
        }

        public override FrameEnum FrameEnum => FrameEnum.MENU;

        public override ushort[] HandledMessages => new ushort[]
        {
            LoadFrameMessage.Id,
        };

    }
}
