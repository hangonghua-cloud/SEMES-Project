using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_OwnProductTransfer实体
    /// 4.任务编号: 自制半成品工单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_OwnProductTransferEntity : BaseEntity
    { 
        #region 表: PM_OwnProductTransfer 实体类: PM_OwnProductTransfer 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 自制半成品Id
        /// </summary>
        public string WorkOrder { get; set; } = "";
        /// <summary>
        /// 物料编码
        /// </summary>
        [NotMapped]
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [NotMapped]
        public string MaterialName { get; set; }
        /// <summary>
        /// 流转卡号
        /// </summary>
        public decimal? TransferBatch { get; set; }

        /// <summary>
        /// 流转卡号
        /// </summary>
        public string TransferCode {get; set; } = "";

        /// <summary>
        /// 流转卡名称
        /// </summary>
        public string TransferName {get; set; } = "";
 
        /// <summary>
        /// 流转卡状态
        /// </summary>
        public string TransferStatus {get; set; } = "";
        /// <summary>
        /// 生产人员
        /// </summary>
        public string UserNames { get; set; } = "";
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
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

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; } = "";
        /// <summary>
        /// 流转卡状态
        /// </summary>
        public string CardStatus { get; set; }


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
