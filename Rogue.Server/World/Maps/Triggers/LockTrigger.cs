using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;

namespace Rogue.Server.World.Maps.Triggers
{
    /// <summary>
    /// Value1 = TargetMapObjectId
    /// Value2 = MySpriteLocked
    /// </summary>
    public class LockTrigger : Trigger
    {
        private bool Locked
        {
            get;
            set;
        }
        private MapObjectRecord TargetMapObject
        {
            get;
            set;
        }
        public LockTrigger(MapInstance mapInstance, MapObjectRecord mapObject) : base(mapInstance, mapObject)
        {
            this.Locked = true;
            this.TargetMapObject = mapInstance.GetMapObject(int.Parse(mapObject.Value1));
        }
        public override void OnClicked(Player player)
        {
            Locked = !Locked;

            if (!Locked)
            {
                MapInstance.UpdateMapObject(this.MapObjectRecord, MapObjectRecord.Value2);
                MapInstance.RemoveMapObject(TargetMapObject);
            }
            else
            {
                MapInstance.UpdateMapObject(this.MapObjectRecord, MapObjectRecord.DefaultVisualData);
                MapInstance.AddMapObject(TargetMapObject);
            }
        }

        public override void Update(long deltaTime)
        {
           
        }
    }
}
