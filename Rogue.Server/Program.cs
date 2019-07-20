using Microsoft.Xna.Framework;
using Rogue.Core.Collisions;
using Rogue.Core.DesignPattern;
using Rogue.Core.IO.Maps;
using Rogue.Core.Network.Protocol;
using Rogue.Core.Utils;
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Rogue.Server
{
    /// <summary>
    /// Server should also Poll network events? threadsafely? one timer for all the server???
    /// </summary>
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

            manager.UseProvider();
            manager.LoadTables();
        }

        [InDeveloppement(InDeveloppementState.TEMPORARY)]
        [StartupInvoke("Synchronize Map", StartupInvokePriority.Primitive)]
        public static void SynchronizeMaps()
        {
            string path = @"C:\Users\Skinz\Desktop\Heros-Die-Last\Rogue\bin\DesktopGL\AnyCPU\Debug\Maps";

            foreach (var file in Directory.GetFiles(Environment.CurrentDirectory + MapRecord.MAPS_DIRECTORY))
            {
                File.Delete(file);
            }

            foreach (var file in Directory.GetFiles(path))
            {
                var dest = Environment.CurrentDirectory + MapRecord.MAPS_DIRECTORY + Path.GetFileName(file);
                File.Copy(file, dest);
            }
        }

        [StartupInvoke("Server", StartupInvokePriority.Last)]
        public static void StartServer()
        {
            Server = new RogueServer();
            Server.Start(Configuration.Self.Port);
        }
    }
}
