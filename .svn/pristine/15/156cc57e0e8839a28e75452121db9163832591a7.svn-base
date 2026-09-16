using System;
namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// [PM_OwnSemiProductOrder]表数据实体类
    /// 描述:自制半成品订单管理
    /// 作者:Dragon
    /// 创建时间:2022-12-06 10:24:28
    /// </summary>
    public class PMOwnSemiProductOrderEntity : BaseEntity
    {
        #region 表: PM_OwnSemiProductOrder 实体类: PMOwnSemiProductOrder
        
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
        /// <summary>
        public string FactoryName {get; set; }
        
        /// <summary>
        /// 工序编码
        /// <summary>
        public string ProcessCode {get; set; }
        
        /// <summary>
        /// 工序名称
        /// <summary>
        public string ProcessName {get; set; }
        
        /// <summary>
        /// 订单号
        /// <summary>
        public string ProductOrder {get; set; }
        
        /// <summary>
        /// 物料编码
        /// <summary>
        public string MaterialCode {get; set; }
        
        /// <summary>
        /// 物料名称
        /// <summary>
        public string MaterialName {get; set; }
        
        /// <summary>
        /// 规格
        /// <summary>
        public string Spec {get; set; }
        
        /// <summary>
        /// 订单数量
        /// <summary>
        public decimal? ProductQty {get; set; }
        
        /// <summary>
        /// 柜号
        /// <summary>
        public string ContainerNO {get; set; }
        
        /// <summary>
        /// 托数
        /// <summary>
        public decimal? PalletNum {get; set; }
        
        /// <summary>
        /// 卷数
        /// <summary>
        public decimal? VolumeNum {get; set; }
        
        /// <summary>
        /// 发货数量
        /// <summary>
        public decimal? DeliveryQty {get; set; }
        
        /// <summary>
        /// 单位
        /// <summary>
        public string UnitName {get; set; }
        
        /// <summary>
        /// 米数
        /// <summary>
        public decimal? Meters {get; set; }

        /// <summary>
        /// 订单状态 1:新建 2：进行中 3：已完成
        /// </summary>
        public string OrderStatus {get; set; }
        
        /// <summary>
        /// 客户编码
        /// <summary>
        public string CustomerCode {get; set; }
        
        /// <summary>
        /// 客户名称
        /// <summary>
        public string CustomerName {get; set; }
        
        /// <summary>
        /// 删除标记
        /// <summary>
        public bool? IsDeleted {get; set; }
        
        /// <summary>
        /// 备注
        /// <summary>
        public string Remark {get; set; }
        
        /// <summary>
        /// 创建人编码
        /// <summary>
        public string CreatorCode {get; set; }
        
        /// <summary>
        /// 创建人名称
        /// <summary>
        public string CreatorName {get; set; }
        
        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime? CreateTime {get; set; }
        
        /// <summary>
        /// 修改人编码
        /// <summary>
        public string ModifyCode {get; set; }
        
        /// <summary>
        /// 修改人名称
        /// <summary>
        public string ModifyName {get; set; }
        
        /// <summary>
        /// 最后修改时间
        /// <summary>
        public DateTime? ModifyTime {get; set; }

        /// <summary>
        /// 订单行号 SAP
        /// </summary>
        public string Orderline { get; set; }
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

