using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using MonoFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class RemoveMapObjectMessage : Message
    {
        public const ushort Id = 40;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int cellId;

        public LayerEnum layer;

        public RemoveMapObjectMessage()
        {

        }
        public RemoveMapObjectMessage(int cellId, LayerEnum layer)
        {
            this.cellId = cellId;
            this.layer = layer;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.cellId = reader.GetInt();
            this.layer = (LayerEnum)reader.GetByte();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(cellId);
            writer.Put((byte)layer);
        }

    }
}
