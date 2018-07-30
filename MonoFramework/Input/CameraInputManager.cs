using Microsoft.Xna.Framework.Input;
using MonoFramework.Cameras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Input
{
    /// <summary>
    /// Permet de gérer les mouvements de la caméra.
    /// </summary>
    public class CameraInputManager
    {
        public float Speed
        {
            get;
            private set;
        }
        public CameraInputManager(float speed)
        {
            this.Speed = speed;
        }
        public void Update()
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.D))
            {
                Camera2D.MainCamera.Position.X += Speed;
            }
            if (state.IsKeyDown(Keys.Q))
            {
                Camera2D.MainCamera.Position.X -= Speed;
            }
            if (state.IsKeyDown(Keys.Z))
            {
                Camera2D.MainCamera.Position.Y -= Speed;
            }
            if (state.IsKeyDown(Keys.S))
            {
                Camera2D.MainCamera.Position.Y += Speed;
            }
        }
    }
}
