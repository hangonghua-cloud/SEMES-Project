using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace ALP.Application.UtilExtend.Log
{
    /// <summary>
    /// 异步日志实现类
    /// </summary>
    public class Logger
    {
        #region 属性
        /// <summary>
        /// 实例化
        /// </summary>
        public static Logger Instance = new Logger();
        /// <summary>
        /// 日志保存的路径
        /// </summary>
        string LogPath = ConfigurationManager.AppSettings["LogPath"];
        /// <summary>
        /// 文件夹路径
        /// </summary>
        string LogDir = ConfigurationManager.AppSettings["LogDir"];
        /// <summary>
        /// 文件夹路径
        /// </summary>
        string LogDateFormat = ConfigurationManager.AppSettings["LogDateFormat"] ?? "yyyyMMddHH";
        #endregion

        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Logger()
        { }

        /// <summary>
        /// 够着函数
        /// </summary>
        /// <param name="LogDir">日志的文件夹</param>
        public Logger(string LogDir)
        {
            this.LogDir = LogDir;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path"></param>
        /// <param name="LogDir"></param>
        public Logger(string path, string LogDir)
        {
            this.LogPath = path;
            this.LogDir = LogDir;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="LogDir">日志路径</param>
        /// <param name="logDateFormat">文件格式</param>
        public Logger(string path, string logDir, string logDateFormat)
        {
            this.LogPath = path;
            this.LogDir = logDir;
            this.LogDateFormat = logDateFormat;
        }
        #endregion

        #region 错误日志记录
        /// <summary>
        /// 添加错误日志
        /// </summary>
        /// <param name="ex">错误的信息</param>
        /// <param name="parms">其他的参数</param>
        public void Error(Exception ex, params object[] parms)
        {
            var strMsg = new StringBuilder();
            strMsg.AppendFormat("\r\n");
            strMsg.AppendFormat("Message:{0}", ex.Message);
            strMsg.AppendFormat("\r\n");
            strMsg.AppendFormat("StackTrace:{0}", ex.StackTrace);
            strMsg.AppendFormat("\r\n");
            strMsg.AppendFormat("source:{0}", ex.Source);
            strMsg.AppendFormat("\r\n");
            strMsg.AppendFormat("InnerException:{0}", ex.InnerException);
            strMsg.AppendFormat("\r\n");

            AddQueue(strMsg.ToString(), "Error", parms);
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="parms">其他的参数</param>
        public void Debug(string msg, params object[] parms)
        {
            AddQueue(msg, "Debug", parms);
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="parms">其他的参数</param>
        public void Info(string msg, params object[] parms)
        {
            AddQueue(msg, "Info", parms);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="parms">其他的参数</param>
        public void Warn(string msg, params object[] parms)
        {
            AddQueue(msg, "Warn", parms);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="parms">其他的参数</param>
        public void Error(string msg, params object[] parms)
        {
            AddQueue(msg, "Error", parms);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="parms">其他的参数</param>
        public void Fatal(string msg, params object[] parms)
        {
            AddQueue(msg, "Fatal", parms);
        }
        #endregion

        #region 添加日志到队列
        /// <summary>
        /// 添加日志到队列
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="level"></param>
        /// <param name="parms"></param>
        private void AddQueue(string msg, string level, params object[] parms)
        {
            QueueHelper<LogEntity>.Instance.Enqueue(new LogEntity()
            {
                Level = level,
                Msg = msg,
                Parms = parms
            });
        }
        #endregion

        #region 保存日志信息
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    var queue = QueueHelper<LogEntity>.Instance.Dequeue();

                    if (queue != null)
                    {
                        #region 基本的设置
                        var fileName = System.DateTime.Now.ToString(LogDateFormat) + ".log";
                        string path = LogPath;
                        if (string.IsNullOrEmpty(path))
                        {
                            path = System.AppDomain.CurrentDomain.BaseDirectory;
                        }
                        path = string.Format("{0}\\Log\\{1}\\{2}\\", path, queue.Level, LogDir);

                        while (path.Contains(@"\\"))
                        {
                            path = path.Replace(@"\\", @"\");
                        }
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string filePath = path + fileName;
                        #endregion

                        using (var sw = new StreamWriter(filePath, true))
                        {
                            var strMsg = new StringBuilder();
                            strMsg.AppendFormat(DateTime.Now.ToString());
                            strMsg.Append("\r\n");
                            strMsg.AppendFormat("{0}", queue.Msg);
                            if (queue.Parms != null)
                            {//--- 其他的参数 ---
                                foreach (var parm in queue.Parms)
                                {
                                    strMsg.Append("\r\n");
                                    strMsg.Append(parm);
                                }
                            }
                            strMsg.Append("\r\n");

                            sw.WriteLine(strMsg.ToString());
                        }
                    }
                }
            });
        }
        #endregion

        /// <summary>
        /// 保存日志 对象
        /// </summary>
        public class LogEntity
        {
            private string msg;
            /// <summary>
            /// 日志基本信息
            /// </summary>
            public string Msg
            {
                get { return msg; }
                set { msg = value; }
            }
            private string level;
            /// <summary>
            /// 日志等级
            /// </summary>
            public string Level
            {
                get { return level; }
                set { level = value; }
            }
            private object[] parms;
            /// <summary>
            /// 其他的参数
            /// </summary>
            public object[] Parms
            {
                get { return parms; }
                set { parms = value; }
            }
        }
    }
 
}