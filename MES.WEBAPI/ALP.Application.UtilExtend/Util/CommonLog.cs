using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.UtilExtend.Util
{
    /// <summary>
    /// 公共方法类记录日志
    /// </summary>
    public static class CommonLog
    {
        //拼接日志目录
        private static string appLogPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="folderName"></param>
        public static void WriteLog(Exception ex, string folderName = "MESErrorLog")
        {
            StringBuilder logInfo = new StringBuilder("");
            //日志记录时间前缀
            string currentTime = System.DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]");
            //日志文件名
            string fileName = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                var path = appLogPath + folderName + "/";
                //日志目录是否存在 不存在创建
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (ex != null)
                {
                    logInfo.Append("\r\n");
                    logInfo.Append(currentTime + "\r\n");
                    //获取描述当前的异常的信息
                    logInfo.Append(ex.Message + "\r\n");
                    //获取当前实例的运行时类型
                    logInfo.Append(ex.GetType() + "\r\n");
                    //获取或设置导致错误的应用程序或对象的名称
                    logInfo.Append(ex.Source + "\r\n");
                    //获取引发当前异常的方法
                    logInfo.Append(ex.TargetSite + "\r\n");
                    //获取调用堆栈上直接桢的字符串表示形式
                    logInfo.Append(ex.StackTrace + "\r\n");
                }

                using (StreamWriter SW = new StreamWriter(path + "\\" + fileName + ".txt", true, Encoding.UTF8))
                {
                    SW.Write(logInfo.ToString());
                    SW.Flush();
                }


            }
            catch (Exception e)
            {
                //return "-1" + "," + "文件写入异常:" + ex.Message.ToString();
            }
            finally
            {
                // errorinfor = "文件写入成功！";
                //SW.Close();
            }
            //return errorcode.ToString() + "," + errorinfor;
            // System.IO.File.AppendAllText(path + DateTime.Now.ToString("yyyy-MM-dd") + ".log", logInfo.ToString());
        }

        //public static string RecordLog(string loginfor)
        //{
        //    int errorcode = 0;
        //    string errorinfor = "";
        //    StreamWriter SW = null;
        //    //日志存放文件夹路径
        //    string DirectoryPath = "C:\\ERRORLOG";
        //    //日志记录时间前缀
        //    string recordlogtime = DateTime.Now.ToString("yyyyMMdd hh:mm:ss");
        //    //日志文件名
        //    string fileName = DateTime.Now.ToString("yyyyMMdd");
        //    try
        //    {
        //        if (!Directory.Exists(DirectoryPath))
        //        {
        //            Directory.CreateDirectory(DirectoryPath);
        //        }
        //        SW = new StreamWriter(DirectoryPath + "\\" + fileName + ".txt", true, Encoding.UTF8);
        //        SW.Write(recordlogtime + "\t" + loginfor + "\r\n");
        //        SW.Flush();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "-1" + "," + "文件写入异常:" + ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        errorinfor = "文件写入成功！";
        //        SW.Close();
        //    }
        //    return errorcode.ToString() + "," + errorinfor;
        //}
        /// <summary>
        /// 记录日志-输入参数
        /// </summary>
        /// <param name="obj"></param>
        public static void WriteInputLog(object obj, string folderName = "MESInputLog")
        {
            //return; 
            try
            {
                var path = appLogPath + folderName + "/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var time = DateTime.Now;
                var logname = time.ToString("yyyyMMdd");

                //DirectoryInfo dyInfo = new DirectoryInfo(path);
                ////获取文件夹下所有的文件
                //foreach (FileInfo feInfo in dyInfo.GetFiles())
                //{
                //    //判断文件日期是否小于今天，是则删除
                //    if (feInfo.CreationTime < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1)) feInfo.Delete();
                //}

                string fileName = path + "/" + logname + ".txt";
                if (!System.IO.File.Exists(fileName))
                {
                    FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(file);
                    sw.WriteLine("************************Start************************");
                    sw.WriteLine(time.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine(obj);
                    sw.WriteLine("***********************End**********************");
                    sw.Close();
                    file.Close();
                }
                else
                {
                    StreamWriter sw = new StreamWriter(fileName, true);
                    sw.WriteLine("***********************Start**********************");
                    sw.WriteLine(time.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine(obj);
                    sw.WriteLine("***********************End**********************");
                    sw.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        /// <summary>
        /// 日志文件写入每天生成一个文件 txt
        /// </summary>
        /// <param name="fstyle"></param>
        /// <param name="messagestr"></param>
        public static void WriteLogWorkDate(string fstyle, string messagestr)
        {
            #region  日志文件写入每天生成一个文件 txt
            try
            {
                //创建一个文件流，用以写入或者创建一个StreamWriter
                string filenamenew = fstyle + " " + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                //包装箱文件夹不存在创建
                string xhpath = AppDomain.CurrentDomain.BaseDirectory + "log\\" + date + "\\";
                //文件夹不存在创建
                if (!Directory.Exists(xhpath))
                {
                    Directory.CreateDirectory(xhpath);
                }

                filenamenew = xhpath + filenamenew;

                FileStream fs = new FileStream(filenamenew, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.Flush();
                // 使用StreamWriter来往文件中写入内容
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                // 把richTextBox1中的内容写入文件
                m_streamWriter.Write(messagestr + ";\r\n");
                //关闭此文件
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
            }
            #endregion
        }
    }
}
