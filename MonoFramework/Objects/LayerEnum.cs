using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects
{
    /// <summary>
    /// GGRid.cs  this.DisplayedLayers = LayerEnum.FIRST | LayerEnum.SECOND | LayerEnum.THIRD; ! before modify
    /// </summary>
    [Flags]
    public enum LayerEnum 
    {
        First = 0x1,
        Second = 0x2,
        Third = 0x4,
        UI = 0x8, // Ne prend pas en compte le mouvement de la caméra
    }
}
