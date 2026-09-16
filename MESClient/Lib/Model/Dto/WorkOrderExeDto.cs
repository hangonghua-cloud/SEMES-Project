using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model.Dto
{
   public class WorkOrderExeDto
    {
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }
        /// <summary>
        /// 执行工单类型
        /// </summary>
        public string ExeWorkOrderType { get; set; }
    }
}
