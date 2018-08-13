using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFramework.Network.Protocol;
using Microsoft.Xna.Framework;
using Rogue.Protocol;

namespace Rogue.Protocol.Messages.Server
{
    public class EntityDispositionMessageDiag : Message
    {
        public const ushort Id = 16;

        public override ushort MessageId => Id;

        public int entityId;

        public Vector2 position;

        public float rotation;

        public long dateTime;
        public EntityDispositionMessageDiag(int entityId, Vector2 position, float rotation,long dateTime)
        {
            this.entityId = entityId;
            this.position = position;
            this.rotation = rotation;
            this.dateTime = dateTime;
        }
        public EntityDispositionMessageDiag()
        {

        }
        public override void Deserialize(NetDataReader reader)
        {
            this.entityId = reader.GetInt();
            this.position = Extensions.DeserializeVector2(reader);
            this.rotation = reader.GetFloat();
            this.dateTime = reader.GetLong();
        }

        public override void Serialize(NetDataWriter writer)
        {
            writer.Put(entityId);
            position.Serialize(writer);
            writer.Put(rotation);
            writer.Put(dateTime);
        }
    }
}
