using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFramework;
using MonoFramework.Scenes;

namespace Rogue.MapEditor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Editor : GameCore
    {
        public Editor()
        {
         //  this.GraphicsDeviceManager.IsFullScreen = true;
           // this.GraphicsDeviceManager.PreferredBackBufferWidth =1920 /2;
      
            SceneManager.LoadScene(new EditorScene());
        }
    }
}
