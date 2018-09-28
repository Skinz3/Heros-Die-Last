using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using MonoFramework.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Messages.Server
{
    public class AIMoveMessage : Message
    {
        public const ushort Id = 37;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int entityId;

        public Vector2 entityPosition;

        public int targetCellId;

        public AIMoveMessage()
        {

        }
        public AIMoveMessage(int entityId, Vector2 entityPosition, int targetCellId)
        {
            this.entityId = entityId;
            this.entityPosition = entityPosition;
            this.targetCellId = targetCellId;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.entityId = reader.GetInt();
            this.entityPosition = Extensions.GetVector2(reader);
            this.targetCellId = reader.GetInt();

        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(entityId);
            writer.Put(entityPosition);
            writer.Put(targetCellId);
        }
    }
}
