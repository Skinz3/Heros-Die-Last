using Rogue.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YAXLib;

namespace Rogue.WorldEditor
{
    public class Configuration
    {
        public const string CONFIG_FILE_NAME = "/config.xml";

        [YAXDontSerialize]
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

        public string ContentPath
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

                    if (Directory.Exists(Self.ContentPath) == false)
                    {
                        CreateConfig();
                    }
                }
                catch
                {
                    Console.Read();
                    CreateConfig();
                }

            }
            else
            {
                CreateConfig();
            }
        }

        public static string GetTilesPath()
        {
            return Path.Combine(Configuration.Self.ContentPath, @"Content\Tiles");
        }
        public static string GetSpritesPath()
        {
            return Path.Combine(Configuration.Self.ContentPath, @"Content\");
        }
        public static string GetAnimationsPath()
        {
            return Path.Combine(Configuration.Self.ContentPath, @"Animations\");
        }

        public static void CreateConfig()
        {
            MessageBox.Show("Please select content directory.", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            var folderBrowserDialog = new FolderBrowserDialog();
            var result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                YAXSerializer serializer = new YAXSerializer(typeof(Configuration));
                Self = new Configuration()
                {
                    ContentPath = folderBrowserDialog.SelectedPath,
                };
                File.WriteAllText(ConfigPath, serializer.Serialize(Self));
            }
            else
            {
                CreateConfig();
            }
        }

    }
}
