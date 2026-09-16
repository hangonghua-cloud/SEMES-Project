using ALP.Application.Entity.HTTPEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity.ToSAP
{
    /// <summary>
    /// 创建：jpf
    /// 时间：2024-3-25 09:00:31
    /// 描述：调用SAP工单创建接口实体
    /// 
    /// </summary>
    public class IF110 : SAPRequestDto
    {
        public IF110_RSQ_DATA RSQ_DATA { get; set; }
    }
    public class IF110_RSQ_DATA
    {
        public IF110_DATA IS_DATA { get; set; }
    }

    public class IF110_DATA
    {
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string WERKS { get; set; } = "";
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MATNR { get; set; } = "";

        /// <summary>
        /// 工单类型
        /// </summary>
        public string AUART { get; set; } = "";
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
        /// MES工单号
        /// </summary>
        public string ABLAD { get; set; } = "";
        /// <summary>
        /// 工艺编号（MES）
        /// </summary>
        public string PLNNR_ALT { get; set; } = "";
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BOMCODE { get; set; } = "";
        /// <summary>
        /// 销售订单号
        /// </summary>
        public string KDAUF { get; set; } = "";
        /// <summary>
        /// 销售订单行项目
        /// </summary>
        public string KDPOS { get; set; } = "";
        /// <summary>
        /// 工单备注
        /// </summary>
        public string TXT { get; set; } = "";


    }
}
