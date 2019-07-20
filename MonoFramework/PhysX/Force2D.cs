using Microsoft.Xna.Framework;
using Rogue.Core.DesignPattern;
using Rogue.Core.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.PhysX
{
    [InDeveloppement(InDeveloppementState.THINK_ABOUT_IT,"How to handle collision with this kind of system?")]
    public struct Force2D
    {
        public Vector2 Input
         {
            get;
            private set;
        }
        public Force2D(Vector2 input)
        {
            this.Input = input;
        }
        public void Apply(PositionableObject obj)
        {
            obj.Position += Input;
        }
    }
}
