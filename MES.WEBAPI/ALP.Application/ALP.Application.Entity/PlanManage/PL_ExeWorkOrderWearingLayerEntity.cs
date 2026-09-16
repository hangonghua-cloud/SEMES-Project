using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.PlanManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-09-01
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_ExeWorkOrderWearingLayer实体
    /// 4.任务编号: 执行工单耐磨层发料
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_ExeWorkOrderWearingLayerEntity : BaseEntity
    { 
        #region 表: PL_ExeWorkOrderWearingLayer 实体类: PL_ExeWorkOrderWearingLayer 
 
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
        /// 执行工单号
        /// </summary>
        public string ExeWorkOrder {get; set; } 
 
        /// <summary>
        /// 耐磨层编码
        /// </summary>
        public string WearingLayerCode {get; set; } 
 
        /// <summary>
        /// 耐磨层名称
        /// </summary>
        public string WearingLayerName {get; set; } 
 
        /// <summary>
        /// 应发数量
        /// </summary>
        public decimal? ShouldNum {get; set; }
 
        /// <summary>
        /// 实发数量
        /// </summary>
        public decimal? ActualNum {get; set; }
 
        /// <summary>
        /// 超发数量
        /// </summary>
        public decimal? SuperNum {get; set; }
 
        /// <summary>
        /// 退库
        /// </summary>
        public decimal? CancellingNum {get; set; }
 
        /// <summary>
        /// 实际消耗数量
        /// </summary>
        public decimal? ConsumeNum {get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string WhsCode { get; set; }
        /// <summary>
        /// 库位
        /// </summary>
        public string LocationCode { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } 
 
        /// <summary>
        /// 创建时间
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
