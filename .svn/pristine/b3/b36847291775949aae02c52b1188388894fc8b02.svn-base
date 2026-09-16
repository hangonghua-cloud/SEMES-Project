using System;
namespace ALP.Application.Entity.MaterialManage
{
    /// <summary>
    /// [MM_ProductStockAdjust]表数据实体类
    /// 描述:MM_成品库存校准记录
    /// 作者:dragon
    /// 创建时间:2022-05-14 17:54:49
    /// </summary>
    public class MMProductStockAdjustEntity : BaseEntity
    {
        #region 表: MM_ProductStockAdjust 实体类: MMProductStockAdjust
        
        /// <summary>
        /// Id
        /// <summary>
        public string Id {get; set; }
        
        /// <summary>
        /// 工厂编码
        /// <summary>
        public string FactoryCode {get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 订单号
        /// <summary>
        public string ProductOrder {get; set; }
        
        /// <summary>
        /// 工单号
        /// <summary>
        public string WorkOrder {get; set; }
        
        /// <summary>
        /// 柜号
        /// <summary>
        public string ContainerNO {get; set; }
        
        /// <summary>
        /// 客户型号
        /// <summary>
        public string MaterialCode {get; set; }
        
        /// <summary>
        /// 客户PO号
        /// <summary>
        public string CustomerPO {get; set; }
        
        /// <summary>
        /// 仓库编码
        /// <summary>
        public string WhsCode {get; set; }
        
        /// <summary>
        /// 库位编码
        /// <summary>
        public string LocationCode {get; set; }
        
        /// <summary>
        /// 库存托数
        /// <summary>
        public decimal? PalletQty {get; set; }
        
        /// <summary>
        /// 库存盒数
        /// <summary>
        public decimal? BoxQty {get; set; }
        
        /// <summary>
        /// 单托盒数
        /// <summary>
        public decimal? PerPalletBoxQty {get; set; }
        
        /// <summary>
        /// 校准托数
        /// <summary>
        public decimal? AdjustPalletQty {get; set; }
        
        /// <summary>
        /// 校准盒数
        /// <summary>
        public decimal? AdjustBoxQty {get; set; }
        
        /// <summary>
        /// 创建人编码
        /// <summary>
        public string Creator {get; set; }
        
        /// <summary>
        /// 创建人
        /// <summary>
        public string CreatorName {get; set; }
        
        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime? CreateTime {get; set; }
        
        /// <summary>
        /// 最后修改人编码
        /// <summary>
        public string ModifyBy {get; set; }
        
        /// <summary>
        /// 最后修改人
        /// <summary>
        public string ModifyByName {get; set; }
        
        /// <summary>
        /// 最后修改时间
        /// <summary>
        public DateTime? ModifyTime {get; set; }
        /// <summary>
        /// 库存片数
        /// </summary>
        public decimal? PieceQty { get; set; }
        /// <summary>
        /// 校准片数
        /// </summary>
        public decimal? AdjustPieceQty { get; set; }

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

