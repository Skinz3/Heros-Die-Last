using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Sprites
{
    public class SpriteManager
    {
        public static string[] SPRITE_EXTENSIONS = new string[]
        {
            ".png",
            ".jpg",
            ".bmp",
            ".gif",
        };
        private static Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();

        /// <summary>
        /// On charge les sprites (pas leur texture, seulement la texture lorsqu'on en a besoin, pour minimiser les calculs)
        /// </summary>
        /// <param name="path"></param>
        public static void Initialize(string path)
        {
            string fullPath = Environment.CurrentDirectory + path;

            if (File.Exists(fullPath) == false)
            {
                Directory.CreateDirectory(fullPath);
            }
            foreach (var file in Directory.GetFiles(fullPath))  
            {
                if (SPRITE_EXTENSIONS.Contains(Path.GetExtension(file)))
                    Sprites.Add(Path.GetFileNameWithoutExtension(file), new Sprite(file));
            }
        }
        public static Sprite[] GetSprites()
        {
            return Sprites.Values.ToArray();
        }
        public static int SpritesLenght
        {
            get
            {
                return Sprites.Count();
            }
        }
        /// <summary>
        /// !! Load The Sprite !!! 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Sprite GetSprite(string name)
        {
            if (!Sprites.ContainsKey(name))
            {
                return null;
            }

            Sprite result = Sprites[name];

            if (!result.Loaded)
                result.Load();

            return result;
        }

    }
}
