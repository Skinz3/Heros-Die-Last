using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Server.Records;
using Rogue.Server.World.Entities;

namespace Rogue.Server.World.Maps.Triggers
{
    public class TeleportTrigger : Trigger
    {
        public TeleportTrigger(MapInstance mapInstance, MapObjectRecord mapObject) : base(mapInstance, mapObject)
        {
        }

        public override void OnClicked(Player player)
        {
            player.Teleport(MapObjectRecord.Value1);
        }

        public override void Update(long deltaTime)
        {
           
        }
    }
}
