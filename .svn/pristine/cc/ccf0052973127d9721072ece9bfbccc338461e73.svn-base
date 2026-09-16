using Lib.Dal;
using Lib.Model.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Bll
{
    public class PrintLogBll
    {
        PrintLogDal dal = new PrintLogDal();

        /// <summary>
        /// 打印日志列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetDataTableWithPage(PrintLogDto query, out int record)
        {
            return dal.GetPrintLogDataTableWithPage(query, out record);
        }

        /// <summary>
        /// 保存打印日志
        /// </summary>
        /// <param name="logList"></param>
        /// <returns></returns>
        public int InsertPrintLog(dynamic logEntity)
        {
            return dal.InsertPrintLog(logEntity);
        }
    }
}
