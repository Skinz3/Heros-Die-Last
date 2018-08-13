using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Types
{
    public class ProtocolEntity : MessageType
    {
        public const short Id = 3;

        public override short TypeIdProp
        {
            get
            {
                return Id;
            }
        }
        public string Name
        {
            get;
            private set;
        }
        public int EntityId
        {
            get;
            private set;
        }
        public Vector2 Position
        {
            get;
            private set;
        }
        public Point Size
        {
            get;
            private set;
        }
        public ProtocolEntity()
        {

        }
        public ProtocolEntity(string name, int entityId, Vector2 position,Point size)
        {
            this.Name = name;
            this.EntityId = entityId;
            this.Position = position;
            this.Size = size;
        }
        public override void Deserialize(NetDataReader reader)
        {
            this.EntityId = reader.GetInt();
            this.Name = reader.GetString();
            this.Position = Extensions.DeserializeVector2(reader);
            this.Size = Extensions.DeserializePoint(reader);
        }

        public override void Serialize(NetDataWriter writer)
        {
            writer.Put(EntityId);
            writer.Put(Name);
            Position.Serialize(writer);
            Size.Serialize(writer);
        }
    }
}
