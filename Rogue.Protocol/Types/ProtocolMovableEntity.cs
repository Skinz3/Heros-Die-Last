using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using MonoFramework.Collisions;
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
        public DirectionalAnimation[] Animations
        {
            get;
            private set;
        }
        public ProtocolMovableEntity()
        {

        }
        public ProtocolMovableEntity(string name, int entityId, Vector2 position, Point size, Stats stats, DirectionalAnimation[] animations) : base(name, entityId, position, size)
        {
            this.Stats = stats;
            this.Animations = animations;
        }

        public override void Deserialize(NetDataReader reader)
        {
            this.Stats = new Stats();
            this.Stats.Deserialize(reader);

            this.Animations = new DirectionalAnimation[reader.GetInt()];

            for (int i = 0; i < Animations.Length; i++)
            {
                Animations[i] = new DirectionalAnimation();
                Animations[i].Deserialize(reader);
            }
            base.Deserialize(reader);
        }

        public override void Serialize(NetDataWriter writer)
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
