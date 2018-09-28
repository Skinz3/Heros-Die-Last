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
using Rogue.Server.World.Entities;
using Rogue.Server.World.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Rogue.Server
{
    public class Program
    {
        public static Assembly SERVER_ASSEMBLY = Assembly.GetExecutingAssembly();

        static Logger logger = new Logger();

        public static RogueServer Server;

        public static event Action OnInitialized;

        public static void Run()
        {
            logger.OnStartup(); // affiche le logo ASCII
            StartupManager.Instance.Initialize(SERVER_ASSEMBLY);

            OnInitialized?.Invoke();

            while (true) // Le thread principal n'est plus utilisé (éventuellement des commandes consoles)
            {
                Console.ReadLine();
            }
        }
        static void Main(string[] args)
        {
            Run();
        }

        [StartupInvoke("Protocol", StartupInvokePriority.First)]
        public static void LoadProtocol()
        {
            ProtocolManager.Initialize(Assembly.GetAssembly(typeof(LoadFrameMessage)), SERVER_ASSEMBLY, false);
            logger.Write("Next Message Id: " + ProtocolManager.GetNextMessageId());
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
