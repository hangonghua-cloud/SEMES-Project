using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ALP.Application.UtilExtend.Util;
using ALP.Data;
using ALP.Data.Repository;

namespace ALP.Application.WebApi.Util
{
    /// <summary>
    /// 定时任务操作
    /// </summary>
    public class TimeHelper
    {
        private static string masterConnString = System.Configuration.ConfigurationManager.ConnectionStrings["BaseMaster"] + "";

        /// <summary>
        /// 启动定时任务
        /// </summary>
        public static void Start()
        {
            try
            {
                //需要解决IIS的进程回收机制，当程序闲置时间超过20分钟（iis默认是20分钟），进程将会被回收

                System.Timers.Timer timer = new System.Timers.Timer();

                timer.Enabled = true;
                //timer.Interval = 1000 * 60 * 60 * 12;//执行间隔时间,单位为毫秒;12小时执行一次
                timer.Interval = 1000 * 60;//执行间隔时间,单位为毫秒;1分钟执行一次
                timer.Elapsed += new System.Timers.ElapsedEventHandler(BackUpDataBase);
                timer.AutoReset = true;
                timer.Start();
                //Thread.Sleep(35000);
                //timer.Stop();

                System.Timers.Timer timer1 = new System.Timers.Timer();
                timer1.Enabled = true;
                timer1.Interval = 1000 * 60 * 60 * 1;//1小时执行一次
                timer1.Elapsed += new System.Timers.ElapsedEventHandler(ExeProcGetOrderStatuse);
                timer1.AutoReset = true;
                timer1.Start();

            }
            catch (Exception ex)
            {
            }
        }



        #region 执行存储过程 proc_GetOrderStatuse
        public static void ExeProcGetOrderStatuse(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                CommonLog.WriteInputLog("执行proc_GetOrderStatuse", "TimerJob");
                IDatabase _db = DbFactory.Base();
                _db.ExecuteByProc("proc_GetOrderStatuse");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region 备份数据库
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void BackUpDataBase(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (DateTime.Now.Hour == 1 && DateTime.Now.Minute == 30)
                {
                    string newname = "FHMESDB_" + DateTime.Now.ToString("yyyyMMddHH") + ".bak";
                    //var str = System.Web.HttpRuntime.AppDomainAppPath;
                    //string newPath = Directory.GetCurrentDirectory() + @"/" + newname;
                    string newPath = @"E:\DBBackUp\" + newname;

                    CommonLog.WriteInputLog("数据库备份", "TimerJob");

                    string sql = @"BACKUP DATABASE FHMESDB to DISK ='" + newPath + "'";

                    using (SqlConnection conn = new SqlConnection(DbConstSettings.BaseDbString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();

                        //cmd.CommandText = "INSERT INTO dbo.Base_Log(CreateTime,Msg) VALUES(GETDATE(),'备份数据库')";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion


        #region 公共方法
        /// <summary>
        /// 获取保养结束时间
        /// </summary>
        /// <param name="dtMark"></param>
        /// <param name="unitMark"></param>
        /// <param name="periodMark"></param>
        /// <returns></returns>
        public static DateTimeOffset GetDays(DateTimeOffset dtMark, string unitMark, int periodMark)
        {
            DateTimeOffset result = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

            try
            {
                if (dtMark == null)
                    dtMark = result;


                dtMark = Convert.ToDateTime(dtMark.ToString("yyyy-MM-dd"));

                switch (unitMark.ToLower())
                {
                    case "日": result = dtMark.AddDays(periodMark); break;
                    case "周": result = dtMark.AddDays(periodMark * 7); break;
                    case "月": result = dtMark.AddMonths(periodMark); break;
                    case "年": result = dtMark.AddYears(periodMark); break;
                    case "day": result = dtMark.AddDays(periodMark); break;
                    case "week": result = dtMark.AddDays(periodMark * 7); break;
                    case "month": result = dtMark.AddMonths(periodMark); break;
                    case "year": result = dtMark.AddYears(periodMark); break;


                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }


        /// <summary>
        ///  转换为字符串
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToString(object o)
        {
            string result = "";
            try
            {
                if (o is DBNull || o == null)
                    result = "";
                else
                    result = Convert.ToString(o);
            }
            catch (Exception)
            {
                return "";
            }

            return result;
        }

        /// <summary>
        ///  转换为decimal
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object o)
        {
            decimal result = 0;
            try
            {
                if (o is DBNull || o == null)
                    result = 0;
                else
                    result = Convert.ToDecimal(o);
            }
            catch (Exception)
            {
                return 0;
            }

            return result;
        }

        /// <summary>
        ///  转换为int
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int ToInt(object o)
        {
            int result = 0;
            try
            {
                if (o is DBNull || o == null)
                    result = 0;
                else
                    result = Convert.ToInt32(o);
            }
            catch (Exception)
            {
                return 0;
            }

            return result;
        }


        /// <summary>
        ///  转换为bool
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool ToBool(object o)
        {
            bool result = false;
            if (o is DBNull || o == null)
                result = false;
            else
                result = Convert.ToBoolean(o);

            return result;
        }

        public static DateTimeOffset ToDatetime(object o)
        {
            DateTimeOffset timeResult = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            try
            {
                if (o is DBNull || o == null)
                    timeResult = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                else
                    timeResult = Convert.ToDateTime(Convert.ToDateTime(o).ToString("yyyy-MM-dd"));
            }
            catch (Exception ex)
            {
                timeResult = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            }

            return timeResult;
        }

        #endregion

    }
}
