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
    /// 3.功能描述: PM_MaterialBatchConsumeRecord实体
    /// 4.任务编号: PM_物料批次消耗记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_MaterialBatchConsumeRecordEntity : BaseEntity
    { 
        #region 表: PM_MaterialBatchConsumeRecord 实体类: PM_MaterialBatchConsumeRecord 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; }
 
        /// <summary>
        /// 报工ID
        /// </summary>
        public string BGID {get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode {get; set; }
 
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName {get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec {get; set; }
 
        /// <summary>
        /// 物料组
        /// </summary>
        public string GroupCode {get; set; }
 
        /// <summary>
        /// 物料批次
        /// </summary>
        public string BatchNo {get; set; }
 
        /// <summary>
        /// 倒冲数量
        /// </summary>
        public decimal? RecoilQty {get; set; }
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
        /// 单位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhsCode { get; set; }
        /// <summary>
        /// 库位编码
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// 物料类别（回收料累类型）
        /// </summary>
        [NotMapped]
        public string MaterialType { get; set; }

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
