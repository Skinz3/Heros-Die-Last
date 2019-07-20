using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Network.Protocol
{
    public abstract class MessageType
    {
        public abstract short TypeIdProp
        {
            get;
        }

        public abstract void Serialize(LittleEndianWriter writer);
        public abstract void Deserialize(LittleEndianReader reader);
    }
}
