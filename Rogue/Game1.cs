using LiteNetLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rogue;
using Rogue.Scenes;
using Rogue.Network;
using System;
using Rogue.Core;
using Rogue.Core.Input;
using Rogue.Core.Scenes;
using Rogue.Animations;
using Rogue.Core.Animations;
using Rogue.Core.Utils;

namespace Rogue
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : GameCore
    {
        FrameCounter frameCounter = new FrameCounter();

        public Game1()
        {
            Configuration.Initialize();

            KeyboardManager.OnKeyPressed += OnKeyPressed;

            if (Configuration.Self.FullScreen)
            {
                GraphicsDeviceManager.PreferredBackBufferWidth = 1920;
                GraphicsDeviceManager.PreferredBackBufferHeight = 1080;
                this.GraphicsDeviceManager.IsFullScreen = true;
            }
        }
        protected override void Initialize()
        {
            base.Initialize();


        }
        protected override void LoadContent()
        {
            base.LoadContent();
            AnimationManager.Initialize();
            SceneManager.LoadScene(new LoginScene());



        }
        protected override void Dispose(bool disposing)
        {
            KeyboardManager.OnKeyPressed -= OnKeyPressed;
            ClientHost.DestroyClient();
            base.Dispose(disposing);
        }
        private void OnKeyPressed(Keys obj)
        {
            if (obj == Keys.Escape)
            {
                if (!(SceneManager.CurrentScene is LoginScene))
                {
                    ClientHost.DestroyClient();
                    SceneManager.LoadScene(new LoginScene());
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
            frameCounter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            Console.Title = "Rogue FPS: " + Math.Round(frameCounter.AverageFramesPerSecond);


            ClientHost.Client?.PollEvents();
            base.Update(gameTime);
        }
    }
}
