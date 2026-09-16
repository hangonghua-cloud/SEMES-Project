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
    /// 成品发货同步
    /// </summary>
    public class ProductDispatch_Job : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:ProductDispatch_Job Start-------------------");

                new ToSAPService().ProductDispatch();

                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:ProductDispatch_Job End-------------------");
            });
        }

        public static async Task StartJob()
        {
            var jobAndTriggerMapping = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();
            var job = JobBuilder.Create<ProductDispatch_Job>()
                .WithIdentity(nameof(ProductDispatch_Job)).Build();
            jobAndTriggerMapping[job] = QuartzHelper.GetCronTriggers("0 3/5 * * * ? ");//第3分钟开始，每5分钟执行一次
            if (jobAndTriggerMapping.Count > 0)
            {
                await QuartzHelper.RunJob(jobAndTriggerMapping);
                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:ProductDispatch_Job start success-------------------");
            }
        }
    }
}
