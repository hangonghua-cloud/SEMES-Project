using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using log4net;

namespace AutoUpdate
{
    internal static class Program
    {
        static readonly new ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //设置进程优先级为高
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Application.ThreadException += UIThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException +=
                CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //HideOnStartupApplicationContext context = new HideOnStartupApplicationContext( new MainForm());
            //Application.Run(context);
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            try
            {
                var errorMsg = "Windows窗体线程异常 : \n\n";
                MessageBox.Show(errorMsg + t.Exception.Message + Environment.NewLine + t.Exception.StackTrace);
            }
            catch
            {
                MessageBox.Show("不可恢复的Windows窗体异常，应用程序将退出！");
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var ex = (Exception) e.ExceptionObject;
                var errorMsg = "警告： : \n\n";
                logger.Error(ex.Message + Environment.NewLine + ex.StackTrace);
                MessageBox.Show(errorMsg + "请与管理员联系！");

            }
            catch
            {
                MessageBox.Show("不可恢复的非Windows窗体线程异常，应用程序将退出！");
            }
        }
    }
}