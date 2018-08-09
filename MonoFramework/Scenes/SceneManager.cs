using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoFramework.Scenes
{
    public class SceneManager
    {
        public static Scene CurrentScene
        {
            get;
            private set;
        }
        public static void SetScene(Scene scene)
        {
            CurrentScene?.Dispose();
            CurrentScene = scene;
            Initialize();
        }

        public static void Initialize()
        {
            CurrentScene.Initialize();
        }

        public static void Dispose()
        {
            CurrentScene?.Dispose();
        }

        public static void Draw(GameTime gameTime)
        {
            CurrentScene.Draw(gameTime);
        }

        public static void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }
    }
}
