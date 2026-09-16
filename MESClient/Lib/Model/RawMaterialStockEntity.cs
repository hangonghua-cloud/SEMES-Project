using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
    [Serializable]
    public class RawMaterialStockEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string FactoryCode { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 规格
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
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? Qty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string SupplierCode { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhsCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WhsName { get; set; }

        /// <summary>
        /// 库位编码
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// 库位名称
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// 冻结标识
        /// </summary>
        public string IsFrozen { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 批次日期
        /// </summary>
        public DateTime? BatchDate { get; set; }
        /// <summary>
        /// 打印日期
        /// </summary>
        public string PrintTime { get; set; }
    }
}
