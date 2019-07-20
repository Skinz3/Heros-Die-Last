using Microsoft.Xna.Framework;
using Rogue.Core.Animations;
using Rogue.Core.Collisions;
using Rogue.Core.Geometry;
using Rogue.Core.Input;
using Rogue.Core.Scenes;
using Rogue.Inputs;
using Rogue.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.World.Entities.Weapons
{
    public class Weapon
    {
        public bool OverCharacter
        {
            get
            {
                return Player.MovementEngine.Direction == DirectionEnum.Down;
            }
        }
        private Player Player
        {
            get;
            set;
        }
        private Dictionary<DirectionEnum,Animation> WeaponAnimations
        {
            get;
            set;
        }
        public Weapon(string animationName,Player owner)
        {
            this.WeaponAnimations = AnimationManager.GetAnimations(animationName);
            this.Player = owner;
        }
        public void OnDraw(GameTime time)
        {
            var dir = Player.MovementEngine.Direction.Restrict4Direction();

            int offsetY = 0;

            if (dir == DirectionEnum.Down)
            {
                offsetY = -15;
            }
            Console.WriteLine(Player.MouseRotation);
            WeaponAnimations[dir].Draw(new Rectangle((int)Player.Center.X, (int)Player.Center.Y + offsetY, 26, 26), Color.White, Player.MouseRotation, new Vector2(4, 13));

        }
    }
}
