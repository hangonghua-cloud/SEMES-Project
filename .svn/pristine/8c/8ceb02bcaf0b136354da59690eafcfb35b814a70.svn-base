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
    /// 报工入库同步
    /// </summary>
    public class WorkReport_InWarehouse_Job : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:WorkReport_InWarehouse_Job Start-------------------");

                new ToSAPService().PackingBG_InWhs();
                new ToSAPService().PackingBG_InWhs2();
                new ToSAPService().OwnProductBG_InWhs();
                new ToSAPService().OwnProductBG_InWhs2();

                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:WorkReport_InWarehouse_Job End-------------------");
            });
        }

        public static async Task StartJob()
        {
            var jobAndTriggerMapping = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();
            var job = JobBuilder.Create<WorkReport_InWarehouse_Job>()
                .WithIdentity(nameof(WorkReport_InWarehouse_Job)).Build();
            jobAndTriggerMapping[job] = QuartzHelper.GetCronTriggers("0 2/5 * * * ? ");//第2分钟开始，每5分钟执行一次
            if (jobAndTriggerMapping.Count > 0)
            {
                await QuartzHelper.RunJob(jobAndTriggerMapping);
                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:WorkReport_InWarehouse_Job start success-------------------");
            }
        }
    }
}
