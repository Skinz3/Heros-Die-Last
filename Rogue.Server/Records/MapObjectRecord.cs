using MonoFramework.Objects;
using Rogue.ORM.Attributes;
using Rogue.ORM.Interfaces;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Types;
using Rogue.Server.World.Maps.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.Records
{
    [Table("MapObjects")]
    public class MapObjectRecord : ITable, IProtocolable<ProtocolMapObject>
    {
        public static List<MapObjectRecord> MapObjects = new List<MapObjectRecord>();

        [Primary]
        public int Id;

        public int MapId;

        public int CellId;

        public LayerEnum LayerEnum;

        public MapObjectType Type;

        public string VisualData;

        [Ignore]
        public string DefaultVisualData;

        public TriggerTypeEnum TriggerType;

        public string Value1;

        public string Value2;

        public string Value3;

        public bool Walkable;

        [Ignore]
        public Trigger Trigger;

        public MapObjectRecord(int id, int mapId, int cellId, LayerEnum layer,
           MapObjectType type, string visualData, TriggerTypeEnum triggerType, string value1, string value2, string value3, bool walkable)
        {
            this.Id = id;
            this.MapId = mapId;
            this.CellId = cellId;
            this.LayerEnum = layer;
            this.Type = type;
            this.VisualData = visualData;
            this.DefaultVisualData = visualData;
            this.TriggerType = triggerType;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
            this.Walkable = walkable;
        }

        public ProtocolMapObject GetProtocolObject()
        {
            return new ProtocolMapObject(CellId, LayerEnum, Type, VisualData, Walkable);
        }

        public static MapObjectRecord[] GetMapObjects(int mapId)
        {
            return MapObjects.FindAll(x => x.MapId == mapId).ToArray();
        }
    }
}
