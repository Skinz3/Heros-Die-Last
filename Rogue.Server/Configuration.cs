using Rogue.Server.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;
using MonoFramework.Utils;

namespace Rogue.Server
{
    public class Configuration
    {
        static Logger logger = new Logger();

        public const string CONFIG_FILE_NAME = "/config.xml";

        [YAXDontSerialize] // Stack Overflow Already? x)
        public static Configuration Self
        {
            get;
            private set;
        }
        private static string ConfigPath
        {
            get
            {
                return Environment.CurrentDirectory + CONFIG_FILE_NAME;
            }
        }
        public int Port
        {
            get;
            set;
        }
        public int PositionUpdateFrameCount
        {
            get;
            set;
        }
        public bool UseInterpolation
        {
            get;
            set;
        }
        public bool DisplayProtocol
        {
            get;
            set;
        }
        public string MySQLHost
        {
            get;
            set;
        }
        public string MySQLUser
        {
            get;
            set;
        }
        public string MySQLPassword
        {
            get;
            set;
        }
        public string DatabaseName
        {
            get;
            set;
        }

        [StartupInvoke("Configuration", StartupInvokePriority.Primitive)]
        public static void Initialize()
        {
            if (File.Exists(ConfigPath))
            {
                try
                {
                    YAXSerializer serializer = new YAXSerializer(typeof(Configuration));
                    Self = (Configuration)serializer.DeserializeFromFile(ConfigPath);
                    logger.Write("Configuration loaded", MessageState.SUCCES);
                }
                catch (Exception ex)
                {
                    logger.Write("Unable to read config: " + ex, MessageState.ERROR);
                    Console.Read();
                    CreateConfig();

                }

            }
            else
            {
                CreateConfig();
            }
        }
        public static void CreateConfig()
        {
            YAXSerializer serializer = new YAXSerializer(typeof(Configuration));
            Self = Default();
            File.WriteAllText(ConfigPath, serializer.Serialize(Self));
            logger.Write("Configuration Created", MessageState.SUCCES);
        }

        public static Configuration Default()
        {
            return new Configuration()
            {
                Port = 555,
                PositionUpdateFrameCount = 5,
                DisplayProtocol = true,
                MySQLHost = "127.0.0.1",
                MySQLUser = "root",
                MySQLPassword = string.Empty,
                DatabaseName = "rogue",
                UseInterpolation = true,
            };
        }
    }
}
