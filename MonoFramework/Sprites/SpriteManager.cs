using Microsoft.Xna.Framework;
using Rogue.Core.Animations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Sprites
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
           
            foreach (string file in Directory.EnumerateFiles(fullPath, "*.*", SearchOption.AllDirectories))
            {
                AddSprite(file);
            }

           

        }
        private static void AddSprite(string path)
        {
            if (SPRITE_EXTENSIONS.Contains(Path.GetExtension(path)))
                Sprites.Add(Path.GetFileNameWithoutExtension(path), new Sprite(path));
        }

        public static Sprite[] GetSprites()
        {
            return Sprites.Values.ToArray();
        }
        public static Sprite[] GetSprites(string folderName)
        {
            return Array.FindAll(Sprites.Values.ToArray(), x => Path.GetFileName(Path.GetDirectoryName(x.Path)) == folderName);
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
