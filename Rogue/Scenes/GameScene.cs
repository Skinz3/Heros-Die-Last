using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Collisions;
using MonoFramework.Geometry;
using MonoFramework.Input;
using MonoFramework.IO;
using MonoFramework.IO.Maps;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.PhysX;
using MonoFramework.Scenes;
using MonoFramework.Utils;
using Rogue.Network;
using Rogue.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Scenes
{
    public class GameScene : MapScene
    {
        public override bool HandleCameraInput => false;

        public override Color ClearColor => Color.Black;

        public override string DefaultFontName => "pixel";

        public GameScene()
        {

        }

        public override void OnInitialize()
        {
            LoadMap(@"C:/Users/Skinz/Documents/test2.map");

        }



        public override void OnInitializeComplete()
        {


        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                LoadMap(@"C:/Users/Skinz/Documents/test.map");
            }
            base.Update(gameTime);
        }

        public override void OnDispose()
        {

        }

        public override void OnMapLoaded()
        {
            Map.ToogleDrawRectangles(false);
            Camera.Zoom = MapTemplate.Zoom;
        }
    }
}
