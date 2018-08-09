using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoFramework.Animations;
using MonoFramework.Objects.Abstract;
using MonoFramework.Sprites;

namespace MonoFramework.Objects
{
    public class AnimableObject : ColorableObject
    {
        private Animation Animation
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="spriteNames"></param>
        /// <param name="delay">en ms</param>
        /// <param name="loop"></param>
        public AnimableObject(Vector2 position, Point size, string[] spriteNames, float delay, bool loop) : base(position, size, Color.White)
        {
            this.Animation = new Animation(spriteNames, delay, loop);

        }
        public override void OnInitializeComplete()
        {
            Animation.Initialize();
        }
        public override void OnUpdate(GameTime time)
        {
            Animation.Update(time);

        }
        public override void OnDraw(GameTime time)
        {
            Animation.Draw(Rectangle, Color);
        }

        public override void OnInitialize()
        {

        }
    }
}
