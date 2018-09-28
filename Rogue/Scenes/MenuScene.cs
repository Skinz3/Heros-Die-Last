using Microsoft.Xna.Framework;
using Rogue.Objects;
using Rogue.Scenes;
using Rogue.MapEditor;
using Rogue.Network;
using Rogue.Protocol.Messages.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFramework.Scenes;
using MonoFramework.Objects;
using MonoFramework.Objects.UI;
using MonoFramework.Objects.Abstract;

namespace Rogue.Scenes
{
    public class MenuScene : Scene
    {
        public MenuScene()
        {

        }

        public override bool HandleCameraInput => true;

        public override Color ClearColor => Color.Black;

        public override string DefaultFontName => "pixel";

        public override void OnDispose()
        {

        }
        public override void OnInitialize()
        {
            AddObject(new Background("style"), LayerEnum.First);
            TextRenderer.AddText(new Vector2(0, 10), "Welcome " + ClientHost.Client.Account.Email, Color.White);
            TextRenderer.AddText(new Vector2(0, 40), "Player Name: " + ClientHost.Client.Account.CharacterName, Color.White);


            AddObject(new SimpleButton(new Vector2(400, 400), new Point(200, 50), "Join Hub", LoadGame), LayerEnum.UI);

        }


        private void LoadGame(PositionableObject obj)
        {
            ClientHost.Client.Send(new PlayRequestMessage());
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void OnInitializeComplete()
        {

        }
    }
}
