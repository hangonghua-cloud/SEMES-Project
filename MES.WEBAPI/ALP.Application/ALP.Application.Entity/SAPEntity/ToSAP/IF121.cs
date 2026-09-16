using ALP.Application.Entity.HTTPEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity.ToSAP
{
    /// <summary>
    /// SAP成本收集器物料消耗与冲销接口  MES 喂料小料、磨粉料
    /// </summary>
    public class IF121: SAPRequestDto
    {
        public IF121_RSQ_DATA RSQ_DATA { get; set; }
    }
    public class IF121_RSQ_DATA
    {
        public IF121_HEAD IS_HEAD { get; set; }

        public List<IF121_ITEM> IT_ITEM { get; set; }
    }
    public class IF121_HEAD
    {
        /// <summary>
        /// 产成品料号（SAP）
        /// </summary>
        public string MATNR { get; set; } = "";
        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; } = "";
        /// <summary>
        /// BOM编号
        /// </summary>
        public string BOMCODE { get; set; } = "";
        /// <summary>
        /// 过账日期
        /// </summary>
        public string BUDAT { get; set; } = "";
        /// <summary>
        /// 凭证日期
        /// </summary>
        public string BLDAT { get; set; } = "";
        /// <summary>
        /// 入库数量
        /// </summary>
        public string ERFMG { get; set; } = "";

        /// <summary>
        /// 入库库位
        /// </summary>
        public string ALORT { get; set; } = "";

        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG { get; set; } = "";
        /// <summary>
        /// 预留字段1
        /// </summary>
        public string YULIU1 { get; set; } = "";
        /// <summary>
        /// 预留字段2
        /// </summary>
        public string YULIU2 { get; set; } = "";
    }
    public class IF121_ITEM
    {
        
        /// <summary>
        /// 组件料号（SAP）
        /// </summary>
        public string MATNR { get; set; } = "";
        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; } = "";
        /// <summary>
        /// 发料仓位
        /// </summary>
        public string LGORT { get; set; } = "";
        /// <summary>
        /// 消耗数量
        /// </summary>
        public string ERFMG_R { get; set; } = "";
        /// <summary>
        /// 组件单位
        /// </summary>
        public string ERFME { get; set; } = "";
        /// <summary>
        /// 批次号
        /// </summary>
        public string CHARG { get; set; } = "";
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
