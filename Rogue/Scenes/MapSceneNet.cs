using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rogue.Core;
using Rogue.Core.Animations;
using Rogue.Core.DesignPattern;
using Rogue.Core.Geometry;
using Rogue.Core.Input;
using Rogue.Core.Objects;
using Rogue.Core.Pathfinding;
using Rogue.Core.Scenes;
using Rogue.Core.Sprites;
using Rogue.Animations;
using Rogue.Network;
using Rogue.Objects;
using Rogue.Objects.UI;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;

namespace Rogue.Scenes
{
    public abstract class MapSceneNet : MapScene
    {
        private bool NetworkReady = false;

        public override void OnInitialize()
        {
            MouseManager.OnRightButtonPressed += OnRightClick;
            MouseManager.OnLeftButtonPressed += OnLeftClick;
            KeyboardManager.OnKeyPressed += OnKeyPressed;

        }

        private void OnKeyPressed(Keys obj)
        {
            if (ClientHost.Client == null || ClientHost.Client.Player == null)
            {
                return;
            }

            var targetPoint = Map.TranslateToScenePosition(MouseManager.State.Position);

            ClientHost.Client.Send(new KeyInputMessage(obj, targetPoint, ClientHost.Client.Player.Position));
        }

        public void Information(string message, Color color)
        {
            AddObject(new InformationText(message, color), LayerEnum.UI);
        }
        private void SendClick(ClickTypeEnum clickType)
        {
            var targetPoint = Map.TranslateToScenePosition(MouseManager.State.Position);

            if (ClientHost.Client != null && ClientHost.Client.Player != null)
            {
                ClientHost.Client.Send(new ClickMessage(targetPoint, ClientHost.Client.Player.Position, clickType));
            }
        }
        private void OnLeftClick()
        {
            SendClick(ClickTypeEnum.Left);


            var mousePosition = Map.TranslateToScenePosition(MouseManager.State.Position);
            var playerCenter = ClientHost.Client.Player.Rectangle.Center;

            var relativePosition = (mousePosition - playerCenter).ToVector2();
            relativePosition.Normalize();
        }

        private void OnRightClick()
        {
            SendClick(ClickTypeEnum.Right);

        }
        public override void Dispose()
        {
            MouseManager.OnRightButtonPressed -= OnRightClick;
            MouseManager.OnLeftButtonPressed -= OnLeftClick;
            KeyboardManager.OnKeyPressed -= OnKeyPressed;
            base.Dispose();
        }


        public override void Update(GameTime gameTime)
        {
            if (NetworkReady)
            {
                base.Update(gameTime);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            if (NetworkReady)
            {
                base.Draw(gameTime);

            }
            else
            {
                Debug.GraphicsDevice.Clear(ClearColor); // loading screen

                Debug.SpriteBatch.Begin();
                Debug.DrawText(new Vector2(), "Map is loading, please wait...", Color.White);
                Debug.SpriteBatch.End();
            }
        }
        public void OnNetworkReady(string mapName)
        {
            this.NetworkReady = true;
            Information(mapName, Color.White);
        }

    }
}
