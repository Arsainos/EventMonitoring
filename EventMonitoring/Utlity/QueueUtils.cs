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
        private Dictionary<string, Queue> queuePool = new Dictionary<string, Queue>();

        public void setQueuePool(string key)
        {
            queuePool.Add(key, new Queue());
        }

        public Queue getQueueFromPool(string key)
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
