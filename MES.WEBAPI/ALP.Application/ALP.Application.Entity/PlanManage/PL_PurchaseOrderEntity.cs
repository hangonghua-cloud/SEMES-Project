using ALP.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_PurchaseOrder实体
    /// 4.任务编号: 采购订单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_PurchaseOrderEntity : BaseEntity
    {
        #region 表: PL_PurchaseOrder 实体类: PL_PurchaseOrder 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 采购订单号
        /// </summary>
        public string PurchaseOrder { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public string LineNum { get; set; }
        /// <summary>
        /// 采购类型
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 生产订单
        /// </summary>
        public string ProductOrder { get; set; }

        /// <summary>
        /// 成品交货日期
        /// </summary>
        public DateTime? ProductDeliveryDate { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 物料规格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 物料小类
        /// </summary>
        public string SmallClass { get; set; }
        /// <summary>
        /// 物料大类
        /// </summary>
        public string MaterialClass { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 应采购数量
        /// </summary>
        [DecimalPrecision(18,3)]
        public decimal? PurchaseNum {get; set; }

        /// <summary>
        /// 下单数量
        /// </summary>
        [DecimalPrecision(18, 3)]
        public decimal? OrderNum {get; set; }
 
        /// <summary>
        /// 采购交货期
        /// </summary>
        public DateTime? PurchaseDeliveryDate {get; set; }
 
        /// <summary>
        /// 到货状态 1：未到货 2：部分到货 3：已到货
        /// </summary>
        public string ArrivalStatus {get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; }
 
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
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 采购到料系数
        /// </summary>
        public string Coefficient { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo { get; set; }

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
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        [DecimalPrecision(18, 3)]
        public decimal? InQty { get; set; }

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
