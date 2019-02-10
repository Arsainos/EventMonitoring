using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMonitoring.Utlity
{
    class QueueUtils
    {
        private Dictionary<string, SyncQueue> queuePool = new Dictionary<string, SyncQueue>();

        public void setQueuePool(string key, object lockObject)
        {
            queuePool.Add(key, new SyncQueue(lockObject));
        }

        public SyncQueue getQueueFromPool(string key)
        {
            try
            {
                return queuePool[key];
            }
            catch
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
