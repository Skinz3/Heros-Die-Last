using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Types
{
    public class ProtocolPlayer : ProtocolMovableEntity
    {
        public new const short Id = 4;

        public override short TypeIdProp
        {
            get
            {
                return Id;
            }
        }

        public ProtocolPlayer()
        {

        }
        public ProtocolPlayer(string name, int entityId, Vector2 position, Point size, Stats stats, DirectionalAnimation[] animations) : base(name, entityId, position, size, stats, animations)
        {
        }
        public override void Deserialize(NetDataReader reader)
        {
            base.Deserialize(reader);
        }

        public override void Serialize(NetDataWriter writer)
        {
            base.Serialize(writer);
        }
    }
}
