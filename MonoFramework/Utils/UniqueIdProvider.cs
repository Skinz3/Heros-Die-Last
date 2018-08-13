using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonoFramework.Utils
{
    public class UniqueIdProvider
    {
        public UniqueIdProvider(long lastId)
        {
            this.m_NextId = lastId;
        }

        protected long m_NextId;

        public virtual int GetNextId()
        {
            return (int)Interlocked.Increment(ref m_NextId);
        }
    }
}
