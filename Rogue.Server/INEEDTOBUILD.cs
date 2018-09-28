using LiteDB;
using MonoFramework.Collisions;
using MonoFramework.Objects;
using MonoFramework.Utils;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Types;
using Rogue.Server.Records;
using Rogue.Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Server
{
 
    class INEEDTOBUILD
    {
        static MonoFramework.Utils.Logger logger = new MonoFramework.Utils.Logger();

        public static void CREATESLIME()
        {
            List<StateAnimations> stateAnimations = new List<StateAnimations>();

            List<DirectionalAnimation> animations = new List<DirectionalAnimation>();

            animations.Add(new DirectionalAnimation(DirectionEnum.Down, AnimationEnum.SLIME_MOVE_DOWN));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right, AnimationEnum.SLIME_MOVE_RIGHT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Up, AnimationEnum.SLIME_MOVE_UP));
            animations.Add(new DirectionalAnimation(DirectionEnum.None, AnimationEnum.SLIME_IDLE));

            animations.Add(new DirectionalAnimation(DirectionEnum.Left, AnimationEnum.SLIME_MOVE_LEFT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Up, AnimationEnum.SLIME_MOVE_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Down, AnimationEnum.SLIME_MOVE_RIGHT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Up, AnimationEnum.SLIME_MOVE_LEFT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Down, AnimationEnum.SLIME_MOVE_LEFT));


            stateAnimations.Add(new StateAnimations(EntityStateEnum.MOVING, animations.ToArray()));

            animations = new List<DirectionalAnimation>();

            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Down, AnimationEnum.SLIME_DASH_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Down, AnimationEnum.SLIME_DASH_LEFT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Up, AnimationEnum.SLIME_DASH_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Up, AnimationEnum.SLIME_DASH_LEFT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right, AnimationEnum.SLIME_DASH_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left, AnimationEnum.SLIME_DASH_LEFT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Up, AnimationEnum.SLIME_DASH_UP));
            animations.Add(new DirectionalAnimation(DirectionEnum.Down, AnimationEnum.SLIME_DASH_DOWN));

            stateAnimations.Add(new StateAnimations(EntityStateEnum.DASHING, animations.ToArray()));

            animations = new List<DirectionalAnimation>();

            animations.Add(new DirectionalAnimation(DirectionEnum.Down, AnimationEnum.SLIME_HIT_DOWN));
            animations.Add(new DirectionalAnimation(DirectionEnum.Up, AnimationEnum.SLIME_HIT_UP));

            animations.Add(new DirectionalAnimation(DirectionEnum.Right, AnimationEnum.SLIME_HIT_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left, AnimationEnum.SLIME_HIT_LEFT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Up, AnimationEnum.SLIME_HIT_LEFT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Down, AnimationEnum.SLIME_HIT_LEFT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Up, AnimationEnum.SLIME_HIT_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Down, AnimationEnum.SLIME_HIT_RIGHT));


            stateAnimations.Add(new StateAnimations(EntityStateEnum.HIT, animations.ToArray()));

            animations = new List<DirectionalAnimation>();

            EntityRecord record = new EntityRecord("Slime", 48 * 3, 48 * 3, stateAnimations.ToArray());

            record.AddInstantElement();
            logger.Write("Slime Created");
        }
        public static void CREATEPLAYER()
        {
            List<StateAnimations> stateAnimations = new List<StateAnimations>();

            List<DirectionalAnimation> animations = new List<DirectionalAnimation>();

            animations.Add(new DirectionalAnimation(DirectionEnum.Down, AnimationEnum.PLAYER_MOVE_DOWN));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right, AnimationEnum.PLAYER_MOVE_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Up, AnimationEnum.PLAYER_MOVE_UP));
            animations.Add(new DirectionalAnimation(DirectionEnum.None, AnimationEnum.PLAYER_IDLE));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left, AnimationEnum.PLAYER_MOVE_LEFT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Up, AnimationEnum.PLAYER_MOVE_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Down, AnimationEnum.PLAYER_MOVE_RIGHT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Up, AnimationEnum.PLAYER_MOVE_LEFT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Down, AnimationEnum.PLAYER_MOVE_LEFT));


            stateAnimations.Add(new StateAnimations(EntityStateEnum.MOVING, animations.ToArray()));

            animations = new List<DirectionalAnimation>();

            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Down, AnimationEnum.PLAYER_DASH_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Down, AnimationEnum.PLAYER_DASH_LEFT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Up, AnimationEnum.PLAYER_DASH_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Up, AnimationEnum.PLAYER_DASH_LEFT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right, AnimationEnum.PLAYER_DASH_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left, AnimationEnum.PLAYER_DASH_LEFT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Up, AnimationEnum.PLAYER_DASH_UP));
            animations.Add(new DirectionalAnimation(DirectionEnum.Down, AnimationEnum.PLAYER_DASH_DOWN));


            stateAnimations.Add(new StateAnimations(EntityStateEnum.DASHING, animations.ToArray()));

            EntityRecord record = new EntityRecord("Default", 48 * 3, 48 * 3, stateAnimations.ToArray());


            record.AddInstantElement();
            logger.Write("Player Created");
        }
    }
}
