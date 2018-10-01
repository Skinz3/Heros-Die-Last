using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rogue.Collisions;
using Rogue.Scenes;
using MonoFramework.Utils;
using Rogue.Frames;
using Rogue.Network;
using Rogue.Protocol.Messages.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFramework.Input;
using Rogue.Objects.UI;
using MonoFramework.Objects;
using MonoFramework.Sprites;
using MonoFramework;
using Rogue.World.Items;

namespace Rogue.Scenes
{
    public class GameScene : MapSceneNet
    {
        public override bool HandleCameraInput => false;

        public override Color ClearColor => Color.Black;

        public override string DefaultFontName => "pixel";

        private string MapName
        {
            get;
            set;
        }
        public GameScene()
        {

        }

        public override void OnInitialize()
        {

            LoadMap(Environment.CurrentDirectory + "/Maps/" + ClientHost.Client.GetFrame<HubFrame>().MapName + ".map");
            base.OnInitialize();
        }

        public override void OnInitializeComplete()
        {
            AddObject(ClientHost.Client.Inventory, LayerEnum.UI);
        }
        public override void Update(GameTime gameTime)
        { 
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
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
