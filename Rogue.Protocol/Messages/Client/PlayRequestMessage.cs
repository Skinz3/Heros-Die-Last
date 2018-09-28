using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Client
{
    public class PlayRequestMessage : Message
    {
        public const ushort Id = 31;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public PlayRequestMessage()
        {

        }
       
        public override void Deserialize(LittleEndianReader reader)
        {

        }

        public override void Serialize(LittleEndianWriter writer)
        {

        }
    }
}
