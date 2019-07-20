using Rogue.Objects.Entities;
using Rogue.Core.Utils;
using Rogue.Objects;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.World.Entities
{
    public class EntityManager
    {
        static Logger logger = new Logger();

        public static Entity GetEntity(ProtocolEntity protocolEntity)
        {
            EntityTypeEnum entityType = ((EntityTypeEnum)protocolEntity.TypeIdProp);
            switch (entityType)
            {
                case EntityTypeEnum.PLAYER:
                    return new Player((ProtocolPlayer)protocolEntity);
                case EntityTypeEnum.MONSTER:
                    return new Monster((ProtocolMonster)protocolEntity);

            }

            logger.Write("Unable to create entity :" + entityType, MessageState.ERROR);
            return default(Entity);
        }
    }
}
