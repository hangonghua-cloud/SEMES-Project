using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-09-08
    /// 2.创建作者: admin
    /// 3.功能描述: MM_SuperProductStock实体
    /// 4.任务编号: 超产品库存
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_SuperProductStockEntity : BaseEntity
    { 
        #region 表: MM_SuperProductStock 实体类: MM_SuperProductStock 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; }
 
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode {get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string ProcessCode {get; set; }
 
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode {get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec {get; set; }
 
        /// <summary>
        /// 面膜型号
        /// </summary>
        public string MMXH {get; set; }
 
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo {get; set; }
 
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhsCode {get; set; }
 
        /// <summary>
        /// 库存数量(片)
        /// </summary>
        public decimal? StockQty {get; set; }
 
        /// <summary>
        /// 锁定数量/片
        /// </summary>
        public decimal? LockedQty {get; set; }
        /// <summary>
        /// 大小张转换
        /// </summary>
        public decimal? DXZH { get; set; }
        
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyBy {get; set; }
 
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WhsName { get; set; }
        /// <summary>
        /// 库位编码
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// 库位名称
        /// </summary>
        public string LocationName { get; set; }

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
