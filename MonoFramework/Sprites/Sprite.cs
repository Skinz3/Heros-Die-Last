using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Sprites
{
    /// <summary>
    /// Représente un fichier de texture en 2D. (png,jpg,bmp,gif...)
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// La texture a elle été chargée?
        /// </summary>
        public bool Loaded
        {
            get
            {
                return Texture != null;
            }
        }
        /// <summary>
        /// Emplacement sur le disque
        /// </summary>
        public string Path
        {
            get;
            set;
        }
        public Texture2D Texture
        {
            get;
            set;
        }
        public string Name
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(Path);
            }
        }

        public Sprite(string path)
        {
            this.Path = path;
        }
        public void Dispose()
        {
            Texture.Dispose();
            Texture = null;
        }
        public void Load()
        {
            FileStream stream = new FileStream(Path, FileMode.Open);
            Texture = Texture2D.FromStream(Debug.GraphicsDevice, stream);

            
        }

        public void Draw(Rectangle rect, Color color)
        {
            Debug.SpriteBatch.Draw(Texture, rect, color);
        }
    }
}
