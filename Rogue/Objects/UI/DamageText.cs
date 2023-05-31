using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue.Core.Geometry;
using Rogue.Core.Lightning;
using Rogue.Core.Objects;
using Rogue.Core.Scenes;
using Rogue.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Objects.UI
{
    public class DamageText : GText
    {
        private static Color TEXT_COLOR = new Color(255 * 2, 92 * 2, 207 * 2);

        private const float COLOR_TRANSPARENCY_MULTIPLIER = 0.999f;

        public const float Y_OFFSET_INCREMENT = -1;

        public const float SCALE = 2f;

        private float CurrentTranparency
        {
            get;
            set;
        }
        private int Amount
        {
            get;
            set;
        }
        private MovableEntity Target
        {
            get;
            set;
        }
        private float YOffset
        {
            get;
            set;
        }
        public DamageText(MovableEntity target, int amount) : base(new Vector2(), SceneManager.CurrentScene.TextRenderer.GetDefaultSpriteFont(), amount.ToString(), TEXT_COLOR, 1f)
        {
            this.Amount = amount;
            this.Target = target;

            this.Scale = SCALE;

            this.DefineGradiant(new Color(Color.Red, 120), Color.Transparent, 2);



        }
        public override void Update(GameTime time)
        {
            if (this.Color.A == 0)
            {
                SceneManager.CurrentScene.RemoveObject(this);
                return;
            }

            this.Align(Target.Rectangle, RectangleOrigin.Center);
            this.Position.Y += YOffset;
            YOffset += Y_OFFSET_INCREMENT;
            base.Update(time);
        }
    }
}
