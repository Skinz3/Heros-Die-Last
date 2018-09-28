using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Protocol.Enums;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;

namespace Rogue.Server.World.Maps.Triggers
{
    /// <summary>
    /// Value1 : VisualDataActive
    /// Value2 : Trigger Delay
    /// Value3 : DamageInflicted
    /// </summary>
    public class SpikesTrigger : Trigger
    {
        public const int HIT_DELAY = 20;

        private bool Active
        {
            get;
            set;
        }
        private MapCell Cell
        {
            get;
            set;
        }
        private int DamageInflicted
        {
            get;
            set;
        }

        private readonly int TriggerDelay;

        private int TriggerDelayCurrent
        {
            get;
            set;
        }
        public SpikesTrigger(MapInstance mapInstance, MapObjectRecord mapObject) : base(mapInstance, mapObject)
        {
            this.Active = false;
            this.Cell = mapInstance.Record.Grid.GetCell<MapCell>(mapObject.CellId);
            this.DamageInflicted = int.Parse(mapObject.Value3);
            this.TriggerDelay = int.Parse(mapObject.Value2);
            this.TriggerDelayCurrent = TriggerDelay;

        }

        public override void OnClicked(Player player)
        {

        }

        public int HitDelayCurrent
        {
            get;
            private set;
        }

        public override void Update(long deltaTime)
        {
            TriggerDelayCurrent--;

            if (TriggerDelayCurrent <= 0)
            {
                Active = !Active;
                TriggerDelayCurrent = TriggerDelay;

                if (Active)
                {
                    HitDelayCurrent = 0;
                    MapInstance.UpdateMapObject(MapObjectRecord, MapObjectRecord.Value1);

                }
                else
                {
                    MapInstance.UpdateMapObject(MapObjectRecord, MapObjectRecord.DefaultVisualData);
                }
            }


            if (Active)
            {
                HitDelayCurrent--;

                if (HitDelayCurrent <= 0)
                {
                    var entities = MapInstance.GetEntities<MovableEntity>();

                    foreach (var entity in entities)
                    {
                        if (entity.GetHitBox().Intersects(Cell.Rectangle))
                        {
                            entity.InflictDamage(entity,DamageInflicted);
                        }
                    }
                    HitDelayCurrent = HIT_DELAY;
                }
            }
        }
    }
}