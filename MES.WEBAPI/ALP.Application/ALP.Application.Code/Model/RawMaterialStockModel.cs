using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Code.Model
{
    public class RawMaterialStockModel
    {
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
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }
        /// <summary>
        /// 执行工单号
        /// </summary>
        public string ExeWorkOrder { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } 

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 库位编码
        /// </summary>
        public string SmallClass { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal? Qty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string SupplierCode { get; set; }

        /// <summary>
        /// 原仓库编码
        /// </summary>
        public string OldWhsCode { get; set; }

        /// <summary>
        /// 原库位编码
        /// </summary>
        public string OldLocationCode { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhsCode { get; set; } 

        /// <summary>
        /// 库位编码
        /// </summary>
        public string LocationCode { get; set; } 
        /// <summary>
        /// 使用
        /// </summary>
        public decimal? ActNum { get; set; }
        /// <summary>
        /// 面膜单耗
        /// </summary>
        public decimal? DanHao { get; set; }
        /// <summary>
        /// 大小张转换
        /// </summary>
        public decimal? DXZH { get; set; }
        /// <summary>
        /// 超发数量
        /// </summary>
        public decimal? SuperNum { get; set; }

    }
}
