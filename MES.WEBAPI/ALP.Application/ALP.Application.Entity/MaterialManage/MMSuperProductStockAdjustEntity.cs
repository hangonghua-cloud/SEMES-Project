using System;
namespace ALP.Application.Entity.MaterialManage
{
    /// <summary>
    /// [MM_SuperProductStockAdjust]表数据实体类
    /// 描述:超产品库存校准
    /// 作者:dragon
    /// 创建时间:2022-01-10 19:49:52
    /// </summary>
    public class MMSuperProductStockAdjustEntity : BaseEntity
    {
        #region 表: MM_SuperProductStockAdjust 实体类: MMSuperProductStockAdjust
        
        /// <summary>
        /// Id
        /// <summary>
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
        /// 工序编码
        /// <summary>
        public string ProcessCode {get; set; }
        
        /// <summary>
        /// 客户型号
        /// <summary>
        public string MaterialCode {get; set; }
        
        /// <summary>
        /// 物料名称
        /// <summary>
        public string MaterialName {get; set; }
        
        /// <summary>
        /// 规格型号
        /// <summary>
        public string Spec {get; set; }
        
        /// <summary>
        /// 面膜型号
        /// <summary>
        public string MMXH {get; set; }
        
        /// <summary>
        /// 批次号
        /// <summary>
        public string BatchNo {get; set; }
        
        /// <summary>
        /// 大小转换
        /// <summary>
        public decimal? DXZH {get; set; }
        
        /// <summary>
        /// 盘点类型
        /// <summary>
        public string TakeType {get; set; }
        
        /// <summary>
        /// 数量(片)
        /// <summary>
        public decimal? Qty {get; set; }
        
        /// <summary>
        /// 创建人
        /// <summary>
        public string Creator {get; set; }
        
        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime? CreateTime {get; set; }
        
        /// <summary>
        /// 修改人
        /// <summary>
        public string ModifyBy {get; set; }
        
        /// <summary>
        /// 修改时间
        /// <summary>
        public DateTime? ModifyTime {get; set; }
        
        /// <summary>
        /// 备注
        /// <summary>
        public string Remark {get; set; }
        /// <summary>
        /// 校准数量
        /// </summary>
        public decimal? AdjustQty { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// <summary>
        public override void Create()
        {
             this.Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// <summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
             this.Id = keyValue;
        }
        #endregion
    }
}

