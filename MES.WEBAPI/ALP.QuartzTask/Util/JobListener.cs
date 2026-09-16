using System;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Quartz;

namespace ALP.QuartzTask.Util
{
     
    public class JobListener : IJobListener
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(JobListener));
        public string Name { get; } = nameof(JobListener);
        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            //Job即将执行
            return Task.Factory.StartNew(() =>
            {
                // Console.WriteLine($" {context.JobDetail.Key} Start time:{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                log.Info($" {context.JobDetail.Key} Start time:{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }, cancellationToken);
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() => {
                //Console.WriteLine($"Job {context.JobDetail.Key} 被否决执行");
                log.Info($"Job { context.JobDetail.Key} 被否决执行");
            }, cancellationToken);
        }
        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            //Job执行完成
            return Task.Factory.StartNew(() =>
            {
                log.Info($" {context.JobDetail.Key} End time:{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                log.Info("------------------------------------\n");
                //Console.WriteLine($" {context.JobDetail.Key} End time:{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                //Console.WriteLine("------------------------------------");
            }, cancellationToken);
        }
    }
}
