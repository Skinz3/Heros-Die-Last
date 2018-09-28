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
    public class TeleportSameMapMessage : Message
    {
        public const ushort Id = 32;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public int entityId;

        public Vector2 position;

        public TeleportSameMapMessage()
        {

        }
        public TeleportSameMapMessage(int entityId, Vector2 position)
        {
            this.entityId = entityId;
            this.position = position;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            entityId = reader.GetInt();
            this.position = Extensions.GetVector2(reader);
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(entityId);
            writer.Put(position);
        }
    }
}
