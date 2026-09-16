using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-19
    /// 2.创建作者: admin
    /// 3.功能描述: PM_ReworkRecord实体
    /// 4.任务编号: PM_生产返工记录明细
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_ReworkRecordEntity : BaseEntity
    { 
        #region 表: PM_ReworkRecord 实体类: PM_ReworkRecord 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 返工单号
        /// </summary>
        public string ReworkOrder {get; set; }
 
        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder {get; set; }
 
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder {get; set; }
 
        /// <summary>
        /// 执行工单号
        /// </summary>
        public string ExeWorkOrder {get; set; }
        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO { get; set; }

        /// <summary>
        /// 当前工序
        /// </summary>
        public string CurrentProcess {get; set; }
 
        /// <summary>
        /// 责任工序
        /// </summary>
        public string DutyProcess {get; set; }
 
        /// <summary>
        /// 返工工序
        /// </summary>
        public string ReworkProcess {get; set; }
 
        /// <summary>
        /// 返工状态 1：未开发 2：正在返工 3：已完成
        /// </summary>
        public string Status {get; set; }
 
        /// <summary>
        /// 确认状态 0：未确认 1：已确认
        /// </summary>
        public string ConfirmStatus {get; set; }
 
        /// <summary>
        /// 质量确认人
        /// </summary>
        public string QualityConfirmUser {get; set; }
 
        /// <summary>
        /// 质量确认时间
        /// </summary>
        public DateTime? QualityConfirmTime {get; set; }
 
        /// <summary>
        /// 创建人编码
        /// </summary>
        public string Creator {get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 修改人编码
        /// </summary>
        public string ModifyBy {get; set; }
 
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }
 
        /// <summary>
        /// 有效标记
        /// </summary>
        public bool? IsEnabled {get; set; }
        /// <summary>
        /// 返工托数
        /// </summary>
        public decimal? PalletQty { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 返工产品类型 1：正常 2：自制
        /// </summary>
        public string ReworkProductType { get; set; }
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
