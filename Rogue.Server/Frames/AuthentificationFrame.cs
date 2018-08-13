using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;
using Rogue.Protocol.Messages.Server;

namespace Rogue.Server.Frames
{
    public class AuthenticationFrame : ServerFrame
    {
        public AuthenticationFrame()
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
                    AuthentificationRequestMessage.Id,
                };
            }
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void OnEntitiesOK()
        {
            throw new NotImplementedException();
        }

        public override void OnLeave()
        {
        }
    }
}
