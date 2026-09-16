using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-23
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_PrinterOrder实体
    /// 4.任务编号: 印刷计划工单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_PrinterOrderEntity : BaseEntity
    { 
        #region 表: PM_PrinterOrder 实体类: PM_PrinterOrder 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } 
 
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode {get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public string ProcessCode {get; set; } 
 
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderType {get; set; } 
 
        /// <summary>
        /// 订单号
        /// </summary>
        public string PrinterOrder {get; set; } 
 
        /// <summary>
        /// 计划号
        /// </summary>
        public string PlanOrder {get; set; } 
 
        /// <summary>
        /// 打包工艺
        /// </summary>
        public string PackageProcess {get; set; } 
 
        /// <summary>
        /// 下单日期
        /// </summary>
        public DateTime? OrderDate {get; set; }
 
        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime? DeliveryDate {get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } 
 
        /// <summary>
        /// 制单人
        /// </summary>
        public string Creator {get; set; } 
 
        /// <summary>
        /// 制单时间
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
