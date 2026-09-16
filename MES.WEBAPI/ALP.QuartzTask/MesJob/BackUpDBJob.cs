using ALP.Application.UtilExtend.Util;
using ALP.QuartzTask.Util;
using ALP.QuartzTask.Util;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.QuartzTask.MesJob
{
    [PersistJobDataAfterExecution]
    public class BackUpDBJob: IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    string newname = "FHMESDB_" + DateTime.Now.ToString("yyMMddHH") + ".bak";
                    //var str = System.Web.HttpRuntime.AppDomainAppPath;
                    //string newPath = Directory.GetCurrentDirectory() + @"/" + newname;
                    string newPath = @"E:\DBBackUp\" + newname;

                    CommonLog.WriteInputLog("数据库备份", "QuartzTask");

                    string sql = @"BACKUP DATABASE FHMESDB to DISK ='" + newPath + "'";

                    using (SqlConnection conn = new SqlConnection(SqlHelper.connStr))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();

                        //cmd.CommandText = "INSERT INTO dbo.Base_Log(CreateTime,Msg) VALUES(GETDATE(),'备份数据库')";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    var logger = LogManager.GetLogger(typeof(BackUpDBJob));
                    logger.ErrorFormat("error:" + typeof(BackUpDBJob) + "-----" + ex.StackTrace);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("error:" + typeof(BackUpDBJob) + "-----" + ex.StackTrace);
                }
            });
        }
    }
}
