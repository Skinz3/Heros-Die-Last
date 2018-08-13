using Microsoft.Xna.Framework;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
using Rogue.Network;
using Rogue.Objects;
using Rogue.Protocol.Messages.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Scripts
{
    public class MainPlayerScript : IScript
    {
        public static int PositionUpdateFrameCount = 1;

        private Player Player
        {
            get;
            set;
        }
        private int CurrentPositionUpdateFrameCount
        {
            get;
            set;
        }
        public MainPlayerScript()
        {
            CurrentPositionUpdateFrameCount = 0;
        }

        public void Initialize(GameObject target)
        {
            this.Player = (Player)target;
            SceneManager.CurrentScene.Camera.Target = Player;
        }

        public void Update(GameTime time)
        {
            CurrentPositionUpdateFrameCount++;

            if (CurrentPositionUpdateFrameCount == PositionUpdateFrameCount)
            {
                CurrentPositionUpdateFrameCount = 0;
                ClientHost.Client.Send(new EntityDispositionRequestMessage(Player.Position, Player.MovementEngine.Direction));
            }
        }
    }
}
