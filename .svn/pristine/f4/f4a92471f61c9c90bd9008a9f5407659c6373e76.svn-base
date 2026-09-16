using ALP.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity
{

    /// <summary>
    /// 描述：接收SAP发货单信息，包括成品、半成品、原材料
    /// 创建：jpf
    /// 时间：2024-3-11 08:57:22
    /// </summary>
  public class SAPMM_ProductDispatchItemEntity
    {
        public MM_ProductDispatchItem ProductDispatchItem;
        public List<DispatchDetail> DispatchDetails;
    }
    public class MM_ProductDispatchItem
    {
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 发货单类型 10：成品
        /// </summary>
        public string DocType { get; set; }
        /// <summary>
        /// 发货单号
        /// </summary>
        public string DocNum { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder { get; set; }
        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO { get; set; }
        /// <summary>
        /// 客户PO号
        /// </summary>
        public string CustomerPO { get; set; }

        /// <summary>
        /// 发货类型
        /// </summary>
        public string DeliveryType { get; set; } = "";

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNO { get; set; }
        /// <summary>
        /// 发货状态
        /// </summary>
        public string Status { get; set; } = "";
        /// <summary>
        /// 提单号
        /// </summary>
        public string LoadingBill { get; set; } = "";
        /// <summary>
        /// 发货日期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 件数(盒)
        /// </summary>
        public decimal? BoxNum { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        public decimal? GrossWeight { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        public decimal? Volume { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? IsDeleted { get; set; }
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
        /// 创建日期
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
    }
    



    public class DispatchDetail
    {
        /// <summary>
        /// 原材料发货为物料的，成品为客户型号
        /// </summary>
        public string MaterialCode { get; set; } = "";

        /// <summary>
        /// 行号
        /// </summary>
        public string LineNum { get; set; } = "";

        /// <summary>
        /// 托数
        /// </summary>
        public decimal? PalletQty { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DecimalPrecision(18, 3)]
        public decimal? MaterialQty { get; set; }
        /// <summary>
        /// 单位编码
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        public decimal? DetailGrossWeight { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        public decimal? DetailVolume { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder { get; set; }
        /// <summary>
        /// 订单行号
        /// </summary>
        public string ProductLine { get; set; }


    }
}
