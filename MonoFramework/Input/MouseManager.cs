using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Input
{
    public class MouseManager
    {
        public static bool LeftButtonPressed = false;

        public static bool RightButtonPressed = false;

        public static bool MiddleButtonPressed = false;

        public static event Action OnLeftButtonDown;
        public static event Action OnRightButtonDown;
        public static event Action OnMiddleButtonDown;

        public static event Action OnLeftButtonPressed;
        public static event Action OnRightButtonPressed;
        public static event Action OnMiddleButtonPressed;

        public static void Update()
        {
            var state = Mouse.GetState();


            if (state.MiddleButton == ButtonState.Pressed && !MiddleButtonPressed)
            {
                MiddleButtonPressed = true;
                OnMiddleButtonDown?.Invoke();
            }
            else if (state.RightButton == ButtonState.Pressed && !RightButtonPressed)
            {
                RightButtonPressed = true;
                OnRightButtonDown?.Invoke();

            }
            else if (state.RightButton == ButtonState.Released && RightButtonPressed)
            {
                RightButtonPressed = false;
                OnRightButtonPressed?.Invoke();
            }


            if (state.MiddleButton == ButtonState.Released && MiddleButtonPressed)
            {
                MiddleButtonPressed = false;
                OnMiddleButtonPressed?.Invoke();
            }
            else if (state.LeftButton == ButtonState.Pressed && !LeftButtonPressed)
            {
                LeftButtonPressed = true;
                OnLeftButtonDown?.Invoke();

            }
            else if (state.LeftButton == ButtonState.Released && LeftButtonPressed)
            {
                LeftButtonPressed = false;
                OnLeftButtonPressed?.Invoke();
            }
        }
    }
}
