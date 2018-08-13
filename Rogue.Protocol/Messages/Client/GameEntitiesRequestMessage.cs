using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Client
{
    public class GameEntitiesRequestMessage : Message
    {
        public const ushort Id = 4;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }
        public GameEntitiesRequestMessage()
        {

        }
        public override void Deserialize(NetDataReader reader)
        {
           
        }

        public override void Serialize(NetDataWriter writer)
        {
            
        }
    }
}
