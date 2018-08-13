using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using Rogue.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class LoadFrameMessage : Message
    {
        public const ushort Id = 6;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public FrameEnum frame;

        public string sceneName;

        public LoadFrameMessage()
        {

        }
        public LoadFrameMessage(FrameEnum frame, string sceneName = "")
        {
            this.frame = frame;
            this.sceneName = sceneName;
        }
        public override void Deserialize(NetDataReader reader)
        {
            this.frame = (FrameEnum)reader.GetByte();
            this.sceneName = reader.GetString();
        }

        public override void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)frame);
            writer.Put(sceneName);
        }
    }
}
