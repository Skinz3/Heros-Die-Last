using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteNetLib.Utils;
using Rogue.Protocol.Types;
using MonoFramework.Network.Protocol;

namespace Rogue.Protocol.Messages.Server
{
    public class GameEntitiesMessage : Message
    {
        public const ushort Id = 8;

        public ProtocolEntity[] entities;

        public int positionUpdateFrameCount;

        public bool useInterpolation;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public GameEntitiesMessage()
        {

        }
        public GameEntitiesMessage(ProtocolEntity[] entities, int positionUpdateFrameCount, bool useInterpolation)
        {
            this.entities = entities;
            this.positionUpdateFrameCount = positionUpdateFrameCount;
            this.useInterpolation = useInterpolation;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            entities = new ProtocolEntity[reader.GetInt()];

            for (int i = 0; i < entities.Length; i++)
            {
                entities[i] = ProtocolTypeManager.GetInstance<ProtocolEntity>(reader.GetShort());
                entities[i].Deserialize(reader);
            }

            this.positionUpdateFrameCount = reader.GetInt();
            this.useInterpolation = reader.GetBool();
        }
        public ProtocolEntity GetMainPlayer(int accountId)
        {
            return this.entities.FirstOrDefault(x => x.EntityId == accountId);
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.Put(entities.Length);
            foreach (var entity in entities)
            {
                writer.Put(entity.TypeIdProp);
                entity.Serialize(writer);
            }

            writer.Put(positionUpdateFrameCount);
            writer.Put(useInterpolation);
        }
    }
}
