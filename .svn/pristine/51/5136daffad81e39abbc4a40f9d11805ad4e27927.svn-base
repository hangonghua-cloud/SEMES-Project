using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;

namespace ALP.QuartzTask.Util
{
    public class QuartzHelper
    {
        /// <summary>
        /// 任务调度的使用过程
        /// </summary>
        /// <returns></returns>
        public static async Task RunJob(Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>> jobAndTriggerMapping)
        {
            // 创建scheduler的引用
            //ISchedulerFactory schedFact = new StdSchedulerFactory();
            //var sched = await schedFact.GetScheduler();
            var sched= await MySchedulerFactory.CreateSchedulerFactory().GetScheduler();


            // 将映射关系包装成制度字典集合
            var readOnlyjobAndTriggerMapping = new ReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>>(jobAndTriggerMapping);

            /*
             * 使用trigger规划执行任务job
             *第二个参数replace：如果为true，则指定的触发器或者任务名称已经存在将会替换，否则将抛出异常
             */
            await sched.ScheduleJobs(readOnlyjobAndTriggerMapping, true);

            //监听起始
            sched.ListenerManager.AddJobListener(new JobListener(), GroupMatcher<JobKey>.AnyGroup());

            // 启动 scheduler
            await sched.Start();
        }

        /// <summary>
        /// 执行job
        /// </summary>
        /// <param name="jobDetail"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public static async Task RunJob(IJobDetail jobDetail, ITrigger trigger)
        {
            //var schedulerFactory = new StdSchedulerFactory();
            //var scheduler = await schedulerFactory.GetScheduler();
            //await scheduler.Start();
            var scheduler = await MySchedulerFactory.CreateSchedulerFactory().GetScheduler();

            //监听起始
            scheduler.ListenerManager.AddJobListener(new JobListener(), GroupMatcher<JobKey>.AnyGroup());

            //4.添加调度
            await scheduler.ScheduleJob(jobDetail, trigger);
        }

        /// <summary>
        /// 获取定时器
        /// </summary>
        /// <param name="cron"></param>
        /// <returns></returns>
        public static ITrigger GetCronTrigger(string cron)
        {
            return TriggerBuilder.Create()
                .WithCronSchedule(cron)    //时间表达式
                .Build();
        }

        /// <summary>
        /// 获取定时触发器
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyCollection<ITrigger> GetCronTriggers(string cron)
        {
            return new ReadOnlyCollection<ITrigger>(
                new List<ITrigger>()
                {
                    GetCronTrigger(cron)
                });
        }

        public static IReadOnlyCollection<ITrigger> GetCronTriggers(ITrigger trigger)
        {
            return new ReadOnlyCollection<ITrigger>(
                new List<ITrigger>()
                {
                    trigger
                });
        }

        public static ITrigger GetTrigger(DateTime startTime, int hours)
        {
            return TriggerBuilder.Create()
                .StartAt(startTime)
                .WithSimpleSchedule(m =>
                {
                    m.WithIntervalInHours(hours).RepeatForever();
                }).Build();
        }


        /// <summary>
        /// 根据星期和时间获取cron表达式
        /// </summary>
        /// <param name="week"></param>
        /// <param name="time"></param>
        public static string GetWeekTimeCron(string week, string time)
        {
            var weekDic = new Dictionary<string,int>()
            {
                {"星期天",1 },{"星期一",2 },{"星期二",3 },{"星期三",4 },
                {"星期四",5 },{"星期五",6 },{"星期六",7 }
            };

            var setTime = DateTime.Parse(time);

            return $"{setTime.Second} {setTime.Minute} {setTime.Hour} ? * {weekDic[week]} *";
        }

        public static IReadOnlyCollection<ITrigger> GetReadOnlyMinTriggers(int? minutes)
        {
            return new ReadOnlyCollection<ITrigger>(
                new List<ITrigger>()
                {
                    GetMinTrigger(minutes)
                });
        }

        public static ITrigger GetMinTrigger(int? minutes)
        {
            minutes = minutes ?? 5;
            return TriggerBuilder.Create()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes((int)minutes).RepeatForever())
                .Build();
        }

    }
}
