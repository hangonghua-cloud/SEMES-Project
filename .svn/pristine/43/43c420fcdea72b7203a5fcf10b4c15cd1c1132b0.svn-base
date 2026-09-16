using ALP.Application.Service.ToSAP;
using ALP.QuartzTask.Util;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.QuartzTask.MesJob
{
    /// <summary>
    /// 库存同步
    /// </summary>
    public class StockSync_Job : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:StockSync_Job Start-------------------");

                new ToSAPService().StockQuery();

                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:StockSync_Job End-------------------");
            });
        }

        public static async Task StartJob()
        {
            var stockSyncCron = ConfigurationManager.AppSettings["StockSyncCron"];

            var jobAndTriggerMapping = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();
            var job = JobBuilder.Create<StockSync_Job>()
                .WithIdentity(nameof(StockSync_Job)).Build();
            jobAndTriggerMapping[job] = QuartzHelper.GetCronTriggers(stockSyncCron);//每天中午12点触发 
            if (jobAndTriggerMapping.Count > 0)
            {
                await QuartzHelper.RunJob(jobAndTriggerMapping);
                Console.WriteLine($"------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}:StockSync_Job start success-------------------");
            }
        }
    }
}
