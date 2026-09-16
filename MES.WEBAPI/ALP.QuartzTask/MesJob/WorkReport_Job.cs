using ALP.Application.Service.ToSAP;
using ALP.QuartzTask.Util;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.QuartzTask.MesJob
{
    /// <summary>
    /// 报工数据同步
    /// </summary>
    public class WorkReport_Job : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:WorkReport_Job Start-------------------");

                new ToSAPService().TransferCardBG();
                new ToSAPService().PackingBG();
                new ToSAPService().OwnProductBG();

                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:WorkReport_Job End-------------------");
            });
        }

        public static async Task StartJob()
        {
            var jobAndTriggerMapping = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();
            var job = JobBuilder.Create<WorkReport_Job>()
                .WithIdentity(nameof(WorkReport_Job)).Build();
            jobAndTriggerMapping[job] = QuartzHelper.GetCronTriggers("0 0/5 * * * ? ");//第0分钟开始，每五分钟执行一次
            if (jobAndTriggerMapping.Count > 0)
            {
                await QuartzHelper.RunJob(jobAndTriggerMapping);
                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:WorkReport_Job start success-------------------");
            }
        }
    }
}
