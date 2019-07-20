using Rogue.Core.Collisions;
using Rogue.Core.Objects;
using Rogue.Core.Utils;
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
        static Rogue.Core.Utils.Logger logger = new Rogue.Core.Utils.Logger();

    /*    public static void CREATESLIME()
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


            stateAnimations.Add(new StateAnimations("moving", animations.ToArray()));

            animations = new List<DirectionalAnimation>();

            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Down, AnimationEnum.SLIME_DASH_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Down, AnimationEnum.SLIME_DASH_LEFT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Up, AnimationEnum.SLIME_DASH_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Up, AnimationEnum.SLIME_DASH_LEFT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right, AnimationEnum.SLIME_DASH_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left, AnimationEnum.SLIME_DASH_LEFT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Up, AnimationEnum.SLIME_DASH_UP));
            animations.Add(new DirectionalAnimation(DirectionEnum.Down, AnimationEnum.SLIME_DASH_DOWN));

            stateAnimations.Add(new StateAnimations("dashing", animations.ToArray()));

            animations = new List<DirectionalAnimation>();

            animations.Add(new DirectionalAnimation(DirectionEnum.Down, AnimationEnum.SLIME_HIT_DOWN));
            animations.Add(new DirectionalAnimation(DirectionEnum.Up, AnimationEnum.SLIME_HIT_UP));

            animations.Add(new DirectionalAnimation(DirectionEnum.Right, AnimationEnum.SLIME_HIT_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left, AnimationEnum.SLIME_HIT_LEFT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Up, AnimationEnum.SLIME_HIT_LEFT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Down, AnimationEnum.SLIME_HIT_LEFT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Up, AnimationEnum.SLIME_HIT_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Down, AnimationEnum.SLIME_HIT_RIGHT));


            stateAnimations.Add(new StateAnimations("hit", animations.ToArray()));

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
     
            animations.Add(new DirectionalAnimation(DirectionEnum.Left, AnimationEnum.PLAYER_MOVE_LEFT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Up, AnimationEnum.PLAYER_MOVE_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Down, AnimationEnum.PLAYER_MOVE_RIGHT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Up, AnimationEnum.PLAYER_MOVE_LEFT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Down, AnimationEnum.PLAYER_MOVE_LEFT));


            stateAnimations.Add(new StateAnimations("moving", animations.ToArray()));


            animations = new List<DirectionalAnimation>();

            animations.Add(new DirectionalAnimation(DirectionEnum.Down, AnimationEnum.PLAYER_IDLE_DOWN));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right, AnimationEnum.PLAYER_IDLE_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Up, AnimationEnum.PLAYER_IDLE_UP));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left, AnimationEnum.PLAYER_IDLE_LEFT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Up, AnimationEnum.PLAYER_IDLE_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Down, AnimationEnum.PLAYER_IDLE_RIGHT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Up, AnimationEnum.PLAYER_IDLE_LEFT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Down, AnimationEnum.PLAYER_IDLE_LEFT));


            stateAnimations.Add(new StateAnimations("idle", animations.ToArray()));



       

            animations = new List<DirectionalAnimation>();

            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Down, AnimationEnum.ITEM_103_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Down, AnimationEnum.ITEM_103_LEFT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right | DirectionEnum.Up, AnimationEnum.ITEM_103_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left | DirectionEnum.Up, AnimationEnum.ITEM_103_RIGHT));


            animations.Add(new DirectionalAnimation(DirectionEnum.Right, AnimationEnum.ITEM_103_RIGHT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left, AnimationEnum.ITEM_103_LEFT));

            animations.Add(new DirectionalAnimation(DirectionEnum.Up, AnimationEnum.ITEM_103_UP));
            animations.Add(new DirectionalAnimation(DirectionEnum.Down, AnimationEnum.ITEM_103_DOWN));


            stateAnimations.Add(new StateAnimations("item103", animations.ToArray()));

            animations = new List<DirectionalAnimation>();

            animations.Add(new DirectionalAnimation(DirectionEnum.Down, AnimationEnum.ITEM_405_DOWN));
            animations.Add(new DirectionalAnimation(DirectionEnum.Up, AnimationEnum.ITEM_405_UP));
            animations.Add(new DirectionalAnimation(DirectionEnum.Left, AnimationEnum.ITEM_405_LEFT));
            animations.Add(new DirectionalAnimation(DirectionEnum.Right, AnimationEnum.ITEM_405_RIGHT));

            stateAnimations.Add(new StateAnimations("item405", animations.ToArray()));

            EntityRecord record = new EntityRecord("Default", 48 * 3, 48 * 3, stateAnimations.ToArray());


            record.AddInstantElement();
            logger.Write("Player Created");
        } */
    }
}
