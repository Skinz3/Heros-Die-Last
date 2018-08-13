using LiteNetLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFramework;
using MonoFramework.Input;
using MonoFramework.Scenes;
using Rogue.Network;
using Rogue.Scenes;

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
            SceneManager.LoadScene(new LoginScene());

        }
        protected override void Dispose(bool disposing)
        {
            KeyboardManager.OnKeyPressed -= OnKeyPressed;
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
