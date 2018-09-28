using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Cameras;
using MonoFramework.Geometry;
using MonoFramework.Input;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
using Rogue.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Scripts
{
    public class CameraControlScript : IScript
    {
        public const float FORCE = 22f;

        public const float FORCE_DECREMENT = 0.5f;

        private Player Player
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
        Vector2 Force
        {
            get;
            set;
        }

        Vector2 InputVector
        {
            get;
            set;
        }
        public void Dispose()
        {
            KeyboardManager.OnKeyDown -= OnKeyDown;
        }

        public void Initialize(GameObject target)
        {
            this.Player = (Player)target;
            KeyboardManager.OnKeyDown += OnKeyDown;
            this.Force = new Vector2(FORCE);
        }

      

        private void OnKeyDown(Keys obj)
        {
            if (obj == Keys.Space && !Player.Aiming)
            {
                Player.Aiming = true;
                Camera.Target = null;

                var mousePosOnWorld = SceneManager.GetCurrentScene<MapScene>().Map.TranslateToScenePosition(Mouse.GetState().Position);
                InputVector = mousePosOnWorld.ToVector2() - Player.Center;
                InputVector = InputVector.GetDirection().GetInputVector();
            }
        }


    

        public void Update(GameTime time)
        {
            if (Player.Aiming)
            {
                var newPosition = Camera.Position + InputVector * Force;

                var newCamCenter = Camera.GetView(newPosition).Center;

                Vector2 l = (newCamCenter.ToVector2() - Player.Center);

                bool xGood = (l * InputVector).X < 0;
                bool yGood = (l * InputVector).Y < 0;

                if (xGood || yGood)
                {
                    Player.Aiming = false;
                    Force = new Vector2(FORCE);
                    Camera.Target = Player;
                }
                else
                {
                    Camera.Position = newPosition;
                    Force -= new Vector2(FORCE_DECREMENT);

                }
            }
        }

        public void OnRemove()
        {
            KeyboardManager.OnKeyDown -= OnKeyDown;
        }
    }
}
