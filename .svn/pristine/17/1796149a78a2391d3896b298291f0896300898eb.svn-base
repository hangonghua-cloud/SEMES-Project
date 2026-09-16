using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// webservice  基础服务
    /// </summary>
    public class Baselog
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLog(string msg,string filenprefix="")
        {
            //日志存放文件夹路径
            string DirectoryPath = "C:\\BaseLog\\";
            //日志记录时间前缀
            string recordlogtime = "操作时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
            //日志文件名
            string fileName = filenprefix + DateTime.Now.ToString("yyyyMMdd");

            try
            {
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }

                fileName = DirectoryPath + fileName + ".txt";
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Dispose();
                }

                using (FileStream stream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine($"{recordlogtime}\t操作内容：{msg}\n");
                    }
                }

            }
            catch (Exception ex)
            {
                fileName = DateTime.Now.ToString("yyyyMMddError");
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }

                fileName = DirectoryPath + fileName + ".txt";
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Dispose();
                }

                using (FileStream stream = new FileStream(fileName, FileMode.Append))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(ex);
                }
                throw ex;
            }
        }
    }
}
