using ALP.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-04
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_PlanStoreIssue实体
    /// 4.任务编号: 工单发料表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Serializable]
    public class PL_PlanStoreIssueEntity : BaseEntity
    {
        #region 表: PL_PlanStoreIssue 实体类: PL_PlanStoreIssue 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        ///// <summary>
        ///// 订单号
        ///// </summary>
        //public string ProductOrder {get; set; } 
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 计划工单号
        /// </summary>
        public string WorkOrder { get; set; }
        /// <summary>
        /// 订单或者生产
        /// </summary>
        public string OrderOrProduct { get; set; }

        /// <summary>
        /// 面膜发料状态 1:未发料 2:已发料
        /// </summary>
        public string MaskStatus { get; set; }

        /// <summary>
        /// 耐磨层发料状态 1：未发料
        /// </summary>
        public string WearLayerStatus { get; set; }

        /// <summary>
        /// 计划张数
        /// </summary>
        public decimal? OrderNum { get; set; }

        /// <summary>
        /// 生产张数
        /// </summary>
        public decimal? ProductNum { get; set; }

        /// <summary>
        /// 未生成执行工单数量/张
        /// </summary>
        public decimal? UnProductNum { get; set; }

        /// <summary>
        /// 理论数量/米
        /// </summary>
        public decimal? ShouldNum { get; set; }

        /// <summary>
        /// 良率
        /// </summary>
        public decimal? Yield { get; set; }

        /// <summary>
        /// 大小张
        /// </summary>
        public decimal? DXZH { get; set; }
        /// <summary>
        /// 面膜单耗
        /// </summary>
        [DecimalPrecision(10, 5)]
        public decimal? MaskConsume { get; set; }

        /// <summary>
        /// 耐磨层单耗
        /// </summary>
        [DecimalPrecision(10, 5)]
        public decimal? WearLayerConsume { get; set; }
        /// <summary>
        /// 拆分流水号
        /// </summary>
        public string StoreIssueNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string WhsCode { get; set; }
        /// <summary>
        /// 库位
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }

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
