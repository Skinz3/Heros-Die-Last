using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Animator
{
    class Utils
    {
        public const string SPRITE_PATH = @"C:\Users\Skinz\Desktop\Heros-Die-Last\Rogue\bin\DesktopGL\AnyCPU\Debug\Content";

        public const string DEFAULT_SAVE_PATH = @"C:\Users\Skinz\Desktop\Heros-Die-Last\Rogue\bin\DesktopGL\AnyCPU\Debug\Animations\";

        public static string GetSpritePath(string spriteName)
        {
            string[] files = Directory.GetFiles(SPRITE_PATH,
            "*.*",
            SearchOption.AllDirectories);

            foreach (var file in files)
            {
                if (System.IO.Path.GetFileNameWithoutExtension(file) == spriteName)
                    return file;
            }
            throw new FileNotFoundException();
        }
    }
}
