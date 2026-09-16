using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-03
    /// 2.创建作者: admin
    /// 3.功能描述: PM_ExeWorkOrderSW实体
    /// 4.任务编号: 派工执行工单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_ExeWorkOrderSWEntity : BaseEntity
    { 
        #region 表: PM_ExeWorkOrderSW 实体类: PM_ExeWorkOrderSW 
 
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
        /// 派工顺序
        /// </summary>
        public decimal? SWSeq {get; set; }
 
        /// <summary>
        /// 工序编码
        /// </summary>
        public string ProcessCode {get; set; }
 
        /// <summary>
        /// 机台编码
        /// </summary>
        public string EquipCode {get; set; }
 
        /// <summary>
        /// 计划生产时间
        /// </summary>
        public DateTime? PlanProductTime {get; set; }
 
        /// <summary>
        /// 派工人
        /// </summary>
        public string Creator {get; set; }
 
        /// <summary>
        /// 派工时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; }
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }
 
        /// <summary>
        /// 派工生产状态 1:未开工 2：正在生产 3：已完成
        /// </summary>
        public string SWStatus {get; set; }
 
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
