﻿using Microsoft.Xna.Framework;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Animations
{
    public class Animation : ICellElement
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
        private float Delay
        {
            get;
            set;
        }
        private float CurrentDelay
        {
            get;
            set;
        }
        private bool Loop
        {
            get;
            set;
        }
        public bool End
        {
            get;
            private set;
        }
        private bool FlipVertical
        {
            get;
            set;
        }
        private bool FlipHorizontal
        {
            get;
            set;
        }
        public event Action OnEnded;

        public Animation(string[] spriteNames, float delay, bool loop = true, bool flipVertical = false, bool flipHorizontal = false)
        {
            this.SpritesNames = spriteNames;
            this.Delay = delay;
            this.Loop = loop;
            this.FlipVertical = flipVertical;
            this.FlipHorizontal = flipHorizontal;
        }

        public void Dispose()
        {
            foreach (var sprite in Sprites)
            {
                sprite.Dispose();
            }
        }

        public void Initialize()
        {
            this.CurrentDelay = Delay;

            this.Sprites = new Sprite[SpritesNames.Length];

            for (int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i] = Sprite.Flip(SpriteManager.GetSprite(SpritesNames[i]), FlipVertical, FlipHorizontal);
            }
        }

        public void Update(GameTime time)
        {
            if (!End)
            {
                CurrentDelay -= (float)time.ElapsedGameTime.TotalMilliseconds;

                if (CurrentDelay <= 0)
                {
                    CurrentIndex++;
                    CurrentDelay = Delay; // 1f

                    if (CurrentIndex >= Sprites.Length)
                    {
                        if (Loop)
                            CurrentIndex = 0;
                        else
                        {
                            CurrentIndex--;
                            OnEnded?.Invoke();
                            End = true;
                        }
                    }
                }
            }
        }
        public void Draw(Rectangle rectangle, Color color, float rotation = 0)
        {
            Debug.SpriteBatch.Draw(Sprites[CurrentIndex].Texture, rectangle, color);
        }

        public Animation Clone()
        {
            return new Animation(SpritesNames, Delay, Loop, FlipVertical, FlipHorizontal);
        }
    }
}