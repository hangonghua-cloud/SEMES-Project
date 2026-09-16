using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_ProductionOrder实体
    /// 4.任务编号: 生产订单表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Serializable]
    public class PL_ProductionOrderEntity : BaseEntity
    {
        #region 表: PL_ProductionOrder 实体类: PL_ProductionOrder 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 生产订单
        /// </summary>
        public string ProductOrder { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 生产计划号
        /// </summary>
        public string ProductPlanNo { get; set; }

        /// <summary>
        /// 下单日期
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 纸盒日期
        /// </summary>
        public DateTime? BoxDate { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        /// 工艺要求
        /// </summary>
        public string Technology { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

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
        /// <summary>
        /// 业务员
        /// </summary>
        public string Salesman { get; set; }
        /// <summary>
        /// 审核人编码
        /// </summary>
        public string AuditBy { get; set; }
        /// <summary>
        /// 审核人名称
        /// </summary>
        public string AuditName { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditTime { get; set; }

        public string IssueStatus { get; set; }
        /// <summary>
        /// 审核下载人
        /// </summary>
        public string CDownloadUser { get; set; }

        /// <summary>
        /// 审核下载时间
        /// </summary>
        public DateTime? CDownloadTime { get; set; }
        /// <summary>
        /// 生产下载人
        /// </summary>
        public string PDownloadUser { get; set; }
        /// <summary>
        /// 生产下载时间
        /// </summary>
        public DateTime? PDownloadTime { get; set; }

        /// <summary>
        /// 产品组，接SAP数据  10：成品
        /// </summary>
        [NotMapped]
        public string productionGroup { get; set; }

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
