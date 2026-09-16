using ALP.Data.Attributes;
using System;
namespace ALP.Application.Entity.MaterialManage
{
    /// <summary>
    /// [MM_RawMaterialDispatchSub]表数据实体类
    /// 描述:MM_原材料半成品发货单子表
    /// 作者:Dragon
    /// 创建时间:2024-03-13 09:27:07
    /// </summary>
    public class MMRawMaterialDispatchSubEntity : BaseEntity
    {
        #region 表: MM_RawMaterialDispatchSub 实体类: MMRawMaterialDispatchSub

        /// <summary>
        /// Id
        /// <summary>
        public string Id { get; set; }

        /// <summary>
        /// 发货单Id
        /// <summary>
        public string DispatchId { get; set; }

        /// <summary>
        /// 发货单号
        /// <summary>
        public string DeliveryNo { get; set; }

        /// <summary>
        /// 物料编码
        /// <summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 物料名称
        /// <summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 数量
        /// <summary>
        [DecimalPrecision(18, 3)]
        public decimal? Qty { get; set; }

        /// <summary>
        /// 发票号单位
        /// <summary>
        public string UnitName { get; set; }

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
        public string CreateByCode { get; set; }

        /// <summary>
        /// 创建人名称
        /// <summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后修改人编码
        /// <summary>
        public string ModifyByCode { get; set; }

        /// <summary>
        /// 最后修改人名称
        /// <summary>
        public string ModifyByName { get; set; }

        /// <summary>
        /// 最后修改时间
        /// <summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 毛重
        /// </summary>
        public decimal? DetailGrossWeight { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal? DetailVolume { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public string LineNum { get; set; }
        /// <summary>
        /// 发货状态 1：未发货 3：已完成
        /// </summary>
        public string SubStatus { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set;}
        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder { get; set; }
        /// <summary>
        /// 订单行号
        /// </summary>
        public string ProductLine { get; set; }
        /// <summary>
        /// 单位编码
        /// </summary>
        public string Unit { get; set; }

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

