using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework;
using MonoFramework.Animations;
using MonoFramework.DesignPattern;
using MonoFramework.Geometry;
using MonoFramework.Input;
using MonoFramework.Objects;
using MonoFramework.Pathfinding;
using MonoFramework.Scenes;
using MonoFramework.Sprites;
using Rogue.Animations;
using Rogue.Network;
using Rogue.Objects;
using Rogue.Objects.UI;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;
using Rogue.World.Entities.Projectiles;

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
            if (obj == Keys.LeftShift || obj == Keys.A)
            {
                ClientHost.Client.Send(new KeyInputMessage(obj));
            }
        }

        public void Information(string message, Color color)
        {
            AddObject(new InformationText(message, color), LayerEnum.UI);
        }
        private void SendClick(ClickTypeEnum clickType)
        {
            var targetPoint = Map.TranslateToScenePosition(Mouse.GetState().Position);

            if (ClientHost.Client != null && ClientHost.Client.Player != null)
            {
                ClientHost.Client.Send(new ClickMessage(targetPoint, ClientHost.Client.Player.Position, clickType));
            }
        }
        private void OnLeftClick()
        {
            SendClick(ClickTypeEnum.Left);


            var mousePosition = Map.TranslateToScenePosition(Mouse.GetState().Position);
            var playerCenter = ClientHost.Client.Player.Rectangle.Center;

            var relativePosition = (mousePosition - playerCenter).ToVector2();
            relativePosition.Normalize();



            var kunai = new KunaiProjectile(id++, new Vector2(ClientHost.Client.Player.Center.X, ClientHost.Client.Player.Center.Y),
                new Point(32), Color.White, relativePosition, ClientHost.Client.Player);

            ClientHost.Client.Player.MapInstance.AddProjectile(kunai);
        }

        [InDeveloppement]
        private int id = 0;

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
