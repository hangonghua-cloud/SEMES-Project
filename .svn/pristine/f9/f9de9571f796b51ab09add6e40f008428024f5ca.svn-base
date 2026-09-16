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
    /// 3.功能描述: PM_TransferCardScrapRecord实体
    /// 4.任务编号: PM_流转卡报废记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_TransferCardScrapRecordEntity : BaseEntity
    { 
        #region 表: PM_TransferCardScrapRecord 实体类: PM_TransferCardScrapRecord 
 
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
        /// 流转卡编码
        /// </summary>
        public string CardCode {get; set; }
 
        /// <summary>
        /// 报废工序
        /// </summary>
        public string ProcessCode {get; set; }
 
        /// <summary>
        /// 报废数量
        /// </summary>
        public decimal? ScrapQty {get; set; }
 
        /// <summary>
        /// 报废原因
        /// </summary>
        public string ScarpReaon {get; set; }
 
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator {get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remak {get; set; }
 
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
        /// 有效标记
        /// </summary>
        public bool IsEnabled {get; set; }
 
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
