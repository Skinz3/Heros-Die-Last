using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFramework;
using MonoFramework.Animations;
using MonoFramework.Collisions;
using MonoFramework.Input;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Objects.Entities;
using MonoFramework.Scenes;
using Rogue.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Objects
{
    public class Player : MovableEntity
    {
        public Player(Vector2 position, GMap map, Point size, Animator animator) : base(position, map, size, animator)
        {

        }

        public override Collider2D CreateCollider(GMap map)
        {
            return new PlayerCollider(this, map);
        }
    }
}
