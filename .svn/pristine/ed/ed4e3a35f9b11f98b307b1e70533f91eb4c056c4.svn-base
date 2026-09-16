using ALP.Application.Entity.HTTPEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity.ToSAP
{
    /// <summary>
    /// 销售交货单过账/冲销接口
    /// 创建：jpf
    /// 时间：2024-3-23 09:43:12
    /// </summary>
    public class IF106 : SAPRequestDto
    {
        public IF106_RSQ_DATA RSQ_DATA { get; set; }
    }

    public class IF106_RSQ_DATA
    {
        public IF106_HEAD IS_HEAD { get; set; }

    }

    public  class IF106_HEAD
    {
        /// <summary>
        /// 过账标识
        /// </summary>
        public string ZGZ { get; set; } = "";

        /// <summary>
        /// 装运点/接收点
        /// </summary>
        public string VSTEL { get; set; } = "";

        /// <summary>
        /// 销售和分销凭证号
        /// </summary>
        public string VBELN { get; set; } = "";

        /// <summary>
        /// 实际货物移动日期
        /// </summary>
        public string WADAT_IST { get; set; } = "";
        /// <summary>
        /// MES传输日期
        /// </summary>
        public string ZMESDATE { get; set; } = "";
        /// <summary>
        /// MES传输时间
        /// </summary>
        public string ZMESTIME { get; set; } = "";
        /// <summary>
        /// 预留字段1
        /// </summary>
        public string ZRESERVATION1 { get; set; } = "";
        /// <summary>
        /// 预留字段2
        /// </summary>
        public string ZRESERVATION2 { get; set; } = "";
        /// <summary>
        /// 预留字段3
        /// </summary>
        public string ZRESERVATION3 { get; set; } = "";
        /// <summary>
        /// 预留字段4
        /// </summary>
        public string ZRESERVATION4 { get; set; } = "";
        /// <summary>
        /// 预留字段5
        /// </summary>
        public string ZRESERVATION5 { get; set; } = "";
       

    }
}
