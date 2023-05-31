using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using Rogue.Core.Collisions;
using Rogue.Core.Network.Protocol;
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

        public Vector2 startPosition;

        public Vector2 endPosition;

        public float speed;

        public string animation;

        public DashMessage()
        {

        }
        public DashMessage(int entityId, Vector2 startPosition, Vector2 endPosition, float speed, string animation)
        {
            this.entityId = entityId;
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            this.speed = speed;
            this.animation = animation;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.entityId = reader.GetInt();
            this.startPosition = reader.GetVector2();
            this.endPosition = reader.GetVector2();
            this.speed = reader.GetFloat();
            this.animation = reader.GetString();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(entityId);
            writer.Put(startPosition);
            writer.Put(endPosition);
            writer.Put(speed);
            writer.Put(animation);
        }
    }
}
