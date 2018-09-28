using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteNetLib.Utils;
using MonoFramework.Objects;
using Rogue.Protocol.Enums;

namespace Rogue.Protocol.Types
{
    public class ProtocolMapObject : MessageType
    {
        public const short Id = 10;

        public override short TypeIdProp => Id;

        public int CellId
        {
            get;
            private set;
        }

        public LayerEnum LayerEnum
        {
            get;
            private set;
        }
        public MapObjectType Type
        {
            get;
            private set;
        }
        public string VisualData
        {
            get;
            private set;
        }
        public bool Walkable
        {
            get;
            private set;
        }
        public ProtocolMapObject()
        {

        }
        public ProtocolMapObject(int cellId, LayerEnum layerEnum, MapObjectType type, string visualData, bool walkable)
        {
            this.CellId = cellId;
            this.LayerEnum = layerEnum;
            this.Type = type;
            this.VisualData = visualData;
            this.Walkable = walkable;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.CellId = reader.GetInt();
            this.LayerEnum = (LayerEnum)reader.GetByte();
            this.Type = (MapObjectType)reader.GetByte();
            this.VisualData = reader.GetString();
            this.Walkable = reader.GetBool();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(CellId);
            writer.Put((byte)LayerEnum);
            writer.Put((byte)Type);
            writer.Put(VisualData);
            writer.Put(Walkable);
        }
    }
}
