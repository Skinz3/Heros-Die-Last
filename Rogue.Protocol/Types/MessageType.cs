using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Types
{
    public abstract class MessageType
    {
        public abstract short TypeIdProp
        {
            get;
        }

        public abstract void Serialize(NetDataWriter writer);
        public abstract void Deserialize(NetDataReader reader);
    }
}
