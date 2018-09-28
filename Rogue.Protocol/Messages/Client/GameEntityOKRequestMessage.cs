using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;

namespace Rogue.Protocol.Messages.Client
{
    public class GameEntityOKRequestMessage : Message
    {
        public const ushort Id = 25;

        public GameEntityOKRequestMessage()
        {
        }

        public override ushort MessageId => Id;

        public override void Deserialize(LittleEndianReader reader)
        {

        }

        public override void Serialize(LittleEndianWriter writer)
        {

        }
    }
}
