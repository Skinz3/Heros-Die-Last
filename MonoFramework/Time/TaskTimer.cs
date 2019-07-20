using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue.Core.Time
{
    public class TaskTimer
    {
        public event Action Elapsed;

        public bool DoStop
        {
            get;
            set;
        }
        private int Interval
        {
            get;
            set;
        }
        public TaskTimer(int interval)
        {
            this.Interval = interval;
        }
        public void Start()
        {
            Tick();
        }
        public void Tick()
        {
            RetryTick:
            if (DoStop)
            {
                return;
            }
            Task.Delay(Interval).Wait();
            Elapsed();
            goto RetryTick;
        }
        public void Stop()
        {
            DoStop = true;
        }
    }
}
