using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model.Dto
{
    public class PackingBGDto
    {
        // <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder { get; set; }
        /// <summary>
        /// 客户PO号
        /// </summary>
        public string CustomerPO { get; set; }

        /// <summary>
        /// 包装状态
        /// </summary>
        public string PackingStatus { get; set; }
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 面膜型号
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO { get; set; }
        /// <summary>
        /// 纸盒型号
        /// </summary>
        public string PaperBox { get; set; }
        /// <summary>
        /// 报工开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 报工结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        //报工机台
        public string MachineCode { get; set; }
        /// <summary>
        /// 打印状态
        /// </summary>
        public string PrintStatus { get; set; }
        /// <summary>
        /// 流转卡编码
        /// </summary>
        public string CardCode { get; set; }

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
