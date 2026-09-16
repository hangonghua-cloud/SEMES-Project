using System;
namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// [PM_ProductPrice]表数据实体类
    /// 描述:产品工价维护
    /// 作者:Dragon
    /// 创建时间:2022-11-08 14:54:29
    /// </summary>
    public class PMProductPriceEntity : BaseEntity
    {
        #region 表: PM_ProductPrice 实体类: PMProductPrice

        /// <summary>
        /// Id
        /// <summary>
        public string Id { get; set; }

        /// <summary>
        /// 工厂编码
        /// <summary>
        public string FactoryCode { get; set; }

        /// <summary>
        /// 工厂名称
        /// <summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 工序编码
        /// <summary>
        public string ProcessCode { get; set; }

        /// <summary>
        /// 工序名称
        /// <summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 工价类型
        /// </summary>
        public string PriceType { get; set; }

        /// <summary>
        /// 物料编码
        /// <summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 物料名称
        /// <summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 规格
        /// <summary>
        public string Spec { get; set; }

        /// <summary>
        /// 单位
        /// <summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 班组人数
        /// <summary>
        public int? PeopleQty { get; set; }

        /// <summary>
        /// 工价
        /// <summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 是否默认
        /// <summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// 删除标记
        /// <summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// 备注
        /// <summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人编码
        /// <summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建人名称
        /// <summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改人编码
        /// <summary>
        public string ModifyCode { get; set; }

        /// <summary>
        /// 修改人名称
        /// <summary>
        public string ModifyName { get; set; }

        /// <summary>
        /// 最后修改时间
        /// <summary>
        public DateTime? ModifyTime { get; set; }
        
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

