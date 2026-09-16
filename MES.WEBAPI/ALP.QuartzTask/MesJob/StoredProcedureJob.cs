using ALP.Application.UtilExtend.Util;
using ALP.QuartzTask.Util;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.QuartzTask.MesJob
{
    [PersistJobDataAfterExecution]
    public class StoredProcedureJob: IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    //CommonLog.WriteInputLog("执行proc_GetOrderStatuse", "QuartzTask");
                    SqlHelper.ExecutePro("proc_GetOrderStatuse",null);
                }
                catch (Exception ex)
                {
                    var logger = LogManager.GetLogger(typeof(StoredProcedureJob));
                    logger.ErrorFormat("error:" + typeof(StoredProcedureJob) + "-----" + ex.StackTrace);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("error:" + typeof(StoredProcedureJob) + "-----" + ex.StackTrace);
                }
            });
        }
    }
}
