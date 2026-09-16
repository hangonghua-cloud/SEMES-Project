using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-12
    /// 2.创建作者: admin
    /// 3.功能描述: PM_StartUpRecord实体
    /// 4.任务编号: 开工记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_StartUpRecordEntity : BaseEntity
    { 
        #region 表: PM_StartUpRecord 实体类: PM_StartUpRecord 
 
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
        /// 开工工序编码
        /// </summary>
        public string ProcessCode {get; set; }
        /// <summary>
        /// 开工工序名称
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 机台编码
        /// </summary>
        public string MachineCode { get; set; }
        /// <summary>
        /// 机台名称
        /// </summary>
        public string MachineName { get; set; }
        /// <summary>
        /// 流转卡编码
        /// </summary>
        public string CardCode {get; set; }
 
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
        public bool IsEnabled {get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
 
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
