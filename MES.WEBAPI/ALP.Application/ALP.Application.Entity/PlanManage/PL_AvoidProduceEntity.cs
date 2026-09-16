using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_AvoidProduce实体
    /// 4.任务编号: 免产单关系表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_AvoidProduceEntity : BaseEntity
    { 
        #region 表: PL_AvoidProduce 实体类: PL_AvoidProduce 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 订单编码
        /// </summary>
        public string ProductOrder {get; set; } = "";
 
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder {get; set; } = "";
 
        /// <summary>
        /// 客户PO号
        /// </summary>
        public string CustomerPO {get; set; } = "";
 
        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO {get; set; } = "";
 
        /// <summary>
        /// 发货总盒数
        /// </summary>
        public DateTimeOffset? LoadingDate {get; set; }
 
        /// <summary>
        /// 发货总托数
        /// </summary>
        public string InvoiceNO {get; set; } = "";
 
        /// <summary>
        /// 原订单号
        /// </summary>
        public string LoadingBill {get; set; } = "";
 
        /// <summary>
        /// 原订单PO号
        /// </summary>
        public string BoxType {get; set; } = "";
 
        /// <summary>
        /// 原柜号
        /// </summary>
        public decimal? GrossWeight {get; set; }
 
        /// <summary>
        /// 本次消化托数
        /// </summary>
        public decimal? Volume {get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } = "";
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTimeOffset? ModifyTime {get; set; }
 
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
 
        #endregion
    }
}
