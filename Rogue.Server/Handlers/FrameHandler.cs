using Rogue.Core.Network.Protocol;
using Rogue.Protocol.Messages.Client;
using Rogue.Server.Frames;
using Rogue.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Handlers
{
    class FrameHandler
    {
        [MessageHandler]
        public static void HandleFrameLoadedMessage(FrameLoadedMessage message, RogueClient client)
        {
            ServerFrame currentFrame = client.GetFrame<ServerFrame>();

            if (currentFrame != null && currentFrame.FrameEnum == message.frame)
            {
                currentFrame.OnClientLoaded();
            }
        }
    }
}
