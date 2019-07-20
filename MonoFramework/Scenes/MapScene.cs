using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue.Core.IO.Maps;
using Rogue.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Scenes
{
    public abstract class MapScene : Scene
    {
        public GMap Map
        {
            get;
            private set;
        }
        public MapTemplate MapTemplate
        {
            get;
            private set;
        }
        public void LoadMap(string path)
        {
            MapTemplate = new MapTemplate();
            MapTemplate.Load(path);
            Map = new GMap(new Point(MapTemplate.Width, MapTemplate.Height), true);
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
        protected override void DrawSceneObjects(GameTime gameTime)
        {


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

            Map.DrawLights(gameTime);

            Debug.SpriteBatch.End();


        }
    }
}
