using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-18
    /// 2.创建作者: admin
    /// 3.功能描述: PM_BGBadRecord实体
    /// 4.任务编号: 报工信息
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_BGBadRecordEntity : BaseEntity
    { 
        #region 表: PM_BGBadRecord 实体类: PM_BGBadRecord 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; }
 
        /// <summary>
        /// 报工ID
        /// </summary>
        public string BGID {get; set; }
        /// <summary>
        /// 业务表
        /// </summary>
        public string BusinessTable { get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 不良项目编码
        /// </summary>
        public string BadItemCode {get; set; }
 
        /// <summary>
        /// 不良项目名称
        /// </summary>
        public string BadItemName {get; set; }
 
        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal? BadQty {get; set; }
 
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
        /// 报废编码
        /// </summary>
        public string ScrapCode { get; set; }
        /// <summary>
        /// 报废原因不良总数
        /// </summary>
        public decimal? TotalBadQty { get; set; }
        /// <summary>
        /// 不良占比
        /// </summary>
        public decimal? BadRatio { get; set; }
        /// <summary>
        /// 报废记录Id
        /// </summary>
        public string ScrapId { get; set; }
        /// <summary>
        /// 分摊数量
        /// </summary>
        public decimal? ShareQty { get; set; }
        /// <summary>
        /// 分摊状态 0：未分摊 1：已分摊
        /// </summary>
        public string ShareStatus { get; set; }
        /// <summary>
        /// 物料小类编码
        /// </summary>
        public string SmallClassCode { get; set; }

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
