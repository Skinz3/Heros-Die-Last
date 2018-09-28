using Microsoft.Xna.Framework;
using MonoFramework.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Objects.Items
{
    public class HitScanShot : GLine
    {
        public HitScanShot(Vector2 position, Vector2 target, Color color, float thickness) : base(position, target, color, thickness)
        {
        }
        public override void Update(GameTime time)
        {
            Color = Color * 0.99f;
            base.Update(time);
        }

    }
}
