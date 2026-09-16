using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-12-05
    /// 2.创建作者: jpf
    /// 3.功能描述: MM_WarehouseSafetyStock实体
    /// 4.任务编号: 仓库安全库存
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_WarehouseSafetyStockEntity : BaseEntity
    { 
        #region 表: MM_WarehouseSafetyStock 实体类: MM_WarehouseSafetyStock 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode {get; set; } = "";
 
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName {get; set; } = "";
 
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhsCode {get; set; } = "";
 
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WhsName {get; set; } = "";
 
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode {get; set; } = "";
 
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName {get; set; } = "";
 
        /// <summary>
        /// 物料规格
        /// </summary>
        public string MaterialSpc {get; set; } = "";
 
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit {get; set; } = "";
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; } = "";
        /// <summary>
        /// 安全库存数
        /// </summary>
        public decimal? SafetyQty {get; set; }
 
        /// <summary>
        /// 删除标识1代表已删除0代表未删除
        /// </summary>
        public bool? IsDelete {get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } = "";
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateName {get; set; } = "";
 
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
        /// 修改人名称
        /// </summary>
        public string ModifyName {get; set; } = "";
 
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
