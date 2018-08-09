using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework.Input;
using MonoFramework.Scenes;
using MonoFramework.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework
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
        protected SpriteBatch SpriteBatch
        {
            get;
            private set;
        }
        public GameCore()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            SpriteManager.Initialize(@"/Content");
            GraphicsDeviceManager.PreferredBackBufferWidth = 1000;  // largeur de la fenêtre
            GraphicsDeviceManager.PreferredBackBufferHeight = 800; // hauteur de la fenêtre
            this.IsMouseVisible = true;

           
           //this.GraphicsDeviceManager.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice); // Le SpriteBatch permet de dessiner a l'écran
            Debug.Initialize(SpriteBatch,Content); // Charge toute les variable statiques
        }

        protected override void UnloadContent()
        {
            SceneManager.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardManager.Update();
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
