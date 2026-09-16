using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-11-01
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_PrinterWorkOrder实体
    /// 4.任务编号: 印刷工单表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_PrinterWorkOrderEntity : BaseEntity
    { 
        #region 表: PM_PrinterWorkOrder 实体类: PM_PrinterWorkOrder 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
        /// <summary>
        /// 工厂
        /// </summary>
        public string FactoryCode { get; set; } = "";
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string PrinterOrder {get; set; } = "";
 
        /// <summary>
        /// 计划工单号
        /// </summary>
        public string PlanOrder {get; set; } = "";
 
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder {get; set; } = "";
 
        /// <summary>
        /// 下单日期
        /// </summary>
        public string OrderDate {get; set; } = "";
 
        /// <summary>
        /// 交货日期
        /// </summary>
        public string DeliveryDate {get; set; } = "";
 
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode {get; set; } = "";
 
        /// <summary>
        /// 版筒号
        /// </summary>
        public string Cylinder {get; set; } = "";
 
        /// <summary>
        /// 花色号
        /// </summary>
        public string DesignColour {get; set; } = "";
 
        /// <summary>
        /// 生产状态
        /// </summary>
        public string WorkOrderType {get; set; } = "";
 
        /// <summary>
        /// 生产米数
        /// </summary>
        public decimal? MeterNum {get; set; }
 
        /// <summary>
        /// 生产卷数
        /// </summary>
        public decimal? ReelNum {get; set; }
 
        /// <summary>
        /// 卷#
        /// </summary>
        public string Reel {get; set; } = "";
 
        /// <summary>
        /// 生产托数
        /// </summary>
        public string Pallet {get; set; }
 
        /// <summary>
        /// 工艺路线
        /// </summary>
        public string ProcessRoute {get; set; } = "";
 
        /// <summary>
        /// 生产批次
        /// </summary>
        public string BatchNo {get; set; } = "";
 
        /// <summary>
        /// 打包
        /// </summary>
        public string Packaging {get; set; } = "";
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } = "";
 
        /// <summary>
        /// 制单人
        /// </summary>
        public string Creator {get; set; } = "";
        /// <summary>
        /// 制单人
        /// </summary>
        public string CreatorName { get; set; } = "";
        

        /// <summary>
        /// 制单时间
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
