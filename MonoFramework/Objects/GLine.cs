﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFramework.Objects.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFramework.Objects
{
    public class GLine : TextureOwnerObject
    {
        public Vector2 Target
        {
            get;
            set;
        }
        public float Thickness
        {
            get;
            set;
        }
        public GLine(Vector2 position, Vector2 target, Color color, float thickness) : base(position, color)
        {
            this.Target = target;
            this.Thickness = thickness;
        }

        public override Point Size => throw new NotImplementedException();

        public override void OnDraw(GameTime time)
        {
            Debug.SpriteBatch.Draw(Texture, Position, null, Color, (float)Math.Atan2(Target.Y - Position.Y, Target.X - Position.X), Vector2.Zero, new Vector2((Target - Position).Length(), Thickness), SpriteEffects.None, 0);
        }

        public override Texture2D CreateTexture(GraphicsDevice graphicsDevice)
        {
            return Debug.DummyTexture;
        }

        public override void OnUpdate(GameTime time)
        {

        }
    }
}
