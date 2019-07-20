using Rogue.Core.DesignPattern;
using Rogue.Core.Objects;
using Rogue.Protocol.Enums;
using Rogue.Server.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Maps.Triggers
{
    class TriggerManager
    {
        [InDeveloppement]
        public static Trigger GetTrigger(MapInstance mapInstance, MapInteractiveRecord record)
        {
            switch (record.TriggerType)
            {
                case TriggerTypeEnum.Locker:
                    return new LockTrigger(mapInstance, record);
                case TriggerTypeEnum.Teleport:
                    return new TeleportTrigger(mapInstance, record);
               case TriggerTypeEnum.Chest:
                    return new ChestTrigger(mapInstance, record); 
             /*   case TriggerTypeEnum.Spikes:
                    return new SpikesTrigger(mapInstance, record); */
            }

            return null;
        }
    }
}
