using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace ALP.QuartzTask.Util
{
    public class MySchedulerFactory
    {
        private MySchedulerFactory() { }
        private static ISchedulerFactory _scheduler = GetInitSchedulerFactory();

        public static ISchedulerFactory CreateSchedulerFactory()
        {
            return _scheduler;
        }

        /// <summary>
        /// 初始设置线程池50
        /// </summary>
        /// <returns></returns>
        private static ISchedulerFactory GetInitSchedulerFactory()
        {
            var properties = new NameValueCollection();
            properties["quartz.threadPool.threadCount"] = "50";
            // 创建scheduler的引用
            ISchedulerFactory schedFact = new StdSchedulerFactory(properties);
            return schedFact;
        }
    }
}
