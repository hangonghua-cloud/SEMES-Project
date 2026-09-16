using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity
{
    /// <summary>
    /// 创建：jpf
    /// 时间：2024-3-29 19:01:25
    /// 描述：接收SAP调用的信息
    /// </summary>
  public   class SAPresultEntity
    {
        /// <summary>
        /// 业务标识  1:流转卡报工 2：包装报工 3：自制半成品报工 4：报工物料消耗 5：包装入库 6：自制半成品入库 7：超产品入库 8：超产品退料
        /// </summary>
        public string BusinessType { get; set; }

        public List<SAPresult> SAPresult { get; set; }
    }
    public class SAPresult
    {
        /// <summary>
        /// ID标识
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 是否成功标识
        /// </summary>
        public string IsPosted { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string PostedMsg { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string MBLNR { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public string MJAHR { get; set; }

    }
}
