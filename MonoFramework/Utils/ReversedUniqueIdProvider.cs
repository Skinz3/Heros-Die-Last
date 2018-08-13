using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonoFramework.Utils
{
    public class ReversedUniqueIdProvider : UniqueIdProvider
    {
        public ReversedUniqueIdProvider(int lastId)
            : base(lastId)
        {
        }

        public override int GetNextId()
        {
            return (int)Interlocked.Decrement(ref m_NextId);
        }
    }
}
