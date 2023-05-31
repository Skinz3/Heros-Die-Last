using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using Rogue.Core.Network.Protocol;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class ProjectileCreateMessage : Message
    {
        public const ushort Id = 51;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int projectileId;

        public int ownerId;

        public ProjectileTypeEnum type;

        public Vector2 startPosition;

        public float speed;

        public string animationName;

        public Vector2 direction;

        public int size;

        public ProtocolAura aura;

        public ProjectileCreateMessage()
        {

        }
        public ProjectileCreateMessage(int projectileId, ProjectileTypeEnum type, int ownerId, Vector2 startPosition, float speed, string animationName, Vector2 direction, int size, ProtocolAura aura = null)
        {
            this.projectileId = projectileId;
            this.ownerId = ownerId;
            this.type = type;
            this.startPosition = startPosition;
            this.speed = speed;
            this.animationName = animationName;
            this.direction = direction;
            this.size = size;
            this.aura = aura;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            this.projectileId = reader.GetInt();
            this.ownerId = reader.GetInt();
            this.type = (ProjectileTypeEnum)reader.GetByte();
            this.startPosition = reader.GetVector2();
            this.speed = reader.GetFloat();
            this.animationName = reader.GetString();
            this.direction = reader.GetVector2();
            this.size = reader.GetInt();

            if (reader.GetBool())
            {
                this.aura = new ProtocolAura();
                this.aura.Deserialize(reader);
            }
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(projectileId);
            writer.Put(ownerId);
            writer.Put((byte)type);
            writer.Put(startPosition);
            writer.Put(speed);
            writer.Put(animationName);
            writer.Put(direction);
            writer.Put(size);

            writer.Put(aura != null);

            if (aura != null)
            {
                aura.Serialize(writer);
            }
        }
    }
}
