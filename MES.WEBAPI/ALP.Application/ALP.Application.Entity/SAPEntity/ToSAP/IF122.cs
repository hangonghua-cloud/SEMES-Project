using ALP.Application.Entity.HTTPEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity.ToSAP
{
    /// <summary>
    /// 工单取消入库
    /// </summary>
    public class IF122 : SAPRequestDto
    {
        public IF122_RSQ_DATA RSQ_DATA { get; set; }
    }

    public class IF122_RSQ_DATA
    {
        public IF122_HEAD IS_HEAD { get; set; }
    }

    public class IF122_HEAD
    {
        /// <summary>
        /// 过账日期
        /// </summary>
        public string BUDAT { get; set; } = "";
        /// <summary>
        /// 操作人
        /// </summary>
        public string BKTXT { get; set; } = "";
        /// <summary>
        /// MES操作单号
        /// </summary>
        public string LFSNR { get; set; } = "";
        /// <summary>
        /// SAP工单号
        /// </summary>
        public string AUFNR { get; set; } = "";
        /// <summary>
        /// 物料号
        /// </summary>
        public string MATNR { get; set; } = "";
        /// <summary>
        /// 入库数量
        /// </summary>
        public string ERFMG { get; set; } = "";
        /// <summary>
        /// 入库库位
        /// </summary>
        public string LGOBE { get; set; } = "";
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG { get; set; } = "";
        /// <summary>
        /// 工厂
        /// </summary>
        public string PWERK { get; set; } = "";
    }
}
