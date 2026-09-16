using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.QuartzTask.Util
{
    public class Cron
    {
        public static string GetCron(string taskCode)
        {
            var cronStr = "1 1/10 * * * ? *";
            var dt = SqlHelper.ExecuteDataTable($"select * from Base_TimedTaskConfig where TaskCode = '{taskCode}'");
            foreach(DataRow dr in dt.Rows)
            {
                if(dr["TaskCode"].ToString() == taskCode)
                {
                    cronStr = dr["Cron"].ToString();
                }
            }
            return cronStr;
        }
    }
}
