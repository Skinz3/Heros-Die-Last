using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Types
{
    public class ProtocolMovableEntity : ProtocolEntity
    {
        public new const short Id = 5;

        public override short TypeIdProp
        {
            get
            {
                return Id;
            }
        }

        public Stats Stats
        {
            get;
            private set;
        }
        public StateAnimations[] Animations
        {
            get;
            private set;
        }
        public ProtocolMovableEntity()
        {

        }
        public ProtocolMovableEntity(string name, int entityId, Vector2 position, Point size, Stats stats, StateAnimations[] animations) : base(name, entityId, position, size)
        {
            this.Stats = stats;
            this.Animations = animations;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.Stats = new Stats();
            this.Stats.Deserialize(reader);

            this.Animations = new StateAnimations[reader.GetInt()];

            for (int i = 0; i < Animations.Length; i++)
            {
                Animations[i] = new StateAnimations();
                Animations[i].Deserialize(reader);
            }
            base.Deserialize(reader);
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            Stats.Serialize(writer);

            writer.Put(Animations.Length);

            foreach (var animation in Animations)
            {
                animation.Serialize(writer);
            }
            base.Serialize(writer);
        }
    }
}
