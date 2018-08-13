using MonoFramework.Utils;
using Rogue.Server.Records;
using Rogue.Server.Utils;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server.World.Maps
{

    public class MapsManager
    {
        public const int MAX_PLAYER_PER_MAP = 50;

        private static Dictionary<MapRecord, List<MapInstance>> Instances;

        private static UniqueIdProvider uniqueIdProvider;

        [StartupInvoke("MapsManager", StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            Instances = new Dictionary<MapRecord, List<MapInstance>>();
            uniqueIdProvider = new UniqueIdProvider(0);

            foreach (var map in MapRecord.Maps)
            {
                map.Initialize();
                Instances.Add(map, new List<MapInstance>());
            }
        }

        public static MapInstance FindMapInstance(Player player, MapRecord map)
        {
            List<MapInstance> instances = Instances[map];

            MapInstance target = instances.FirstOrDefault(x => x.PlayersCount < MAX_PLAYER_PER_MAP);

            if (instances.Count > 0 && target != null)
            {
                return target;
            }
            else
            {
                return CreateMapInstance(map);
            }
        }
        public static MapInstance CreateMapInstance(MapRecord map)
        {
            MapInstance instance = new MapInstance(uniqueIdProvider.GetNextId(), map);
            Instances[map].Add(instance);
            return instance;
        }

        public static void RemoveMapInstance(MapInstance mapInstance)
        {
            Instances[mapInstance.Record].Remove(mapInstance);
        }
    }
}
