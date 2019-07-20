using Rogue.Core.Animations;
using Rogue.Core.Collisions;
using Rogue.Core.IO.Animations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Animations
{
    public class AnimationManager
    {
        private static Dictionary<string, Dictionary<DirectionEnum, Animation>> Animations;

        public const string ANIMATION_PATH = "/Animations/";

        public static void Initialize()
        {
            Animations = new Dictionary<string, Dictionary<DirectionEnum, Animation>>();

            foreach (var file in Directory.GetFiles(Environment.CurrentDirectory + ANIMATION_PATH))
            {
                AnimationTemplate template = new AnimationTemplate();
                template.Load(file);
                Add(template);
            }

        }
        private static void Add(AnimationTemplate template)
        {
            Dictionary<DirectionEnum, Animation> directions = new Dictionary<DirectionEnum, Animation>();

            foreach (var element in template.Elements)
            {
                Animation animation = new Animation(element.Value.SpriteNames, element.Value.Delay, element.Value.Loop, element.Value.FlipVertical, element.Value.FlipHorizontal);
                animation.Initialize();

                directions.Add(element.Key, animation);
            }

            Animations.Add(template.AnimationName, directions);
        }
        public static Animation GetAnimation(string name, DirectionEnum direction)
        {
            var animation = Animations[name][direction].Clone();
            animation.Initialize();
            return animation;
        }
        public static Dictionary<DirectionEnum, Animation> GetAnimations(string name)
        {
            return Animations[name];
        }
        public static Dictionary<string, Dictionary<DirectionEnum, Animation>> GetAnimations(string[] animations)
        {
            Dictionary<string, Dictionary<DirectionEnum, Animation>> result = new Dictionary<string, Dictionary<DirectionEnum, Animation>>();

            foreach (var animation in animations)
            {
                Dictionary<DirectionEnum, Animation> animDir = new Dictionary<DirectionEnum, Animation>();

                foreach (var anim in Animations[animation])
                {
                    animDir.Add(anim.Key, anim.Value.Clone());
                }
                result.Add(animation, animDir);
            }
            return result;
        }
        public static Animation GetAnimation(string name)
        {
            return GetAnimation(name, DirectionEnum.None);
        }
    }
}
