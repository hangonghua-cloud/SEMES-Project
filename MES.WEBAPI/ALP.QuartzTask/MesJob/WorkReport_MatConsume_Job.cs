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
    /// 报工物料消耗同步
    /// </summary>
    public class WorkReport_MatConsume_Job : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:WorkReport_MatConsume_Job Start-------------------");

                new ToSAPService().TransferCardBG_MatConsume();
                new ToSAPService().PackingBG_MatConsume();
                new ToSAPService().OwnProductBG_MatConsume();

                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:WorkReport_MatConsume_Job End-------------------");
            });
        }

        public static async Task StartJob()
        {
            var jobAndTriggerMapping = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();
            var job = JobBuilder.Create<WorkReport_MatConsume_Job>()
                .WithIdentity(nameof(WorkReport_MatConsume_Job)).Build();
            jobAndTriggerMapping[job] = QuartzHelper.GetCronTriggers("0 1/2 * * * ? ");//第1分钟开始，每2分钟执行一次
            if (jobAndTriggerMapping.Count > 0)
            {
                await QuartzHelper.RunJob(jobAndTriggerMapping);
                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:WorkReport_MatConsume_Job start success-------------------");
            }
        }
    }
}
