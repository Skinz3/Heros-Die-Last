using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;

namespace Rogue.Frames
{
    public class AuthentificationFrame : ClientFrame
    {
        public AuthentificationFrame(string sceneName = "") : base(sceneName)
        {
        }
        public override FrameEnum FrameEnum
        {
            get
            {
                return FrameEnum.AUTHENTIFICATION;
            }
        }

        public override ushort[] HandledMessages
        {
            get
            {
                return new ushort[]
                {
                    AuthentificationSuccesMessage.Id,
                    AuthentificationFailedMessage.Id,
                    LoadFrameMessage.Id,
                };
            }
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}
