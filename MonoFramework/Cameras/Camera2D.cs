using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework.Objects.Abstract;
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

        public Rectangle View
        {
            get
            {
                return GetView(Position);
            }
        }
        public Rectangle GetView(Vector2 pos)
        {
            return new Rectangle(pos.ToPoint(), (Debug.ScreenSize / new Vector2(Zoom)).ToPoint());
        }
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 1f) _zoom = 1f; } // Negative zoom will flip image at 0.1, < 1 Pixel is not displayed? (Texture Filtering)
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

        public PositionableObject Target
        {
            get
            {
                return m_target;
            }
            set
            {
                m_target = value;
                UpdateTargetedPosition();
            }
        }

        private PositionableObject m_target;

        public Camera2D()
        {
            _zoom = 1.0f;
            Rotation = 0.0f;
            Position = Vector2.Zero;
        }
        private void UpdateTargetedPosition()
        {
            if (Target != null)
            {
                var v1 = Vector2.Divide(Debug.GraphicsDevice.Viewport.Bounds.Size.ToVector2() / new Vector2(Zoom), 2);
                var v2 = Vector2.Divide(Target.Rectangle.Size.ToVector2(), 2);
                Position = Target.Position - v1 + v2;
            }
        }
        public void Update()
        {
            UpdateTargetedPosition();

            if (SceneManager.CurrentScene.HandleCameraInput)
                HandleInput();
        }
        public void HandleInput()
        {
            var state = Keyboard.GetState();

            float speed = 7f;

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
                Zoom += 0.02f;
            }
            else if (scrollWheelValue < OldMouseScrollWhellValue)
            {
                Zoom -= 0.02f;
            }
            OldMouseScrollWhellValue = scrollWheelValue;
        }
        public Matrix GetTransformation()
        {
            _transform =
              Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 1)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)
                                        );
            return _transform;
        }
    }
}
