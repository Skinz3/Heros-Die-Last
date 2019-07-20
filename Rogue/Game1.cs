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

namespace Rogue
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : GameCore
    {
        public Game1()
        {
            KeyboardManager.OnKeyPressed += OnKeyPressed;
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
            ClientHost.Client?.PollEvents();
            base.Update(gameTime);
        }
    }
}
