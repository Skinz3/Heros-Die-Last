

using System;

namespace Rogue.Protocol.Enums
{
    [Flags]
    public enum CollisionMask
    {
        ENTITIES = 0x1,
        WALL = 0x2,
    }
}