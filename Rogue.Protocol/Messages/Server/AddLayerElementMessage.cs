using LiteNetLib.Utils;
using Rogue.Core.Network.Protocol;
using Rogue.Core.Objects;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class AddLayerElementMessage : Message
    {
        public const ushort Id = 39;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public ProtocolLayerElement mapObject;

        public AddLayerElementMessage()
        {

        }
        public AddLayerElementMessage(ProtocolLayerElement mapObject)
        {
            this.mapObject = mapObject;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.mapObject = new ProtocolLayerElement();
            this.mapObject.Deserialize(reader);
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            mapObject.Serialize(writer);
        }
    }
}
