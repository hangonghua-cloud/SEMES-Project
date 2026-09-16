using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model.Dto
{
   public class MachineDto
    {
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string OperationCode { get; set; }
        /// <summary>
        /// 机台编码
        /// </summary>
        public string MachineCode { get; set; }
        /// <summary>
        /// 机台名称
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sidx { get; set; } = "(SELECT 0)";
    }
}
