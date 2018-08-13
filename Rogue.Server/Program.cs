using Microsoft.Xna.Framework;
using MonoFramework.Collisions;
using MonoFramework.Network.Protocol;
using MonoFramework.Utils;
using Rogue.ORM;
using Rogue.Protocol;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Server;
using Rogue.Protocol.Types;
using Rogue.Server.Auth;
using Rogue.Server.Network;
using Rogue.Server.Records;
using Rogue.Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server
{
    class Program
    {
        static Assembly SERVER_ASSEMBLY = Assembly.GetExecutingAssembly();

        static Logger logger = new Logger();

        public static RogueServer Server;

        static void Main(string[] args)
        {
            logger.OnStartup(); // affiche le logo ASCII

            StartupManager.Instance.Initialize(SERVER_ASSEMBLY);

            while (true) // Le thread principal n'est plus utilisé (éventuellement des commandes consoles)
            {
                Console.ReadLine();
               // CreatePlayer();

            }
        }
        private static void CreatePlayer()
        {
            List<DirectionalAnimation> animations = new List<DirectionalAnimation>();

            animations.Add(new DirectionalAnimation(DirectionEnum.Down, new string[] { "sprite_hero06", "sprite_hero07", "sprite_hero08", "sprite_hero09" }, 150f));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right, new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f));
            animations.Add(new DirectionalAnimation(DirectionEnum.Up, new string[] { "sprite_hero14", "sprite_hero15", "sprite_hero16", "sprite_hero17" }, 150f));
            animations.Add(new DirectionalAnimation(DirectionEnum.None, new string[] { "sprite_hero00", "sprite_hero01", "sprite_hero02", "sprite_hero01" }, 200f));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left, new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f, false, true));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Up, new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Down, new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f));

            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Up, new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f, false, true));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Down, new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f, false, true));

            EntityRecord record = new EntityRecord("Default", 48 * 3, 48 * 3, animations.ToArray());


            record.AddInstantElement();
            logger.Write("Player Created");
        }
        [StartupInvoke("Protocol", StartupInvokePriority.First)]
        public static void LoadProtocol()
        {
            ProtocolManager.Initialize(Assembly.GetAssembly(typeof(LoadFrameMessage)), SERVER_ASSEMBLY, true);
        }
        [StartupInvoke("MySql", StartupInvokePriority.First)]
        public static void LoadMySql()
        {
            DatabaseManager manager = new DatabaseManager(SERVER_ASSEMBLY,
               Configuration.Self.MySQLHost, Configuration.Self.DatabaseName,
               Configuration.Self.MySQLUser, Configuration.Self.MySQLPassword);

            DatabaseManager.GetInstance().CreateTable(typeof(EntityRecord));

            manager.UseProvider();
            manager.LoadTables();


        }
        [StartupInvoke("Server", StartupInvokePriority.Last)]
        public static void StartServer()
        {
            Server = new RogueServer();
            Server.Start(Configuration.Self.Port);
        }
    }
}
