using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rogue.Core;
using Rogue.Core.Scenes;
using Rogue.ServerView.Scenes;

namespace Rogue.ServerView
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : GameCore
    {
        protected override void LoadContent()
        {
            SceneManager.LoadScene(new View());
            base.LoadContent();
        }
    }
}
