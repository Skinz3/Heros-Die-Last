using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue.Core.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Objects
{
    public class TextRenderer
    {
        private Dictionary<string, SpriteFont> SpriteFonts
        {
            get;
            set;
        }

        public TextRenderer()
        {
            this.SpriteFonts = new Dictionary<string, SpriteFont>();
        }

        /// <summary>
        /// Add the GText object to the scene ! no need to call Scene.AddObject() !
        /// </summary>
        /// <returns></returns>
        public GText AddText(Vector2 position, string text, Color color, float scale = 1f)
        {
            return AddText(position, text, color, SceneManager.CurrentScene.DefaultFontName, scale);
        }
        /// <summary>
        /// Add the GText object to the scene ! no need to call Scene.AddObject() !
        /// </summary>
        /// <returns></returns>
        public GText AddText(Vector2 position, string text, Color color, string fontName, float scale = 1f)
        {
            GText gText = new GText(position, GetSpriteFont(fontName), text, color, scale);
            SceneManager.CurrentScene.AddObject(gText, LayerEnum.UI);
            return gText;
        }
        public SpriteFont GetDefaultSpriteFont()
        {
            return GetSpriteFont(SceneManager.CurrentScene.DefaultFontName);
        }
        public SpriteFont GetSpriteFont(string name)
        {
            if (SpriteFonts.ContainsKey(name))
            {
                return SpriteFonts[name];
            }
            else
            {
                var spriteFont = Debug.Content.Load<SpriteFont>(name);
                SpriteFonts.Add(name, spriteFont);
                return spriteFont;
            }
        }
        public void Dispose()
        {
            SpriteFonts = null;
        }
    }
}
