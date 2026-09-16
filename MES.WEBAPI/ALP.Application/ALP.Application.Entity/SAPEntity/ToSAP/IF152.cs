using ALP.Application.Entity.HTTPEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity.ToSAP
{
    /// <summary>
    /// 报工/领退料/出入库数据接收接口
    /// </summary>
    public class IF152 : SAPRequestDto
    {
        public IF152_RSQ_DATA RSQ_DATA { get; set; }
    }

    public class IF152_RSQ_DATA
    {
        public IF152_HEAD IS_HEAD { get; set; }
        public List<IF152_ITEM1> IT_ITEM1 { get; set; }
        public List<IF152_ITEM2> IT_ITEM2 { get; set; }
        public List<IF152_ITEM3> IT_ITEM3 { get; set; }
    }
    public class IF152_HEAD
    {
        /// <summary>
        /// 业务标识
        /// </summary>
        public string ZBTYPE { get; set; } = "";
        /// <summary>
        /// 业务类型描述
        /// </summary>
        public string ZTYPE { get; set; } = "";
    }
    /// <summary>
    /// 报工
    /// </summary>
    public class IF152_ITEM1
    {
        /// <summary>
        /// MESID
        /// </summary>
        public string MESID { get; set; } = "";
        /// <summary>
        /// MES时间戳
        /// </summary>
        public string ZTIME { get; set; } = "";
        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; } = "";
        /// <summary>
        /// 工单号
        /// </summary>
        public string AUFNR { get; set; } = "";
        /// <summary>
        /// 工艺路线编号
        /// </summary>
        public string PLNNR_ALT { get; set; } = "";
        /// <summary>
        /// 工序编号
        /// </summary>
        public string KTSCH { get; set; } = "";
        /// <summary>
        /// 报工日期
        /// </summary>
        public string BUDAT { get; set; } = "";
        /// <summary>
        /// 报工人员
        /// </summary>
        public string TXT1 { get; set; } = "";
        /// <summary>
        /// 良品数量
        /// </summary>
        public string NUM1 { get; set; } = "";
        /// <summary>
        /// 不良品数量
        /// </summary>
        public string NUM2 { get; set; } = "";
        /// <summary>
        /// 报工备注
        /// </summary>
        public string TXT2 { get; set; } = "";
        /// <summary>
        /// 直接人工（S） 分摊系数*报工数量
        /// </summary>
        public string VGW01 { get; set; } = "";
        /// <summary>
        /// 间接人工（S） 分摊系数*报工数量
        /// </summary>
        public string VGW02 { get; set; } = "";
        /// <summary>
        /// 燃料动力（S） 分摊系数*报工数量
        /// </summary>
        public string VGW03 { get; set; } = "";
        /// <summary>
        /// 折旧摊销（S)  分摊系数*报工数量
        /// </summary>
        public string VGW04 { get; set; } = "";
        /// <summary>
        /// 备品备件（PC） 分摊系数*报工数量
        /// </summary>
        public string VGW05 { get; set; } = "";
        /// <summary>
        /// 其他费用（PC） 分摊系数*报工数量
        /// </summary>
        public string VGW06 { get; set; } = "";

    }
    /// <summary>
    /// 领退料
    /// </summary>
    public class IF152_ITEM2
    {
        /// <summary>
        /// MESID
        /// </summary>
        public string MESID { get; set; } = "";
        /// <summary>
        /// MES时间戳
        /// </summary>
        public string ZTIME { get; set; } = "";
        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; } = "";
        /// <summary>
        /// 工单号
        /// </summary>
        public string AUFNR { get; set; } = "";
        /// <summary>
        /// 凭证中的过帐日期
        /// </summary>
        public string BUDAT { get; set; } = "";
        /// <summary>
        /// 凭证抬头文本
        /// </summary>
        public string BKTXT { get; set; } = "";
        /// <summary>
        /// 外部物料单号
        /// </summary>
        public string MTSNR { get; set; } = "";
        /// <summary>
        /// 物料编号
        /// </summary>
        public string MATNR { get; set; } = "";
        /// <summary>
        /// 批次号
        /// </summary>
        public string CHARG { get; set; } = "";
        /// <summary>
        /// 数量
        /// </summary>
        public string ERFMG { get; set; } = "";
        /// <summary>
        /// 物料类别
        /// </summary>
        public string ISHS { get; set; } = "";
        /// <summary>
        /// 库存地点
        /// </summary>
        public string LGORT { get; set; } = "";
        /// <summary>
        /// 项目文本(操作人)
        /// </summary>
        public string SGTXT { get; set; } = "";
    }
    /// <summary>
    /// 入库
    /// </summary>
    public class IF152_ITEM3
    {
        /// <summary>
        /// MESID
        /// </summary>
        public string MESID { get; set; } = "";
        /// <summary>
        /// MES时间戳
        /// </summary>
        public string ZTIME { get; set; } = "";
        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; } = "";
        /// <summary>
        /// 入库类型 1-正常入库-101  2-跨柜入库-101+413E  3-超产品入库-101+414
        /// </summary>
        public string ZTYPE { get; set; } = "";
        /// <summary>
        /// 凭证中的过帐日期
        /// </summary>
        public string BUDAT { get; set; } = "";
        /// <summary>
        /// 凭证抬头文本
        /// </summary>
        public string BKTXT { get; set; } = "";
        /// <summary>
        /// 外部交货单编号
        /// </summary>
        public string LFSNR { get; set; } = "";
        /// <summary>
        /// 工单号
        /// </summary>
        public string AUFNR { get; set; } = "";
        /// <summary>
        /// 数量
        /// </summary>
        public string ERFMG { get; set; } = "";
        /// <summary>
        /// 仓储地点
        /// </summary>
        public string LGOBE { get; set; } = "";
        /// <summary>
        /// 批号
        /// </summary>
        public string CHARG { get; set; } = "";
        /// <summary>
        /// 已评估的销售订单存货名称(传输过帐)
        /// </summary>
        public string UMMAT_KDAUF { get; set; } = "";
        /// <summary>
        /// 已评估的销售订单存货项目(传输过帐)
        /// </summary>
        public string UMMAT_KDPOS { get; set; } = "";
    }
}
