using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue.Core;
using Rogue.Core.Geometry;
using Rogue.Core.Objects;
using Rogue.Core.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Objects.UI
{
    public class InformationText : GText
    {
        private const float COLOR_TRANSPARENCY_MULTIPLIER = 0.999f;

        public InformationText(string text, Color color) : base(new Vector2(),
            SceneManager.CurrentScene.TextRenderer.GetDefaultSpriteFont(), text, color, 6)
        {

        }
        public override void OnUpdate(GameTime time)
        {
            if (this.Color.A == 0)
            {
                SceneManager.CurrentScene.RemoveObject(this);
                return;
            }
            this.Color = Color * COLOR_TRANSPARENCY_MULTIPLIER;
            this.Align(Debug.ScreenBounds, RectangleOrigin.Center);

            this.Position.Y -= Debug.ScreenSize.Y / 4;
            base.OnUpdate(time);
        }
    }
}
