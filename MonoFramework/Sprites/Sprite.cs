using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework.DesignPattern;
using MonoFramework.Objects;
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
    public class Sprite : ICellElement
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
        public bool FlippedVertically
        {
            get;
            private set;
        }
        public bool FlippedHorizontally
        {
            get;
            private set;
        }
        public Sprite(string path)
        {
            this.Path = path;
            this.FlippedVertically = false;
            this.FlippedHorizontally = false;
        }
        public Sprite(string path, Texture2D texture, bool flippedVertically, bool flippedHorizontally)
        {
            this.Path = path;
            this.Texture = texture;
            this.FlippedVertically = flippedVertically;
            this.FlippedHorizontally = flippedHorizontally;
        }

        public void Dispose()
        {
            //  Texture?.Dispose(); Sprite Garabage Collector needed!
            //  Texture = null;
        }
        [Obsolete("Remove this when developpement is finished")]
        bool WaitForFile(string fullPath)
        {
            int numTries = 0;
            while (true)
            {
                ++numTries;
                try
                {
                    // Attempt to open the file exclusively.
                    using (FileStream fs = new FileStream(fullPath,
                        FileMode.Open, FileAccess.ReadWrite,
                        FileShare.None, 100))
                    {
                        fs.ReadByte();

                        // If we got this far the file is ready
                        break;
                    }
                }
                catch 
                {
                    if (numTries > 10)
                    {
                        return false;
                    }

                    // Wait for the lock to be released
                    System.Threading.Thread.Sleep(500);
                }
            }

            return true;
        }
        public void Load()
        {
            WaitForFile(Path); // Permit multiple instances of the game. (not allowed)
            FileStream stream = new FileStream(Path, FileMode.Open);
            Texture = Texture2D.FromStream(Debug.GraphicsDevice, stream);
            stream.Dispose();
        }
        public void Draw(Rectangle rect, Color color)
        {
            Debug.SpriteBatch.Draw(Texture, rect, color);
        }
        public static Sprite Flip(Sprite input, bool vertical, bool horizontal)
        {
            if (!vertical && !horizontal)
            {
                return input;
            }
            Texture2D flipped = new Texture2D(input.Texture.GraphicsDevice, input.Texture.Width, input.Texture.Height);
            Color[] data = new Color[input.Texture.Width * input.Texture.Height];
            Color[] flipped_data = new Color[data.Length];

            input.Texture.GetData(data);

            for (int x = 0; x < input.Texture.Width; x++)
            {
                for (int y = 0; y < input.Texture.Height; y++)
                {
                    int index = 0;
                    if (horizontal && vertical)
                        index = input.Texture.Width - 1 - x + (input.Texture.Height - 1 - y) * input.Texture.Width;
                    else if (horizontal && !vertical)
                        index = input.Texture.Width - 1 - x + y * input.Texture.Width;
                    else if (!horizontal && vertical)
                        index = x + (input.Texture.Height - 1 - y) * input.Texture.Width;
                    else if (!horizontal && !vertical)
                        index = x + y * input.Texture.Width;

                    flipped_data[x + y * input.Texture.Width] = data[index];
                }
            }

            flipped.SetData(flipped_data);

            vertical = vertical ? !input.FlippedVertically : input.FlippedVertically;
            horizontal = horizontal ? !input.FlippedHorizontally : input.FlippedHorizontally;
            return new Sprite(input.Path, flipped, vertical, horizontal);
        }

        public void Update(GameTime time)
        {

        }
    }
}
