using ALP.Application.Entity.HTTPEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity.ToSAP
{
    /// <summary>
    /// 描述：工单领退料接口实体
    /// 创建：jpf
    /// 时间：2024-3-27 14:56:11
    /// </summary>
    public class IF112:SAPRequestDto
    {
        public IF112_RSQ_DATA RSQ_DATA { get; set; }
    }
    public class IF112_RSQ_DATA
    {
        public IF112_DATA IS_DATA { get; set; }

        public List<IF112_ITEM> IS_ITEM { get; set; }
    }

    public class IF112_DATA
    {
        /// <summary>
        /// SAP工单号
        /// </summary>
        public string AUFNR { get; set; } = "";

        /// <summary>
        ///过账日期
        /// </summary>
        public string BUDAT { get; set; } = "";

        /// <summary>
        ///MES操作人
        /// </summary>
        public string BKTXT { get; set; } = "";
        /// <summary>
        ///MES操作单号
        /// </summary>
        public string MTSNR { get; set; } = "";
    }
    public  class IF112_ITEM
    {
        /// <summary>
        ///物料
        /// </summary>
        public string MATNR { get; set; } = "";
        /// <summary>
        ///工厂
        /// </summary>
        public string WERKS { get; set; } = "";
        /// <summary>
        ///批号
        /// </summary>
        public string CHARG { get; set; } = "";
        /// <summary>
        ///数量
        /// </summary>
        public string ERFMG { get; set; } = "";
        /// <summary>
        ///物料类别
        /// </summary>
        public string ISHS { get; set; } = "";
        /// <summary>
        ///发出库位（仓位）
        /// </summary>
        public string LGORT { get; set; } = "";
    }
}
