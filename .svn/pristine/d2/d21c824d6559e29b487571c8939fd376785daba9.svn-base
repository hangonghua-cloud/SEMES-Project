using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-21
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_PackingPrintMark实体
    /// 4.任务编号: 包装唛头打印
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_PackingPrintMarkEntity : BaseEntity
    { 
        #region 表: PM_PackingPrintMark 实体类: PM_PackingPrintMark 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 包装Id
        /// </summary>
        public string PackingRecordId {get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 唛头流水号
        /// </summary>
        public string PackTransferCode {get; set; } = "";
        /// <summary>
        /// 唛头名称
        /// </summary>
        public string Mark { get; set; } = "";

        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder {get; set; } = "";
 
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder {get; set; } = "";
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO {get; set; } = "";
 
        /// <summary>
        /// PO号
        /// </summary>
        public string CustomerPO {get; set; } = "";
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 盒数
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// 打印状态
        /// </summary>
        public string PrintStatus {get; set; } = "";
 
        /// <summary>
        /// 打印人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } = "";
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }
        /// <summary>
        /// 唛头状态  1：未入库 2：已入库 3：已作废 4：已冻结 5：已发货 6：已调柜 7：待发货
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 源唛头
        /// </summary>
        public string SourceMarkCode { get; set; }
        /// <summary>
        /// 工单类型
        /// </summary>
        public string WorkOrderType { get; set; }
        /// <summary>
        /// 更换人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 更换时间
        /// </summary>
        public DateTime? ChangeTime { get; set; }
        /// <summary>
        /// 纸盒日期
        /// </summary>
        public string BoxDate { get; set; }
        /// <summary>
        /// 面膜型号
        /// </summary>
        public string MMXH { get; set; }
        /// <summary>
        /// 唛头标题
        /// </summary>
        public string MTBT { get; set; }
        /// <summary>
        /// 片数
        /// </summary>
        public decimal? PieceQty { get; set; }
        /// <summary>
        /// 订单行号
        /// </summary>
        public string Orderline { get; set; }

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
