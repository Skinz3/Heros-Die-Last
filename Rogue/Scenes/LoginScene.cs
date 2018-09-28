using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Network.Protocol;
using Rogue.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Objects.UI;
using Rogue.Scenes;
using Rogue.MapEditor;
using Rogue.Network;
using Rogue.Protocol.Messages.Client;
using Rogue.Protocol.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MonoFramework.Objects;
using MonoFramework.Scenes;

namespace Rogue.Scenes
{
    public class LoginScene : Scene
    {
        public override bool HandleCameraInput => false;

        public override Color ClearColor => Color.Black;

        public override string DefaultFontName => "pixel";



        public override void OnInitialize()
        {

            AddObject(new Background("style"), LayerEnum.First);

            AddObject(new SimpleButton(new Vector2(400, 400), new Point(200, 50), "Simulate Login", OnConnectClicked), LayerEnum.UI);

            AddObject(new SimpleButton(new Vector2(400, 450), new Point(200, 50), "Map Editor", LoadMapEditor), LayerEnum.UI);


        }

        private void LoadMapEditor(PositionableObject obj)
        {
            SceneManager.LoadScene(new EditorMenuScene());
        }

        private void OnConnectClicked(PositionableObject obj)
        {
            if (ClientHost.IsInitialized == false)
            {
                ClientHost.Initialize();
                ClientHost.Client.OnConnectionSucceed += OnConnectionSucceed;
                ClientHost.Client.OnConnectionFailed += OnConnectionFailed;
                ClientHost.Connect();
            }
        }

        private void OnConnectionFailed()
        {
            ClientHost.DestroyClient();
        }
        private void OnConnectionSucceed()
        {
            ClientHost.Client.Send(new AuthentificationRequestMessage("test1", "test"));
        }

        public override void OnInitializeComplete()
        {
            if (!ProtocolManager.Initialized)
                ProtocolManager.Initialize(Assembly.GetAssembly(typeof(LoadFrameMessage)), Assembly.GetExecutingAssembly(), false);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void OnDispose()
        {

        }
    }
}
