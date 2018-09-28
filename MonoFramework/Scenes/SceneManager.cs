using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoFramework.Scenes
{
    public class SceneManager
    {
        public static Dictionary<string, Type> Scenes = new Dictionary<string, Type>();

        public static void Initialize(Assembly sceneAssembly)
        {
            foreach (var type in sceneAssembly.GetTypes().Where(x => x.IsAbstract == false && x.InheritsFrom(typeof(Scene))))
            {
                Scenes.Add(type.Name, type);
            }
        }
        public static Scene GetScene(string name)
        {
            return (Scene)Activator.CreateInstance(Scenes[name]);
        }


        public static Scene CurrentScene
        {
            get;
            private set;
        }

        public static event Action<Scene> OnSceneLoaded;

        public static T GetCurrentScene<T>() where T : Scene
        {
            return CurrentScene as T;
        }
        public static void LoadScene(string name)
        {
            Scene scene = GetScene(name);
            LoadScene(scene);
        }
        public static void LoadScene(Scene scene)
        {
            CurrentScene?.Dispose();
            CurrentScene = scene;
            InitializeScene();
        }

        public static void InitializeScene()
        {
            CurrentScene.Initialize();
            OnSceneLoaded?.Invoke(CurrentScene);
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
