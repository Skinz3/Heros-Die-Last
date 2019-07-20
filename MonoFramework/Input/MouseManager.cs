using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Input
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

        public static MouseState State;

        public static void Update()
        {
            State =  Mouse.GetState();

            if (State.MiddleButton == ButtonState.Pressed && !MiddleButtonPressed)
            {
                MiddleButtonPressed = true;
                OnMiddleButtonDown?.Invoke();
            }
            else if (State.RightButton == ButtonState.Pressed && !RightButtonPressed)
            {
                RightButtonPressed = true;
                OnRightButtonDown?.Invoke();

            }
            else if (State.RightButton == ButtonState.Released && RightButtonPressed)
            {
                RightButtonPressed = false;
                OnRightButtonPressed?.Invoke();
            }


            if (State.MiddleButton == ButtonState.Released && MiddleButtonPressed)
            {
                MiddleButtonPressed = false;
                OnMiddleButtonPressed?.Invoke();
            }
            else if (State.LeftButton == ButtonState.Pressed && !LeftButtonPressed)
            {
                LeftButtonPressed = true;
                OnLeftButtonDown?.Invoke();

            }
            else if (State.LeftButton == ButtonState.Released && LeftButtonPressed)
            {
                LeftButtonPressed = false;
                OnLeftButtonPressed?.Invoke();
            }
        }
    }
}
