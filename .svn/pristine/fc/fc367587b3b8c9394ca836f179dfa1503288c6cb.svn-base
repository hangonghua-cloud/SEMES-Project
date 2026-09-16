using ALP.QuartzTask.MesJob;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace ALP.QuartzTask
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            log.Warn("Job Program execution begins...");

            DisbleQuickEditMode();

            await WorkReport_Job.StartJob(); //报工数据同步
            await WorkReport_MatConsume_Job.StartJob(); //报工物料消耗同步
            await WorkReport_InWarehouse_Job.StartJob(); //报工入库同步
            await ProductDispatch_Job.StartJob(); //成品发货同步
            //韩总要求取消 modify 2026-03-29 by dragon 
            //await StockSync_Job.StartJob();// SAP库存同步


            Console.ReadLine();
        }

        #region 关闭Console application的quick edit模式
        //关闭 cmd 窗口默认为快速编辑(quickedit)，解决控制台程序,鼠标点击暂停运行
        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int hConsoleHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint mode);

        public static void DisbleQuickEditMode()
        {
            IntPtr hStdin = GetStdHandle(STD_INPUT_HANDLE);
            uint mode;
            GetConsoleMode(hStdin, out mode);
            mode &= ~ENABLE_QUICK_EDIT_MODE;
            SetConsoleMode(hStdin, mode);
        }
        #endregion

    }
}
