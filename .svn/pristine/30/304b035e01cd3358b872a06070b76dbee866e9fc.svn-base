using System;
namespace ALP.Application.Entity.MaterialManage
{
    /// <summary>
    /// [MM_SemiProductOut]表数据实体类
    /// 描述:半成品出库记录
    /// 作者:Dragon
    /// 创建时间:2022-12-06 10:23:43
    /// </summary>
    public class MMSemiProductOutEntity : BaseEntity
    {
        #region 表: MM_SemiProductOut 实体类: MMSemiProductOut
        
        /// <summary>
        /// Id
        /// <summary>
        public string Id {get; set; }
        
        /// <summary>
        /// 出库Id
        /// <summary>
        public string OutId {get; set; }
        
        /// <summary>
        /// 工厂编码
        /// <summary>
        public string FactoryCode {get; set; }
        
        /// <summary>
        /// 工厂名称
        /// <summary>
        public string FactoryName {get; set; }
        
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
        /// 批次号
        /// <summary>
        public string BatchNo {get; set; }
        
        /// <summary>
        /// 仓库编码
        /// <summary>
        public string WhsCode {get; set; }
        
        /// <summary>
        /// 仓库名称
        /// <summary>
        public string WhsName {get; set; }
        
        /// <summary>
        /// 库位编码
        /// <summary>
        public string LocationCode {get; set; }
        
        /// <summary>
        /// 库位名称
        /// <summary>
        public string LocationName {get; set; }
        
        /// <summary>
        /// 出库数量
        /// <summary>
        public decimal? OutQty {get; set; }
        
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

