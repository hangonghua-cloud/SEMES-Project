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
    ///  收料通知单表
    /// </summary>
    public class MM_ReceiptNoticeEntity : BaseEntity
    { 
        #region 表: MM_ReceiptNotice 实体类: MM_ReceiptNotice 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 采购订单Id
        /// </summary>
        public string PurchaseId {get; set; } = "";
 
        /// <summary>
        /// 收料通知单号
        /// </summary>
        public string ReceiptCode {get; set; } = "";
        /// <summary>
        /// 行号
        /// </summary>
        public string LineNum { get; set; }

        /// <summary>
        /// 是否免检
        /// </summary>
        [NotMapped]
        public string IsExemption { get; set; }
        /// <summary>
        /// 通知到货数量
        /// </summary>
        [DecimalPrecision(18, 3)]
        public decimal? ArrivalQty {get; set; }
 
        /// <summary>
        /// 到货日期
        /// </summary>
        public DateTime? ArrivalTime {get; set; }
 
        /// <summary>
        /// 收料通知单状态 1：创建 2：检验中 3：待入库 4：已完成
        /// </summary>
        public string ReceiptStatus {get; set; } = "";

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = "";

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
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
        ///// <summary>
        ///// jpf add 采购系数
        ///// </summary>
        //[NotMapped]
        //public decimal Coefficient { get; set; }
        /// <summary>
        /// jpf add 采购总数
        /// </summary>
        [NotMapped]
        public decimal PurchaseNum { get; set; }
        /// <summary>
        /// 厂家编码
        /// </summary>
        public string ManufacturerCode { get; set; }
        /// <summary>
        /// 厂家名称
        /// </summary>
        public string ManufacturerName { get; set; }
        /// <summary>
        /// 批次数量
        /// </summary>
        public int? BatchCount { get; set; }
        /// <summary>
        /// 供应商编码2
        /// </summary>
        public string SupplierCode2 { get; set; }
        /// <summary>
        /// 供应商名称2
        /// </summary>
        public string SupplierName2 { get; set; }

        /// <summary>
        /// 供应商编码3
        /// </summary>
        public string SupplierCode3 { get; set; }
        /// <summary>
        /// 供应商名称3
        /// </summary>
        public string SupplierName3 { get; set; }

        /// <summary>
        /// 供应商编码4
        /// </summary>
        public string SupplierCode4 { get; set; }
        /// <summary>
        /// 供应商名称4
        /// </summary>
        public string SupplierName4 { get; set; }

        /// <summary>
        /// 供应商编码5
        /// </summary>
        public string SupplierCode5 { get; set; }
        /// <summary>
        /// 供应商名称5
        /// </summary>
        public string SupplierName5 { get; set; }

        /// <summary>
        /// 供应商编码6
        /// </summary>
        public string SupplierCode6 { get; set; }
        /// <summary>
        /// 供应商名称6
        /// </summary>
        public string SupplierName6 { get; set; }
        /// <summary>
        /// 最后一次入库时间
        /// </summary>
        public DateTime? InWhsTime { get; set; }
        /// <summary>
        /// 采购订单号
        /// </summary>
        public string PurchaseOrder { get; set; }
        /// <summary>
        /// 采购订单行号
        /// </summary>
        public string PurchaseOrderLineNum { get; set; }
        /// <summary>
        /// 是否删除标识
        /// SAP
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 物料小类
        /// </summary>
        public string SmallClass { get; set; }
        /// <summary>
        /// 物料小类名称
        /// </summary>
        public string SmallClassName { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string SupplierCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName{ get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        [DecimalPrecision(18, 3)]
        public decimal? InQty { get; set; }
        /// <summary>
        /// 销售订单号
        /// </summary>
        public string ProductOrder { get; set; }

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
