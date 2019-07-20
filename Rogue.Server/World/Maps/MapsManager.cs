using Rogue.Server.Network;
using Rogue.Server.Records;
using Rogue.Server.Utils;
using Rogue.Server.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core.Utils;

namespace Rogue.Server.World.Maps
{
    public class MapsManager
    {
        public const int MAX_PLAYER_PER_MAP = 50;

        public static event Action<MapInstance> OnInstanceCreated;

        public static Dictionary<MapRecord, List<MapInstance>> Instances;

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
        [StartupInvoke("Spawning Monsters", StartupInvokePriority.Ninth)]
        public static void SpawnMonsters()
        {
            MapRecord targetMap = MapRecord.GetMap("donjon");

            if (targetMap != null)
            {
                var monster = new Monster(EntityRecord.GetEntity("Slime"), (MapCell)targetMap.Grid.GetCell(6, 17));
                MapsManager.JoinFreeMapInstance(monster, targetMap);

                monster = new Monster(EntityRecord.GetEntity("Slime"), (MapCell)targetMap.Grid.GetCell(5, 17));
                MapsManager.JoinFreeMapInstance(monster, targetMap);


                monster = new Monster(EntityRecord.GetEntity("Slime"), (MapCell)targetMap.Grid.GetCell(7, 17));
                MapsManager.JoinFreeMapInstance(monster, targetMap);

            }

            targetMap = MapRecord.GetMap("Map de test 1");

            if (targetMap != null)
            {
               
                var monster = new Monster(EntityRecord.GetEntity("Slime"), (MapCell)targetMap.Grid.GetCell(306));
                MapsManager.JoinFreeMapInstance(monster, targetMap);

                monster = new Monster(EntityRecord.GetEntity("Slime"), (MapCell)targetMap.Grid.GetCell(288));
                MapsManager.JoinFreeMapInstance(monster, targetMap);

                monster = new Monster(EntityRecord.GetEntity("Slime"), (MapCell)targetMap.Grid.GetCell(234));
                MapsManager.JoinFreeMapInstance(monster, targetMap);

                monster = new Monster(EntityRecord.GetEntity("Slime"), (MapCell)targetMap.Grid.GetCell(93));
                MapsManager.JoinFreeMapInstance(monster, targetMap);

                monster = new Monster(EntityRecord.GetEntity("Slime"), (MapCell)targetMap.Grid.GetCell(109));
                MapsManager.JoinFreeMapInstance(monster, targetMap);

            }


        }
        private static MapInstance FindMapInstance(MapRecord map)
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
        public static void JoinFreeMapInstance(MovableEntity entity, string targetMap)
        {
            JoinFreeMapInstance(entity, MapRecord.GetMap(targetMap));
        }
        public static void JoinFreeMapInstance(MovableEntity entity, MapRecord targetMap)
        {
            MapInstance targetInstance = FindMapInstance(targetMap);
            entity.DefineMapInstance(targetInstance);
        }
        public static MapInstance CreateMapInstance(MapRecord map)
        {
            MapInstance instance = new MapInstance(uniqueIdProvider.GetNextId(), map);
            Instances[map].Add(instance);
            OnInstanceCreated?.Invoke(instance);
            return instance;
        }

        public static void RemoveMapInstance(MapInstance mapInstance)
        {
            mapInstance.Dispose();
            Instances[mapInstance.Record].Remove(mapInstance);
        }
    }
}
