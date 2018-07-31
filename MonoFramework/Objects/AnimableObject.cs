using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoFramework.Objects.Abstract;
using MonoFramework.Sprites;

namespace MonoFramework.Objects
{
    public class AnimableObject : PositionableObject
    {
        private int CurrentIndex
        {
            get;
            set;
        }
        private string[] SpritesNames
        {
            get;
            set;
        }
        private Sprite[] Sprites
        {
            get;
            set;
        }
        private double Delay
        {
            get;
            set;
        }
        private double CurrentDelay
        {
            get;
            set;
        }
        private bool Loop
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
        public AnimableObject(Vector2 position, Point size, string[] spriteNames, float delay, bool loop) : base(position, size)
        {
            this.SpritesNames = spriteNames;
            this.Delay = delay;
            this.Loop = loop;
        }
        public override void OnInitializeComplete()
        {
            this.CurrentDelay = Delay;

            this.Sprites = new Sprite[SpritesNames.Length];

            for (int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i] = SpriteManager.GetSprite(SpritesNames[i]);
            }
        }
        public override void OnUpdate(GameTime time)
        {
            CurrentDelay -= time.ElapsedGameTime.TotalMilliseconds;

            if (CurrentDelay <= 0)
            {
                CurrentIndex++;
                CurrentDelay = Delay; // 1f

                if (CurrentIndex >= Sprites.Length)
                {
                    CurrentIndex = 0;
                }
            }

        }
        public override void OnDraw(GameTime time)
        {
            Debug.SpriteBatch.Draw(Sprites[CurrentIndex].Texture, Rectangle, Color.White);
        }

        public override void OnInitialize()
        {
          
        }
    }
}
