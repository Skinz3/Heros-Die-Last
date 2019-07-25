using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;
using Rogue.Core.Utils;

namespace Rogue
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
        public string ServerIp
        {
            get;
            set;
        }
        public int ServerPort
        {
            get;
            set;
        }
        public bool FullScreen
        {
            get;
            set;
        }

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
                ServerIp = "127.0.0.1",
                ServerPort = 555,
                FullScreen = false,
            };
        }
    }
}
