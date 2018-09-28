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
    public class DashMessage : Message
    {
        public const ushort Id = 34;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int entityId;

        public float speed;

        public int distance;

        public DirectionEnum direction;

        public DashMessage()
        {

        }
        public DashMessage(int entityId, float speed, DirectionEnum direction, int distance)
        {
            this.entityId = entityId;
            this.direction = direction;
            this.speed = speed;
            this.distance = distance;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.entityId = reader.GetInt();
            this.speed = reader.GetFloat();
            this.distance = reader.GetInt();
            this.direction = (DirectionEnum)reader.GetByte();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(entityId);
            writer.Put(speed);
            writer.Put(distance);
            writer.Put((byte)direction);
        }
    }
}
