using ALP.Application.Entity.HTTPEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity.ToSAP
{
    /// <summary>
    /// 工单修改
    /// </summary>
    public class IF111 : SAPRequestDto
    {
        public IF111_RSQ_DATA RSQ_DATA { get; set; }
    }

    public class IF111_RSQ_DATA
    {
        /// <summary>
        /// 1-抬头、2-组件、3-工序
        /// </summary>
        public string IV_IFLAG { get; set; } = "";

        public IF111_DATA IS_DATA { get; set; }
        public List<IF111_ITEM1> IT_ITEM1 { get; set; }
        public List<IF111_ITEM2> IT_ITEM2 { get; set; }
    }

    public class IF111_DATA
    {
        /// <summary>
        /// SAP工单号
        /// </summary>
        public string AUFNR { get; set; } = "";
        /// <summary>
        /// 工单数量
        /// </summary>
        public string GAMNG { get; set; } = "";
        /// <summary>
        /// 开始日期
        /// </summary>
        public string GSTRP { get; set; } = "";
        /// <summary>
        /// 结束日期
        /// </summary>
        public string GLTRP { get; set; } = "";
        /// <summary>
        /// 工单备注
        /// </summary>
        public string TXT { get; set; } = "";
    }

    public class IF111_ITEM1
    {
        /// <summary>
        /// SAP工单号
        /// </summary>
        public string AUFNR { get; set; } = "";
        /// <summary>
        /// 组件料号
        /// </summary>
        public string IDNRK { get; set; } = "";
        /// <summary>
        /// 需求数量
        /// </summary>
        public string MENGE { get; set; } = "";
        /// <summary>
        /// 发料工序
        /// </summary>
        public string SORTF { get; set; } = "";
        /// <summary>
        /// 外发标识
        /// </summary>
        public string WEMPF { get; set; } = "";
    }

    public class IF111_ITEM2
    {
        /// <summary>
        /// SAP工单号
        /// </summary>
        public string AUFNR { get; set; } = "";
        /// <summary>
        /// 工艺路线编码（MES）
        /// </summary>
        public string PLNNR_ALT { get; set; } = "";
        /// <summary>
        /// 工序编码
        /// </summary>
        public string KTSCH { get; set; } = "";
        /// <summary>
        /// 是否外协
        /// </summary>
        public string CY_SEQNRV { get; set; } = "";

    }
}
