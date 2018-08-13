using Microsoft.Xna.Framework;
using Rogue.ORM.Attributes;
using Rogue.Protocol.Enums;
using System;
using System.Collections.Generic;
using MonoFramework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.ORM.Interfaces;
using MonoFramework.IO.Maps;
using MonoFramework.Objects;
using Rogue.Server.World.Maps;

namespace Rogue.Server.Records
{
    [Table("maps")]
    public class MapRecord : ITable
    {
        public const string MAPS_DIRECTORY = "/Maps/";

        public static List<MapRecord> Maps = new List<MapRecord>();

        [Primary]
        public int Id;

        public string MapName;

        public FrameEnum Frame;

        public bool LonlyInstance;

        [Update]
        public List<int> SpawnPositions;

        [Ignore]
        public MapTemplate Template;

        [Ignore]
        public MapGrid Grid;

        public MapRecord(int id, string mapName, FrameEnum frame, bool lonlyInstance, List<int> spawnPositions)
        {
            this.Id = id;
            this.MapName = mapName;
            this.Frame = frame;
            this.LonlyInstance = lonlyInstance;
            this.SpawnPositions = spawnPositions;
        }

        public void Initialize()
        {
            Template = new MapTemplate();
            Template.Load(Environment.CurrentDirectory + MAPS_DIRECTORY + MapName + ".map");

            Grid = new MapGrid(Template);
            Grid.Load();
        }

        public int RandomSpawnCellId()
        {
            return SpawnPositions.Random();
        }
        public static MapRecord[] GetMaps(FrameEnum frameEnum)
        {
            return Maps.FindAll(x => x.Frame == frameEnum).ToArray();
        }
        public static MapRecord GetMap(string sceneName)
        {
            return Maps.Find(x => x.MapName == sceneName);
        }
        public static MapRecord GetMap(int id)
        {
            return Maps.Find(x => x.Id == id);
        }
    }
}
