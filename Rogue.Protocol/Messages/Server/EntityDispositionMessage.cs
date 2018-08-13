using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using MonoFramework.Collisions;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class EntityDispositionMessage : Message
    {
        public const ushort Id = 12;

        public override ushort MessageId => Id;

        public int entityId;

        public Vector2 position;

        public DirectionEnum direction;

        public EntityDispositionMessage(int entityId, Vector2 position, DirectionEnum direction)
        {
            this.entityId = entityId;
            this.position = position;
            this.direction = direction;
        }
        public EntityDispositionMessage()
        {

        }
        public override void Deserialize(NetDataReader reader)
        {
            this.entityId = reader.GetInt();
            this.position = Extensions.DeserializeVector2(reader);
            this.direction = (DirectionEnum)reader.GetByte();
        }

        public override void Serialize(NetDataWriter writer)
        {
            writer.Put(entityId);
            position.Serialize(writer);
            writer.Put((byte)direction);
        }
    }
}
