using ALP.Application.Entity.HTTPEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity.ToSAP
{
    /// <summary>
    /// 交货单修改
    /// </summary>
    public class IF150 : SAPRequestDto
    {
        public IF150_RSQ_DATA RSQ_DATA { get; set; }
    }

    public class IF150_RSQ_DATA
    {
        public IF150_DATA IS_DATA { get; set; }
        public List<IF150_ITEM> IT_ITEM { get; set; }
    }

    public class IF150_DATA
    {
        /// <summary>
        /// 过账标识 X：过账  空：修改
        /// </summary>
        public string ZGZ { get; set; } = "";
        /// <summary>
        /// 装运地点 （工厂编码）
        /// </summary>
        public string VSTEL { get; set; } = "";
        /// <summary>
        /// 交货单号
        /// </summary>
        public string VBELN { get; set; } = "";
        /// <summary>
        /// 发票号
        /// </summary>
        public string ZFPN { get; set; } = "";
        /// <summary>
        /// 提单号
        /// </summary>
        public string ZTDN { get; set; } = "";
        /// <summary>
        /// 件数
        /// </summary>
        public string ZJS { get; set; } = "";
        /// <summary>
        /// 集装箱ID
        /// </summary>
        public string ZJZID { get; set; } = "";
        /// <summary>
        /// 封箱号
        /// </summary>
        public string ZFXN { get; set; } = "";
        /// <summary>
        /// 车牌号
        /// </summary>
        public string ZCID { get; set; } = "";
        /// <summary>
        /// 叉车工
        /// </summary>
        public string ZCCG { get; set; } = "";
        /// <summary>
        /// 木工
        /// </summary>
        public string ZMG { get; set; } = "";
        /// <summary>
        /// 发货人
        /// </summary>
        public string ZFHR { get; set; } = "";
        /// <summary>
        /// 变更日期
        /// </summary>
        public string ZDAT3 { get; set; } = "";
        /// <summary>
        /// 变更时间
        /// </summary>
        public string ZTIM3 { get; set; } = "";
        /// <summary>
        /// 变更人
        /// </summary>
        public string ZNAM3 { get; set; } = "";
        /// <summary>
        /// 过账日期  过账标识为X时必填
        /// </summary>
        public string WADAT_IST { get; set; } = "";
        /// <summary>
        /// MES发货日期
        /// </summary>
        public string ZMFHD { get; set; } = "";
    }

    public class IF150_ITEM
    {
        /// <summary>
        /// 交货项目（行号）
        /// </summary>
        public string POSNR { get; set; } = "";
        /// <summary>
        /// 批次拆分行
        /// </summary>
        public string ZPOSNR { get; set; } = "";
        /// <summary>
        /// 销售订单号
        /// </summary>
        public string VGBEL { get; set; } = "";
        /// <summary>
        /// 订单行号
        /// </summary>
        public string VGPOS { get; set; } = "";
        /// <summary>
        /// 物料编号
        /// </summary>
        public string MATNR { get; set; } = "";
        /// <summary>
        /// 数量
        /// </summary>
        public string LFIMG { get; set; } = "";
        /// <summary>
        /// 库存地点（MES库位）
        /// </summary>
        public string LGORT { get; set; } = "";
        /// <summary>
        /// 批号
        /// </summary>
        public string CHARG { get; set; } = "";
        /// <summary>
        /// 托数
        /// </summary>
        public string ZTS { get; set; } = "";
        /// <summary>
        /// 发货总盒数
        /// </summary>
        public string ZFZHE { get; set; } = "";
        /// <summary>
        /// 发货总托数
        /// </summary>
        public string ZFZT { get; set; } = "";
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
