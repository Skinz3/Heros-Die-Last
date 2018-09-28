using Microsoft.Xna.Framework;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Scripts
{
    public class MOFadeOutScript : IScript
    {
        public const float DECREMENT = 0.03f;

        private GCell Target
        {
            get;
            set;
        }
        private LayerEnum Layer
        {
            get;
            set;
        }
        private float Transparency
        {
            get;
            set;
        }
        private Color DefaultColor
        {
            get;
            set;
        }
        public MOFadeOutScript(LayerEnum layer)
        {
            this.Layer = layer;
        }

        public void Dispose()
        {
            
        }

        public void Initialize(GameObject target)
        {
          

            Target = (GCell)target;
            Target.RemoveFirstScript<MOFadeInScript>();
            Target.RemoveFirstScript<MOFadeOutScript>();
            DefaultColor = Target.SpriteColors[Layer];
            Transparency = 1f;
        }

        public void Update(GameTime time)
        {
            Transparency -= DECREMENT;

            Target.SpriteColors[Layer] = DefaultColor * Transparency;

            if (Transparency <= 0f)
            {
                Target.SpriteColors[Layer] = DefaultColor;
                Target.RemoveSprite(Layer);
                Target.RemoveScript(this);
            }
        }

        public void OnRemove()
        {
            Target.SpriteColors[Layer] = DefaultColor;
        }
    }
}
