using MonoFramework.Animations;
using Rogue.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Animations
{
    public class AnimationManager
    {
        private static Dictionary<AnimationEnum, Animation> Animations;

        public static void Initialize()
        {
            Animations = new Dictionary<AnimationEnum, Animation>();

            #region Map Elements
            Add(AnimationEnum.SPIKES_ACTIVE, 50f, false, false, false, "sprite_217", "sprite_219", "sprite_218");
            Add(AnimationEnum.SPIKES_UNACTIVE, 50f, false, false, false, "sprite_219", "sprite_217", "sprite_216");
            Add(AnimationEnum.TORCH, 150f, "sprite_286", "sprite_287", "sprite_288", "sprite_289");
            #endregion

            #region Player
            Add(AnimationEnum.PLAYER_IDLE, 150f, "sprite_hero00", "sprite_hero01", "sprite_hero02", "sprite_hero01");
            Add(AnimationEnum.PLAYER_MOVE_DOWN, 150f, "sprite_hero06", "sprite_hero07", "sprite_hero08", "sprite_hero09");
            Add(AnimationEnum.PLAYER_MOVE_RIGHT, 150f, "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13");
            Add(AnimationEnum.PLAYER_MOVE_UP, 150f, "sprite_hero14", "sprite_hero15", "sprite_hero16", "sprite_hero17");
            Add(AnimationEnum.PLAYER_MOVE_LEFT, 150f, true, false, true, "sprite_hero10", "sprite_hero11", "sprite_hero12", "sprite_hero13");

            Add(AnimationEnum.PLAYER_DASH_RIGHT, 50f, "sprite_hero23", "sprite_hero24", "sprite_hero25");
            Add(AnimationEnum.PLAYER_DASH_LEFT, 50f, true, false, true, "sprite_hero23", "sprite_hero24", "sprite_hero25");
            Add(AnimationEnum.PLAYER_DASH_UP, 50f, "sprite_hero27", "sprite_hero28", "sprite_hero29");
            Add(AnimationEnum.PLAYER_DASH_DOWN, 50f, "sprite_hero19", "sprite_hero20", "sprite_hero21");
            #endregion


            #region Slime
            Add(AnimationEnum.SLIME_IDLE, 150f, "slime_00", "slime_01", "slime_02", "slime_01");
            Add(AnimationEnum.SLIME_MOVE_DOWN, 150f, "slime_06", "slime_07", "slime_08", "slime_09");
            Add(AnimationEnum.SLIME_MOVE_RIGHT, 150f, "slime_10", "slime_11", "slime_12", "slime_13");
            Add(AnimationEnum.SLIME_MOVE_UP, 150f, "slime_14", "slime_15", "slime_16", "slime_17");
            Add(AnimationEnum.SLIME_MOVE_LEFT, 150f, true, false, true, "slime_10", "slime_11", "slime_12", "slime_13");

            Add(AnimationEnum.SLIME_DASH_RIGHT, 50f, "slime_23", "slime_24", "slime_25");
            Add(AnimationEnum.SLIME_DASH_LEFT, 50f, true, false, true, "slime_23", "slime_24", "slime_25");
            Add(AnimationEnum.SLIME_DASH_UP, 50f, "slime_27", "slime_28", "slime_29");
            Add(AnimationEnum.SLIME_DASH_DOWN, 50f, "slime_19", "slime_20", "slime_21");

            Add(AnimationEnum.SLIME_HIT_DOWN, 150f, false, false, false, "slime_31", "slime_32", "slime_33");
            Add(AnimationEnum.SLIME_HIT_UP, 150f, false, false, false, "slime_39", "slime_40", "slime_41");
            Add(AnimationEnum.SLIME_HIT_RIGHT, 150f, false, false, false, "slime_35", "slime_36", "slime_37");
            Add(AnimationEnum.SLIME_HIT_LEFT, 150f, true, false, false, "slime_35", "slime_36", "slime_37");
            #endregion
        }

        private static void Add(AnimationEnum animationEnum, float delay, bool loop, bool flipVertical, bool flipHorizontal, params string[] sprites)
        {
            var animation = new Animation(sprites, delay, loop, flipVertical, flipHorizontal);
            animation.Initialize();
            Animations.Add(animationEnum, animation);
        }
        private static void Add(AnimationEnum animationEnum, float delay, params string[] sprites)
        {
            Add(animationEnum, delay, true, false, false, sprites);
        }
        public static Animation GetAnimation(AnimationEnum animationEnum)
        {
            var animation = Animations[animationEnum].Clone();
            animation.Initialize();
            return animation;
        }
    }
}
