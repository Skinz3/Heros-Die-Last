using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Collisions
{
    [Flags]
    public enum DirectionEnum
    {
        None = 0x0,
        Right = 0x1,
        Left = 0x2,
        Up = 0x4,
        Down = 0x8,
    }
}
