using LiteNetLib.Utils;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFramework.Collisions;
using Rogue.Protocol.Enums;

namespace Rogue.Protocol.Types
{
    public class StateAnimations : MessageType
    {
        public static short Id = 9;
        public override short TypeIdProp => Id;

        public EntityStateEnum State
        {
            get;
            set;
        }
        public DirectionalAnimation[] Animations
        {
            get;
            set;
        }
        public StateAnimations()
        {

        }
        public StateAnimations(EntityStateEnum state, DirectionalAnimation[] animations)
        {
            this.State = state;
            this.Animations = animations;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            State = (EntityStateEnum)reader.GetByte();
            Animations = new DirectionalAnimation[reader.GetInt()];

            for (int i = 0; i < Animations.Length; i++)
            {
                Animations[i] = new DirectionalAnimation();
                Animations[i].Deserialize(reader);
            }
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put((byte)State);

            writer.Put(Animations.Length);

            foreach (var animation in Animations)
            {
                animation.Serialize(writer);
            }
        }
    }
    public class DirectionalAnimation
    {
        public DirectionEnum Direction
        {
            get;
            set;
        }
        public AnimationEnum AnimationEnum
        {
            get;
            set;
        }
        public DirectionalAnimation()
        {

        }
        public DirectionalAnimation(DirectionEnum direction, AnimationEnum animationEnum)
        {
            this.Direction = direction;
            this.AnimationEnum = animationEnum;
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.Put((byte)Direction);

            writer.Put((short)AnimationEnum);

        }

        public void Deserialize(LittleEndianReader reader)
        {
            this.Direction = (DirectionEnum)reader.GetByte();

            this.AnimationEnum = (AnimationEnum)reader.GetShort();
        }
    }
}
