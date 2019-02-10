using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMonitoring.Utlity
{
    class SyncQueue
    {
        private Queue queue;
        private object lockObject;

        public SyncQueue(object Lock)
        {
            queue = new Queue();
            lockObject = Lock;
        }

        private object Dequeue()
        {
            lock(lockObject)
            {
                return queue.Dequeue();
            }           
        }

        private bool CanDequeue()
        {
            if(queue.Peek() == null)
            {
                return false;
            }
            return true;
        }

        public void SafeEnqueue(object input)
        {
            lock(lockObject)
            {
                queue.Enqueue(input);
            }
        }

        public object SafeDequeue()
        {
            if(CanDequeue())
            {
                return Dequeue();
            }

            return null;
        }
    }
}
