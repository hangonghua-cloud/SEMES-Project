using System;
namespace ALP.Application.Entity.MaterialManage
{
    /// <summary>
    /// [MM_RawMaterialDispatch]表数据实体类
    /// 描述:原材料半成品发货单主表
    /// 作者:Dragon
    /// 创建时间:2024-03-13 09:27:07
    /// </summary>
    public class MMRawMaterialDispatchEntity : BaseEntity
    {
        #region 表: MM_RawMaterialDispatch 实体类: MMRawMaterialDispatch

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
        /// 发货单号
        /// <summary>
        public string DeliveryNo { get; set; }

        /// <summary>
        /// 毛重
        /// <summary>
        public decimal? GrossWeight { get; set; }

        /// <summary>
        /// 体积
        /// <summary>
        public decimal? Volume { get; set; }

        /// <summary>
        /// 发票号
        /// <summary>
        public string InvoiceNO { get; set; }

        /// <summary>
        /// 提单号
        /// <summary>
        public string LoadingBill { get; set; }

        /// <summary>
        /// 发货状态 1：未发货 2：发货中 3：已完成
        /// <summary>
        public string Status { get; set; }

        /// <summary>
        /// 发货日期
        /// <summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 集装箱ID
        /// <summary>
        public string ContainerID { get; set; }

        /// <summary>
        /// 车牌号
        /// <summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 叉车工
        /// <summary>
        public string ForkliftWorker { get; set; }

        /// <summary>
        /// 木工
        /// <summary>
        public string WoodWorker { get; set; }

        /// <summary>
        /// 发货人编码
        /// <summary>
        public string DeliveryUserCode { get; set; }

        /// <summary>
        /// 发货人名称
        /// <summary>
        public string DeliveryUserName { get; set; }

        /// <summary>
        /// 实际发货时间
        /// <summary>
        public DateTime? ActualDeliveryTime { get; set; }

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
        /// 报关单日期
        /// </summary>
        public DateTime? CusdeclarationDate { get; set; }
        /// <summary>
        /// 报关单号
        /// </summary>
        public string CusdeclarationNum { get; set; }
        /// <summary>
        /// 港口
        /// </summary>
        public string Harbor { get; set; }
        /// <summary>
        /// 过账日期
        /// </summary>
        public DateTime? PostDate { get; set; }
        /// <summary>
        /// 是否过账 1：已过账
        /// </summary>
        public string IsPosted { get; set; }
        /// <summary>
        /// 过账消息
        /// </summary>
        public string PostedMsg { get; set; }
        /// <summary>
        /// 过账时间
        /// </summary>
        public DateTime? PostedTime { get; set; }
        /// <summary>
        /// 过账人员
        /// </summary>
        public string PostedUser { get; set; }
        /// <summary>
        /// SAP物料凭证编号
        /// </summary>
        public string SAP_MBLNR { get; set; }
        /// <summary>
        /// SAP交货单号
        /// </summary>
        public string SAP_VBELN { get; set; }
        /// <summary>
        /// 过账标识 G：过账 R：冲销
        /// </summary>
        public string PostMark { get; set; }
        /// <summary>
        /// 是否冲销过账 1：已过账
        /// </summary>
        public string Off_IsPosted { get; set; }
        /// <summary>
        /// 冲销过账消息
        /// </summary>
        public string Off_PostedMsg { get; set; }
        /// <summary>
        /// 冲销过账时间
        /// </summary>
        public DateTime? Off_PostedTime { get; set; }
        /// <summary>
        /// 冲销过账人员
        /// </summary>
        public string Off_PostedUser { get; set; }
        /// <summary>
        /// SAP冲销物料凭证编号
        /// </summary>
        public string Off_SAP_MBLNR { get; set; }
        /// <summary>
        /// SAP冲销交货单号
        /// </summary>
        public string Off_SAP_VBELN { get; set; }
        /// <summary>
        /// 封箱号
        /// </summary>
        public string SealingNo { get; set; }
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

