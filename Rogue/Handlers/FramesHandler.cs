using Rogue.Core.Network.Protocol;
using Rogue.Core.Utils;
using Rogue.Frames;
using Rogue.Network;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;
using Rogue.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Handlers
{
    class FramesHandler
    {
        static Logger logger = new Logger();

        [MessageHandler]
        public static void HandleLoadFrameMessage(LoadFrameMessage message, RogueClient client)
        {
            switch (message.frame)
            {
                case FrameEnum.AUTHENTIFICATION:
                    client.LoadFrame(new AuthentificationFrame(message.dataName));
                    break;
                case FrameEnum.MENU:
                    client.LoadFrame(new MenuFrame(message.dataName));
                    break;
                case FrameEnum.HUB:
                    client.LoadFrame(new HubFrame(message.dataName));
                    break;
                default:
                    logger.Write("Server wants me to load a weird frame...", MessageState.ERROR);
                    break;
            }

        }
    }
}
