using LiteNetLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFramework;
using MonoFramework.Scenes;

namespace Rogue
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : GameCore
    {
        public Game1()
        {
            SceneManager.SetScene(new TestScene());
        }
    }
}
