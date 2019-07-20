using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rogue.Core.Geometry;
using Rogue.Core.Input;
using Rogue.Core.Objects.Abstract;
using Rogue.Core.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Objects.UI
{
    public class InputText : UIObject
    {
        public InputText(Vector2 position, Point size, Color color, SpriteFont font) : base(position, size, color)
        {
        }
        public InputText(Vector2 position, Point size, Color color) : base(position, size, color)
        {

        }
        public override void OnDraw(GameTime time)
        {

        }

        public override void OnInitialize()
        {

        }

        private void OnKeyPressed(Keys obj)
        {

        }

        public override void OnInitializeComplete()
        {

        }

        public override void OnUpdate(GameTime time)
        {

        }

        public override void OnDispose()
        {
           
        }
    }
}
