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
    /// 3.功能描述: PM_ReworkRecord_Detail实体
    /// 4.任务编号: PM_生产返工记录明细
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_ReworkRecord_DetailEntity : BaseEntity
    {
        #region 表: PM_ReworkRecord_Detail 实体类: PM_ReworkRecord_Detail 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 返工主表Id
        /// </summary>
        public string ReworkId { get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 流转卡编码
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// 托号
        /// </summary>
        public string CardName { get; set; }
        /// <summary>
        /// 返工前报工数量
        /// </summary>
        public decimal? BGQty { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

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
        /// 返工状态  1：未开始 3：已完成 5：已作废
        /// </summary>
        public string ReworkStatus { get; set; }

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
