using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rogue.Core;
using Rogue.Core.Cameras;
using Rogue.Core.Collisions;
using Rogue.Core.DesignPattern;
using Rogue.Core.Geometry;
using Rogue.Core.Objects;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Scenes;
using Rogue.Network;
using Rogue.Objects;
using Rogue.Protocol.Messages.Client;

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
        private Camera2D Camera
        {
            get
            {
                return SceneManager.CurrentScene.Camera;
            }
        }
        public MainPlayerScript()
        {
            CurrentPositionUpdateFrameCount = 0;
        }

        public void Initialize(GameObject target)
        {
            this.Player = (Player)target;
            Camera.Target = Player;
        }

        public void SendOnNextFrame()
        {
            CurrentPositionUpdateFrameCount = PositionUpdateFrameCount;
        }
        public void Update(GameTime time)
        {
            CurrentPositionUpdateFrameCount++;

            if (this.Player.Dashing)
            {
                return;
            }

            if (CurrentPositionUpdateFrameCount >= PositionUpdateFrameCount)
            {
                CurrentPositionUpdateFrameCount = 0;
                ClientHost.Client.Send(new EntityDispositionRequestMessage(Player.Position, Player.MovementEngine.Direction, Player.MouseRotation));
            }
        }

        public void Dispose()
        {

        }

        public void OnRemove()
        {

        }
    }
}
