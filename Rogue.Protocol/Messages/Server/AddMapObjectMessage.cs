using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using MonoFramework.Objects;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class AddMapObjectMessage : Message
    {
        public const ushort Id = 39;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public ProtocolMapObject mapObject;

        public AddMapObjectMessage()
        {

        }
        public AddMapObjectMessage(ProtocolMapObject mapObject)
        {
            this.mapObject = mapObject;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.mapObject = new ProtocolMapObject();
            this.mapObject.Deserialize(reader);
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            mapObject.Serialize(writer);
        }
    }
}
