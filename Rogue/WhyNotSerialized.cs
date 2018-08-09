using Microsoft.Xna.Framework;
using MonoFramework.Animations;
using MonoFramework.Collisions;
using MonoFramework.Objects;
using Rogue.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue
{
    class WhyNotSerialized
    {
        public static Player CreatePlayer(GMap map)
        {

            Dictionary<DirectionEnum, Animation> animations = new Dictionary<DirectionEnum, Animation>();

            animations.Add(DirectionEnum.Down, new Animation(new string[] { "sprite_hero06", "sprite_hero07", "sprite_hero08", "sprite_hero09" }, 150f, true));
            animations.Add(DirectionEnum.Right, new Animation(new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f));
            animations.Add(DirectionEnum.Up, new Animation(new string[] { "sprite_hero14", "sprite_hero15", "sprite_hero16", "sprite_hero17" }, 150f));
            animations.Add(DirectionEnum.None, new Animation(new string[] { "sprite_hero00", "sprite_hero01", "sprite_hero02", "sprite_hero01" }, 200f));
            animations.Add(DirectionEnum.Left, new Animation(new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f, true, false, true));


            animations.Add(DirectionEnum.Right | DirectionEnum.Up, new Animation(new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f));
            animations.Add(DirectionEnum.Right | DirectionEnum.Down, new Animation(new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f));

            animations.Add(DirectionEnum.Left | DirectionEnum.Up, new Animation(new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f, true, false, true));
            animations.Add(DirectionEnum.Left | DirectionEnum.Down, new Animation(new string[] { "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13" }, 150f, true, false, true));


            Animator animator = new Animator(animations);

            return new Player(new Vector2(), map, new Point(48 * 3, 48 * 3), animator);
        }
    }
}
