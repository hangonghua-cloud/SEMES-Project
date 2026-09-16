using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-21
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_PackingRecord实体
    /// 4.任务编号: 包装记录表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_PackingRecordEntity : BaseEntity
    { 
        #region 表: PM_PackingRecord 实体类: PM_PackingRecord 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder {get; set; } = "";
 
        /// <summary>
        /// 生产工单号
        /// </summary>
        public string WorkOrder {get; set; } = "";
 
        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO {get; set; } = "";
 
        /// <summary>
        /// PO号
        /// </summary>
        public string CustomerPO {get; set; } = "";
 
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode {get; set; } = "";
 
        /// <summary>
        /// 包装状态
        /// </summary>
        public string PackingStatus {get; set; } = "";
 
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
