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
    /// 3.功能描述: PL_WorkOrderSortRule实体
    /// 4.任务编号: 工单排序规则
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_WorkOrderSortRuleEntity : BaseEntity
    { 
        #region 表: PL_WorkOrderSortRule 实体类: PL_WorkOrderSortRule 
 
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
        /// 排序规则编码
        /// </summary>
        public string RuleCode {get; set; } 
   

        /// <summary>
        /// 排序规则名称
        /// </summary>
        public string RuleName {get; set; } 
        /// <summary>
        /// 字段编码
        /// </summary>
        public string FieldCode { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName {get; set; } 
 
        /// <summary>
        /// 排序
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 正反向
        /// </summary>
        public bool IsAsc {get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } 
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } 
 
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
