using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue.Core.Input;
using Rogue.Core.Scenes;
using Rogue.Core.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core
{
    /// <summary>
    /// Représente une fenêtre de jeu, vide.
    /// 
    /// utilisation: MonJeu : GameCore
    /// </summary>
    public abstract class GameCore : Game
    {
        protected GraphicsDeviceManager GraphicsDeviceManager
        {
            get;
            private set;
        }
        public SpriteBatch SpriteBatch
        {
            get;
            private set;
        }
        public GameCore()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            SpriteManager.Initialize(@"/Content");
            SceneManager.Initialize(Assembly.GetEntryAssembly());
            GraphicsDeviceManager.PreferredBackBufferWidth = 1000;  // largeur de la fenêtre
            GraphicsDeviceManager.PreferredBackBufferHeight = 800; // hauteur de la fenêtre
            this.IsMouseVisible = true;

            /* GraphicsDeviceManager.PreferredBackBufferWidth = 1920;
             GraphicsDeviceManager.PreferredBackBufferHeight = 1080;
             this.GraphicsDeviceManager.IsFullScreen = true;*/
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice); // Le SpriteBatch permet de dessiner a l'écran
            Debug.Initialize(this, Content); // Charge toute les variable statiques
        }

        protected override void UnloadContent()
        {
            SceneManager.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardManager.Update();
            MouseManager.Update();
            SceneManager.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {

            SceneManager.Draw(gameTime);
            base.Draw(gameTime);

        }
    }
}
