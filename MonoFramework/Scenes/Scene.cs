using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework.Cameras;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Scenes
{
    public abstract class Scene
    {
        public event Action OnIntializationFinalized;

        public bool Initialized = false;

        public Camera2D Camera
        {
            get;
            private set;
        }
        /// <summary>
        /// Represente des objets dessinable et la couche sur la quelle ils se trouvent
        /// </summary>
        public Dictionary<LayerEnum, List<GameObject>> GameObjects
        {
            get;
            private set;
        }
        public List<GameObject> UIGameObjects
        {
            get;
            private set;
        }

        public abstract bool HandleCameraInput
        {
            get;
        }
        public abstract Color ClearColor
        {
            get;
        }
        public TextRenderer TextRenderer
        {
            get;
            private set;
        }
        public abstract string DefaultFontName
        {
            get;
        }
        public Scene()
        {
            this.Camera = new Camera2D();
            this.UIGameObjects = new List<GameObject>();
            this.GameObjects = new Dictionary<LayerEnum, List<GameObject>>()
            {
                { LayerEnum.First, new List<GameObject>() },
                { LayerEnum.Second, new List<GameObject>() },
                { LayerEnum.Third, new List<GameObject>() },
                { LayerEnum.UI,new List<GameObject>() },
            };
            this.TextRenderer = new TextRenderer();
        }
        public void Initialize()
        {
            OnInitialize();
            foreach (var list in GameObjects.Values)
            {
                foreach (var gameObject in list)
                {
                    gameObject.Initialize();
                }
            }
            foreach (var gameObject in UIGameObjects)
            {
                gameObject.Initialize();
            }
            OnInitializeComplete();
            OnIntializationFinalized?.Invoke();
            Initialized = true;
        }
        /// <summary>
        /// On ajoute les elements a la scène
        /// </summary>
        public abstract void OnInitialize();
        /// <summary>
        /// Lorsque les élements on été ajoutés & initialisés
        /// </summary>
        public abstract void OnInitializeComplete();


        public void AddObject(GameObject gameObject, LayerEnum layer)
        {
            gameObject.Layer = layer;

            if (layer == LayerEnum.UI)
                UIGameObjects.Add(gameObject);
            else
                this.GameObjects[layer].Add(gameObject);

        }
        public void RemoveObject(GameObject gameObject)
        {
            if (gameObject.Layer == LayerEnum.UI)
                UIGameObjects.Remove(gameObject);
            else
                this.GameObjects[gameObject.Layer].Remove(gameObject);
        }
        public virtual void Dispose()
        {
            foreach (var list in GameObjects.Values)
            {
                foreach (var gameObject in list)
                {
                    gameObject.Dispose();
                }
            }
            foreach (var gameObject in UIGameObjects)
            {
                gameObject.Dispose();
            }
            GameObjects.Clear();
            UIGameObjects.Clear();
            TextRenderer.Dispose();
            OnDispose();
        }

        public abstract void OnDispose();

        /// <summary>
        /// Remove virtual while test finished
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            Camera.Update();

            foreach (var list in GameObjects.Values)
            {
                foreach (var gameObject in list.ToArray())
                {
                    gameObject.Update(gameTime);
                }
            }
            foreach (var gameObject in UIGameObjects.ToArray())
            {
                gameObject.Update(gameTime);
            }
        }
        protected virtual void DrawUI(GameTime gameTime)
        {
            Debug.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null,
               null, null, null);

            foreach (var gameObject in UIGameObjects)
            {
                gameObject.Draw(gameTime);
            }
            Debug.SpriteBatch.End();
        }
        protected virtual void DrawSceneObjects(GameTime gameTime)
        {

            Debug.SpriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend, SamplerState.PointClamp,
                        null,
                        null,
                        null,
                        Camera.GetTransformation());
            foreach (var list in GameObjects.Values)
            {
                foreach (var gameObject in list)
                {
                    gameObject.Draw(gameTime);
                }
            }
            Debug.SpriteBatch.End();


        }

        public virtual void Draw(GameTime gameTime)
        {
            Debug.GraphicsDevice.Clear(ClearColor);
            DrawSceneObjects(gameTime);
            DrawUI(gameTime);
        }
    }
}
