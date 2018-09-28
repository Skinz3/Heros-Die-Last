using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Protocol.Types
{
    public interface IProtocolable<T> where T : MessageType
    {
        T GetProtocolObject();
    }
}
