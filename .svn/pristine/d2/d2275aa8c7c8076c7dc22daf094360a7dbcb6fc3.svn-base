using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.UtilExtend
{
    public class LogExtends
    {
        private static LogExtends _logExtends = null;

        static LogExtends()
        {
            if (_logExtends == null)
                _logExtends = new LogExtends();
        }

        /// <summary>
        /// 日志函数
        /// </summary>
        /// <param name="info"></param>
        public static string WriteLog(string info)
        {
            int errorcode = 0;
            string errorinfor = "";
            StreamWriter SW = null;
            //日志存放文件夹路径
            string DirectoryPath = "C:\\AppApiLog";
            //日志记录时间前缀
            string recordlogtime = "操作时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
            //日志文件名
            string fileName = DateTime.Now.ToString("yyyyMMdd");
            try
            {
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
                SW = new StreamWriter(DirectoryPath + "\\" + fileName + ".txt", true, Encoding.UTF8);
                SW.Write(recordlogtime + "\t操作内容：" + info + "\r\n");
                SW.Flush();
            }
            catch (Exception ex)
            {
                return "-1" + "," + "文件写入异常:" + ex.Message.ToString();
            }
            finally
            {
                errorinfor = "文件写入成功！";
                SW.Close();
            }
            return errorcode.ToString() + "," + errorinfor;
        }

    }
}
