using ALP.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: BS_BOMItems实体
    /// 4.任务编号: 供应商管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_BOMItemsEntity : BaseEntity
    { 
        #region 表: BS_BOMItems 实体类: BS_BOMItems 
 
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
        /// 物料类别
        /// </summary>
        public string MaterialType { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [NotMapped]
        public string ProductCode { get; set; }
        /// <summary>
        /// 订单类型 内销 出口
        /// </summary>
        [NotMapped]
        public string OrderType { get; set; }
        /// <summary>
        /// BOM主键
        /// </summary>
        public string BOMId {get; set; } = "";
 
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode {get; set; } = "";
 
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName {get; set; } = "";
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BOMCode { get; set; } = "";
        /// <summary>
        /// 数量
        /// </summary>
        [DecimalPrecision(18,3)]
        public decimal? Num {get; set; }
 
        /// <summary>
        /// 单位编码
        /// </summary>
        public string Unit {get; set; } = "";
        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 库存地点
        /// </summary>
        public string Warehouse {get; set; } = "";
        
        /// <summary>
        /// 消耗工序
        /// </summary>
        public string ConsumeProcess {get; set; } = "";
        /// <summary>
        /// 倒冲 否0
        /// </summary>
        public string Backflush { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } = "";
 
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
