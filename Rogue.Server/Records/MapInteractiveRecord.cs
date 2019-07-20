using Rogue.Core.Objects;
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
    [Table("MapInteractives")]
    public class MapInteractiveRecord : ITable
    {
        public static List<MapInteractiveRecord> MapObjects = new List<MapInteractiveRecord>();

        [Primary]
        public int Id;

        public int MapId;

        public int CellId;

        public LayerEnum LayerEnum;

        public TriggerTypeEnum TriggerType;

        public string Value1;

        public string Value2;

        public string Value3;

        public string Value4;

        public string Value5;

        public string Value6;

        public string Value7;

        [Ignore]
        public Trigger Trigger;

        public MapInteractiveRecord(int id, int mapId, int cellId, LayerEnum layer,
          TriggerTypeEnum triggerType, string value1, string value2, string value3, string value4, string value5, string value6, string value7)
        {
            this.Id = id;
            this.MapId = mapId;
            this.CellId = cellId;
            this.LayerEnum = layer;
            this.TriggerType = triggerType;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
            this.Value4 = value4;
            this.Value5 = value5;
            this.Value6 = value6;
            this.Value7 = value7;
        }


        public static MapInteractiveRecord[] GetMapObjects(int mapId)
        {
            return MapObjects.FindAll(x => x.MapId == mapId).ToArray();
        }
    }
}
