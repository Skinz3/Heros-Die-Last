using LiteNetLib.Utils;
using Rogue.Core.Network.Protocol;
using Rogue.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class RemoveLayerElementMessage : Message
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

        public bool IsCellWalkable;

        public RemoveLayerElementMessage()
        {

        }
        public RemoveLayerElementMessage(int cellId, LayerEnum layer, bool isCellWalkable)
        {
            this.cellId = cellId;
            this.layer = layer;
            this.IsCellWalkable = isCellWalkable;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.cellId = reader.GetInt();
            this.layer = (LayerEnum)reader.GetByte();
            this.IsCellWalkable = reader.GetBool();

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(cellId);
            writer.Put((byte)layer);
            writer.Put(IsCellWalkable);
        }

    }
}
