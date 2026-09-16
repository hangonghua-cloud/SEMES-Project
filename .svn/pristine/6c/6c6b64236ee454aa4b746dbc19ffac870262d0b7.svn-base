using ALP.Application.Entity.HTTPEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity.ToSAP
{
    /// <summary>
    /// SAP采购订单入库
    /// </summary>
    public class IF108: SAPRequestDto
    {
        public IF108_RSQ_DATA RSQ_DATA { get; set; }
    }
    public class IF108_RSQ_DATA
    {
        public IF108_HEAD IS_HEAD { get; set; }

        public List<IF108_ITEM> IT_ITEM { get; set; }
    }
    public class IF108_HEAD
    {
        /// <summary>
        /// 凭证日期
        /// </summary>
        public string BLDAT { get; set; } = "";

        /// <summary>
        /// 过账日期
        /// </summary>
        public string BUDAT { get; set; } = "";
        /// <summary>
        /// 最后发生
        /// </summary>
        public string CPUDT { get; set; } = "";

        /// <summary>
        /// 最后发生日期时分秒
        /// </summary>
        public string CPUTM { get; set; } = "";

        /// <summary>
        /// 最后发送人
        /// </summary>
        public string USNAM { get; set; } = "";

        /// <summary>
        /// 参考凭证编号
        /// </summary>
        public string XBLNR { get; set; } = "";

        /// <summary>
        /// 凭证抬头文本
        /// </summary>
        public string BKTXT { get; set; } = "";

        /// <summary>
        /// 预留字段1
        /// </summary>
        public string YULIU1 { get; set; } = "";
        /// <summary>
        /// 预留字段2
        /// </summary>
        public string YULIU2 { get; set; } = "";
        /// <summary>
        /// 预留字段3
        /// </summary>
        public string YULIU3 { get; set; } = "";
    }
    public class IF108_ITEM
    {
        /// <summary>
        /// 移动类型
        /// </summary>
        public string BWART { get; set; } = "";
        /// <summary>
        /// 物流编号
        /// </summary>
        public string MATNR { get; set; } = "";
        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; } = "";
        /// <summary>
        /// 库存地点
        /// </summary>
        public string LGORT { get; set; } = "";
        /// <summary>
        /// 批次号
        /// </summary>
        public string CHARG { get; set; } = "";
        /// <summary>
        /// 录入单位的数量
        /// </summary>
        public string ERFMG { get; set; } = "";
        /// <summary>
        /// 录入计量单位
        /// </summary>
        public string ERFME { get; set; } = "";
        /// <summary>
        /// 供应商
        /// </summary>
        public string LIFNR { get; set; } = "";
        /// <summary>
        /// 采购订单
        /// </summary>
        public string EBELN { get; set; } = "";
        /// <summary>
        /// 采购订单行项目
        /// </summary>
        public string EBELP { get; set; } = "";
        /// <summary>
        /// 交货单
        /// </summary>
        public string VBELN_IM { get; set; } = "";
        /// <summary>
        /// 交货单行项目
        /// </summary>
        public string VBELP_IM { get; set; } = "";
        /// <summary>
        /// 预留字段1
        /// </summary>
        public string YULIU1 { get; set; } = "";

        /// <summary>
        /// 预留字段2
        /// </summary>
        public string YULIU2 { get; set; } = "";
        /// <summary>
        /// 预留字段1
        /// </summary>
        public string YULIU3 { get; set; } = "";

    }
}
