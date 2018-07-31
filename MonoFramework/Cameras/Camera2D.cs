using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Cameras
{
    /// <summary>
    /// Todo add method:  Follow(GameObject target);
    /// </summary>
    public class Camera2D
    {
        /// <summary>
        /// Caméra actuellement utilisée.
        /// </summary>
        public static Camera2D MainCamera
        {
            get
            {
                return SceneManager.CurrentScene.Camera;
            }
        }

        float _zoom;
        Matrix _transform;

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom <1f) _zoom = 1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get;
            set;
        }

        public Vector2 Position;

        private int OldMouseScrollWhellValue
        {
            get;
            set;
        }

        public Camera2D()
        {
            _zoom = 1.0f;
            Rotation = 0.0f;
            Position = Vector2.Zero;
        }

        public void HandleInput()
        {
            var state = Keyboard.GetState();

            float speed = 5f;

            if (state.IsKeyDown(Keys.D))
            {
                Camera2D.MainCamera.Position.X += speed;
            }
            if (state.IsKeyDown(Keys.Q))
            {
                Camera2D.MainCamera.Position.X -= speed;
            }
            if (state.IsKeyDown(Keys.Z))
            {
                Camera2D.MainCamera.Position.Y -= speed;
            }
            if (state.IsKeyDown(Keys.S))
            {
                Camera2D.MainCamera.Position.Y += speed;
            }

            var scrollWheelValue = Mouse.GetState().ScrollWheelValue;

            if (scrollWheelValue > OldMouseScrollWhellValue)
            {
                Zoom += 0.01f;
            }
            else if (scrollWheelValue < OldMouseScrollWhellValue)
            {
                Zoom -= 0.01f;
            }
            OldMouseScrollWhellValue = scrollWheelValue;
        }
        public Matrix GetTransformation()
        {
            _transform =
              Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)
                                        );
            return _transform;
        }
    }
}
