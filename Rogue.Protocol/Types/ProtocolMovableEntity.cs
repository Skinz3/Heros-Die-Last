using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Types
{
    public abstract class ProtocolMovableEntity : ProtocolEntity
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
        public string[] Animations
        {
            get;
            set;
        }
        public string IdleAnimation
        {
            get;
            set;
        }

        public string MovementAnimation
        {
            get;
            set;
        }
        public ProtocolEntityAura Aura
        {
            get;
            set;
        }

        public ProtocolMovableEntity()
        {

        }
        public ProtocolMovableEntity(string name, int entityId, Vector2 position, Point size, Stats stats, string[] animations, string idleAnimation, string movementAnimation, ProtocolEntityAura aura) : base(name, entityId, position, size)
        {
            this.Stats = stats;
            this.Animations = animations;
            this.IdleAnimation = idleAnimation;
            this.MovementAnimation = movementAnimation;
            this.Aura = aura;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.Stats = new Stats();
            this.Stats.Deserialize(reader);

            this.Animations = new string[reader.GetInt()];

            for (int i = 0; i < Animations.Length; i++)
            {
                Animations[i] = reader.GetString();
            }

            this.IdleAnimation = reader.GetString();
            this.MovementAnimation = reader.GetString();

            if (reader.GetBool())
            {
                this.Aura = new ProtocolEntityAura();
                this.Aura.Deserialize(reader);
            }
            base.Deserialize(reader);
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            Stats.Serialize(writer);

            writer.Put(Animations.Length);

            foreach (var animation in Animations)
            {
                writer.Put(animation);
            }
            writer.Put(IdleAnimation);
            writer.Put(MovementAnimation);

            bool flag = Aura != null;

            writer.Put(flag);

            if (flag)
            {
                Aura.Serialize(writer);
            }
            base.Serialize(writer);
        }
    }
}
