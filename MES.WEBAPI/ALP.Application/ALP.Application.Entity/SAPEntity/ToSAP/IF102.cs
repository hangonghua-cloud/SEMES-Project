using ALP.Application.Entity.HTTPEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity.ToSAP
{
    /// <summary>
    /// 调拨过账
    /// </summary>
    public class IF102 : SAPRequestDto
    {
        public IF102_RSQ_DATA RSQ_DATA { get; set; }
    }
    public class IF102_RSQ_DATA
    {
        public IF102_HEAD IS_HEAD { get; set; }

        public List<IF102_ITEM> IT_ITEM { get; set; }
    }
    public class IF102_HEAD
    {
        /// <summary>
        /// 凭证中的凭证日期
        /// </summary>
        public string BLDAT { get; set; } = "";
        /// <summary>
        /// 凭证中的过帐日期
        /// </summary>
        public string BUDAT { get; set; } = "";
        /// <summary>
        /// 会计凭证输入日期
        /// </summary>
        public string CPUDT { get; set; } = "";
        /// <summary>
        /// 输入时间
        /// </summary>
        public string CPUTM { get; set; } = "";
        /// <summary>
        /// 用户名 
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
    public class IF102_ITEM
    {
        /// <summary>
        /// 移动类型(库存管理)  311：原材料移库、调拨   413：成品入库、移库、调柜，特殊库存标识传 E
        /// </summary>
        public string BWART { get; set; } = "";

        /// <summary>
        /// 物料编号
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
        /// 批号
        /// </summary>
        public string CHARG { get; set; } = "";
        /// <summary>
        /// 以录入项单位表示的数量 
        /// </summary>
        public string ERFMG { get; set; } = "";
        /// <summary>
        /// 条目单位
        /// </summary>
        public string ERFME { get; set; } = "";
        /// <summary>
        /// 销售订单号
        /// </summary>
        public string KDAUF { get; set; } = "";
        /// <summary>
        /// 销售订单行项目号
        /// </summary>
        public string KDPOS { get; set; } = "";
        /// <summary>
        /// 特殊库存标识
        /// </summary>
        public string SOBKZ { get; set; } = "";
        /// <summary>
        /// 接受工厂
        /// </summary>
        public string UMWRK { get; set; } = "";
        /// <summary>
        /// 接受库存地点
        /// </summary>
        public string UMLGO { get; set; } = "";

        /// <summary>
        /// 接受批次
        /// </summary>
        public string UMCHA { get; set; } = "";
        /// <summary>
        /// 实地库存转移的特殊库存标识 
        /// </summary>
        public string UMSOK { get; set; } = "";
        /// <summary>
        /// 接受销售订单号
        /// </summary>
        public string MAT_KDAUF { get; set; } = "";
        /// <summary>
        /// 接受销售订单行项目号
        /// </summary>
        public string MAT_KDPOS { get; set; } = "";
        /// <summary>
        /// 预留字段1
        /// </summary>
        public string YULIU1 { get; set; } = "";

        /// <summary>
        /// 预留字段1
        /// </summary>
        public string YULIU2 { get; set; } = "";
        /// <summary>
        /// 预留字段1
        /// </summary>
        public string YULIU3 { get; set; } = "";
    }
}
