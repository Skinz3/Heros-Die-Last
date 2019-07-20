using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rogue.Core.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Objects.UI
{
    public abstract class UIObject : ColorableObject
    {
      // public bool focused
        public UIObject(Vector2 position, Point size, Color color) : base(position, size, color)
        {

        }
    }
}
