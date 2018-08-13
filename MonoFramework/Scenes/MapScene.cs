using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework.IO.Maps;
using MonoFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Scenes
{
    public abstract class MapScene : Scene
    {
        public GMap Map
        {
            get;
            private set;
        }
        protected MapTemplate MapTemplate
        {
            get;
            private set;
        }
        public void LoadMap(string path)
        {
            MapTemplate = new MapTemplate();
            MapTemplate.Load(path);
            Map = new GMap(new Point(MapTemplate.Width, MapTemplate.Height));
            Map.Initialize();
            Map.Load(MapTemplate);
            OnMapLoaded();
        }
        public abstract void OnMapLoaded();
        public override void Update(GameTime gameTime)
        {
            Map.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Debug.GraphicsDevice.Clear(ClearColor);

            Debug.SpriteBatch.Begin(SpriteSortMode.Immediate,
                        BlendState.AlphaBlend, SamplerState.PointClamp,
                        null,
                        null,
                        null,
                        Camera.GetTransformation());

            foreach (var pair in GameObjects)
            {
                Map.DrawLayer(gameTime, pair.Key);

                foreach (var gameObject in pair.Value)
                {
                    gameObject.Draw(gameTime);
                }
            }
            Debug.SpriteBatch.End();

            Debug.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null,
               null, null, null);

            foreach (var gameObject in UIGameObjects)
            {
                gameObject.Draw(gameTime);
            }
            Debug.SpriteBatch.End();

        }
    }
}
