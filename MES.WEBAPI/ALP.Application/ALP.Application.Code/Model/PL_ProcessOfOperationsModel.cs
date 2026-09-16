using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Code.Model
{
    public class PL_ProcessOfOperationsModel
    {
        public string Id { get; set; } 
        /// <summary>
        /// 新Id
        /// </summary>
        public string NewId { get; set; }
        /// <summary>
        /// ProcessId
        /// </summary>
        public string ProcessId { get; set; }
        /// <summary>
        /// 工艺编码
        /// </summary>
        public string ProcessCode { get; set; } 

        /// <summary>
        /// 工序编码
        /// </summary>
        public string OperationCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        public int SN { get; set; }

        /// <summary>
        /// 产出仓库编码
        /// </summary>
        public string OutWarehouse { get; set; } 

        /// <summary>
        /// 养生周期(天)
        /// </summary>
        public decimal? CuringCycle { get; set; }
    }
}
