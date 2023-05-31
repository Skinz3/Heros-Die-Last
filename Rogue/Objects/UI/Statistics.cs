using Microsoft.Xna.Framework;
using Rogue.Core.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Objects.UI
{
    public class Statistics : ColorableObject
    {
        public Statistics(Vector2 position, Point size, Color color, float rotation = 0) : base(position, size, color, rotation)
        {
        }

        public override void OnInitialize()
        {
            throw new NotImplementedException();
        }

        public override void OnInitializeComplete()
        {
            throw new NotImplementedException();
        }

        public override void OnDraw(GameTime time)
        {
            throw new NotImplementedException();
        }

        public override void OnDispose()
        {
            throw new NotImplementedException();
        }

    }
}
