using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ALP.Application.UtilExtend
{
    /// <summary>
    /// QueueHelper
    /// </summary>
    public class QueueHelper<T> where T : class, new()
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public static QueueHelper<T> Instance = new QueueHelper<T>();
        private Queue<T> queue = new Queue<T>();
        /// <summary>
        /// 添加到队列
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public void Enqueue(T t)
        {
            lock (queue)
            {
                queue.Enqueue(t);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            lock (queue)
            {
                if (queue.Count > 0)
                {
                    return queue.Dequeue();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
