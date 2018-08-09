﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework;
using MonoFramework.Collisions;
using MonoFramework.Input;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Scenes;
using Rogue.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Objects
{
    public class Player : AnimableObject
    {
        private Collider2D Collider
        {
            get;
            set;
        }
        private MovementEngine MovementEngine
        {
            get;
            set;
        }
        public Player(Vector2 position, Point size, string[] spriteNames, float delay, bool loop) : base(position, size, spriteNames, delay, loop)
        {
            this.Collider = new PlayerCollider(this, (SceneManager.CurrentScene as TestScene).map);
            this.MovementEngine = new MovementEngine(Collider, this, 3f);
        }

        public override void OnInitialize()
        {

        }
        public override void OnDraw(GameTime time)
        {
            base.OnDraw(time);
         //   Debug.DrawRectangle(Rectangle, Color.Green);
         //   Debug.DrawRectangle(Collider.MovementHitBox, Color.Red);
        }
        public override void OnUpdate(GameTime time)
        {
            MovementEngine.Update(time);
            base.OnUpdate(time);
        }
    }
}
