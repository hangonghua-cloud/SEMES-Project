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
    /// 3.功能描述: BS_BOM实体
    /// 4.任务编号: 供应商管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_BOMEntity : BaseEntity
    { 
        #region 表: BS_BOM 实体类: BS_BOM 
 
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
        /// 物料分类
        /// </summary>
        public string MaterialClass {get; set; } 
 
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BOMCode {get; set; } 
 
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode {get; set; } 
 
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName {get; set; } 

        /// <summary>
        /// 单位数量
        /// </summary>
        [DecimalPrecision(18,3)]
        public decimal? UnitNum {get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 订单类型 内销 出口
        /// </summary>
        public string OrderType { get; set; } 

        /// <summary>
        /// 工艺
        /// </summary>
        public string Process {get; set; } 
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } 
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } 
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTimeOffset? ModifyTime {get; set; }
        /// <summary>
        /// BOM类型
        /// </summary>
        public string BOMType { get; set; }
        /// <summary>
        /// 是否默认BOM
        /// </summary>
        public bool? isDefault { get; set; }
       
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; }
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
