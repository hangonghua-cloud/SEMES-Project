using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-19
    /// 2.创建作者: admin
    /// 3.功能描述: MM_ProductRework实体
    /// 4.任务编号: 成品返工单明细
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_ProductReworkEntity : BaseEntity
    { 
        #region 表: MM_ProductRework 实体类: MM_ProductRework 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 成品返工单号
        /// </summary>
        public string ReWorkOrder {get; set; } = "";
 
        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder {get; set; } = "";
 
        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO {get; set; } = "";
 
        /// <summary>
        /// 客户PO号
        /// </summary>
        public string CustomerPO { get; set; } = "";
 
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode {get; set; } = "";
 
        /// <summary>
        /// 返工责任工序
        /// </summary>
        public string DutyProcess {get; set; } = "";
 
        /// <summary>
        /// 成品返工单状态 1：未开始 2：正在生产 3：已完成
        /// </summary>
        public string ReWorkStatus {get; set; } = "";
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
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
        /// 总返工片数
        /// </summary>
        public decimal? TotalPieceQty { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }
        /// <summary>
        /// 返工工序
        /// </summary>
        public string ReworkProcess { get; set; }

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
