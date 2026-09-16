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
    /// 3.功能描述: PM_PrinterWorkOrderBG实体
    /// 4.任务编号: 印刷计划工单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_PrinterWorkOrderBGEntity : BaseEntity
    { 
        #region 表: PM_PrinterWorkOrderBG 实体类: PM_PrinterWorkOrderBG 
 
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
        /// 工单号
        /// </summary>
        public string WorkOrder {get; set; }

        /// <summary>
        /// 物料
        /// </summary>
        public string MaterialCode { get; set; }
        

        /// <summary>
        /// 报工工序
        /// </summary>
        public string ProcessCode {get; set; } 
 
        /// <summary>
        /// 人员组别
        /// </summary>
        public string UserGroup {get; set; } 
 
        /// <summary>
        /// 报工卷数
        /// </summary>
        public decimal? ReelNum {get; set; }
 
        /// <summary>
        /// 报工米数
        /// </summary>
        public decimal? MeterNum {get; set; }
        /// <summary>
        /// 报工kg数
        /// </summary>
        public decimal? WeightNum { get; set; }
        /// <summary>
        /// 成品批次
        /// </summary>
        public string BatchNo {get; set; }
        /// <summary>
        /// 成品批次
        /// </summary>
        public string BatchDate { get; set; }
        

        /// <summary>
        /// 白料批次
        /// </summary>
        public string WhiteBatchNo {get; set; } 
 
        /// <summary>
        /// 报工日期
        /// </summary>
        public DateTime? BGTime {get; set; }
 
        /// <summary>
        /// 报工班组
        /// </summary>
        public string Team {get; set; } 
 
        /// <summary>
        /// 报工人
        /// </summary>
        public string Creator {get; set; } 
 
        /// <summary>
        /// 报工时间
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
