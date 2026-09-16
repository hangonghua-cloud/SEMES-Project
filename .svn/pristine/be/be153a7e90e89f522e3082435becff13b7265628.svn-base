using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-06
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_TransferCardResume实体
    /// 4.任务编号: 流转卡履历表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Serializable]
    public class PM_TransferCardResumeEntity : BaseEntity
    {
        #region 表: PM_TransferCardResume 实体类: PM_TransferCardResume 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public string ProcessCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        [NotMapped]
        public string ProcessName { get; set; }

        /// <summary>
        /// 流转卡编码
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType { get; set; }

        /// <summary>
        /// 操作Id
        /// </summary>
        public string OperationId { get; set; }

        /// <summary>
        /// 流转标识
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public string WhsCode { get; set; }

        /// <summary>
        /// 库位
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// 张数
        /// </summary>
        public decimal? SheetQty { get; set; }
        /// <summary>
        /// 片数
        /// </summary>
        public decimal? PieceQty { get; set; }

        /// <summary>
        /// 在库标识
        /// </summary>
        public string IsInWHs { get; set; }

        /// <summary>
        /// 报工人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 报工时间
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
        /// 有效标识
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [NotMapped]
        public string ProductOrder { get; set; }
        /// <summary>
        /// 柜号
        /// </summary>
        [NotMapped]
        public string ContainerNO { get; set; }
        /// <summary>
        /// 客户型号
        /// </summary>
        [NotMapped]
        public string MaterialCode { get; set; }
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
