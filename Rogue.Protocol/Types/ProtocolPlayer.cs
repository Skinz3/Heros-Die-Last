using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Types
{
    public class ProtocolPlayer : ProtocolMovableEntity
    {
        public new const short Id = 4;

        public override short TypeIdProp
        {
            get
            {
                return Id;
            }
        }
        public string WeaponAnimation
        {
            get;
            set;
        }
        public ProtocolPlayer()
        {

        }
        public ProtocolPlayer(string name, int entityId, Vector2 position, Point size, Stats stats, string[] animations, string idleAnimation, string movementAnimation, ProtocolEntityAura aura, string weaponAnimation)
            : base(name, entityId, position, size, stats, animations, idleAnimation, movementAnimation, aura)
        {
            this.WeaponAnimation = weaponAnimation;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.WeaponAnimation = reader.GetString();

            base.Deserialize(reader);
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(WeaponAnimation);

            base.Serialize(writer);
        }
    }
}
