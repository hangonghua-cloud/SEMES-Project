using ALP.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-25
    /// 2.创建作者: admin
    /// 3.功能描述: MM_RawMaterialIn实体
    /// 4.任务编号: 原材料入库单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Serializable]
    public class MM_RawMaterialInEntity : BaseEntity
    { 
        #region 表: MM_RawMaterialIn 实体类: MM_RawMaterialIn 
 
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
        /// 业务表Id
        /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
        /// 业务表名称
        /// </summary>
        public string BusinessTable { get; set; }

        /// <summary>
        /// 计划订单
        /// </summary>
        public string ProductOrder {get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }

        /// <summary>
        /// 原材料入库单号
        /// </summary>
        public string DocNum {get; set; }
 
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode {get; set; }
 
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName {get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 物料小类
        /// </summary>
        public string SmallClass { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit {get; set; }
 
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo {get; set; }
 
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string SupplierCode { get; set; }
 
        /// <summary>
        /// 质检状态
        /// </summary>
        public string QualityStatus {get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DecimalPrecision(18, 3)]
        public decimal? Qty {get; set; }
        /// <summary>
        /// 入库前数量
        /// </summary>
        [DecimalPrecision(18, 3)]
        public decimal? BeforeQty { get; set; }
        /// <summary>
        /// 入库后数量
        /// </summary>
        [DecimalPrecision(18, 3)]
        public decimal? AfterQty { get; set; }

        /// <summary>
        /// 入库类型 1：收料 2：移库 3：调拨 4：盘库 5：退库 6：发料 7：新增入库 8：跨工厂调拨 9：报工入库 10:采购入库 11：原材料退库 12：合批入库
        /// </summary>
        public string InType {get; set; }
 
        /// <summary>
        /// 入库仓库
        /// </summary>
        public string WhsCode {get; set; }
 
        /// <summary>
        /// 入库库位
        /// </summary>
        public string LocationCode {get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; }
        /// <summary>
        /// 关联号
        /// </summary>
        public string AssociateNo { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set;}
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; }
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }

        /// <summary>
        /// 批次日期
        /// </summary>
        [NotMapped]
        public DateTime? BatchDate { get; set; }
        /// <summary>
        /// 过账日期
        /// </summary>
        public DateTime? PostDate { get; set; }
        /// <summary>
        /// 移动类型
        /// </summary>
        public string MoveType { get; set; }
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
        /// SAP年份
        /// </summary>
        public string SAP_MJAHR { get; set; }
        /// <summary>
        /// 采购订单号
        /// </summary>
        public string PurchaseOrder { get; set; }
        /// <summary>
        /// 采购订单行号
        /// </summary>
        public string LineNum { get; set; }
        /// <summary>
        /// SAP入库类型
        /// </summary>
        public string SAPInType { get; set; }
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
        /// SAP冲销年份
        /// </summary>
        public string Off_SAP_MJAHR { get; set; }
        /// <summary>
        /// 收料通知单号
        /// </summary>
        public string ReceiptCode { get; set; }
        /// <summary>
        /// 收料通知单行号
        /// </summary>
        public string ReceiptLineNum { get; set; }
        /// <summary>
        /// 车间编码
        /// </summary>
        public string WorkShopCode { get; set; }
        /// <summary>
        /// 车间名称
        /// </summary>
        public string WorkShopName { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyTime = DateTime.Now;
        }
        #endregion
 
        #endregion
    }
}
