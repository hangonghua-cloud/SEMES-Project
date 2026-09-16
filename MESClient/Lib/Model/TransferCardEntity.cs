using Lib.Model.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
    [Serializable]
    public class TransferCardEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        ///// <summary>
        ///// 工厂编码
        ///// </summary>
        //public string FactoryCode { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder { get; set; }

        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }

        /// <summary>
        /// 工单类型
        /// </summary>
        public string WorkOrderType { get; set; }

        /// <summary>
        /// 执行工单号
        /// </summary>
        public string ExeWorkOrder { get; set; }

        /// <summary>
        /// 流转卡编码
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// 流转卡名称
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// 流转卡类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO { get; set; }

        /// <summary>
        /// 背胶工艺
        /// </summary>
        public string BJGY { get; set; }

        /// <summary>
        /// 工单备注
        /// </summary>
        public string WorkOrderRemark { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 面膜型号
        /// </summary>
        public string MMXH { get; set; }

        /// <summary>
        /// 挤出规格
        /// </summary>
        public string JCGG { get; set; }

        /// <summary>
        /// 板纹型号
        /// </summary>
        public string BWXH { get; set; }

        /// <summary>
        /// 生产托盘数量（张）
        /// </summary>
        public decimal? SCTPSL { get; set; }

        /// <summary>
        /// 生产托盘数量（片）
        /// </summary>
        public decimal? SCTPSLP { get; set; }

        /// <summary>
        /// 包装托盘数量
        /// </summary>
        public decimal? BZTPSL { get; set; }

        /// <summary>
        /// 开槽扣型
        /// </summary>
        public string KCKX { get; set; }

        /// <summary>
        /// UV
        /// </summary>
        public string UV { get; set; }

        /// <summary>
        /// 订单张数
        /// </summary>
        public decimal? TotalSheets { get; set; }

        /// <summary>
        /// 放量张数
        /// </summary>
        public decimal? ActualSheets { get; set; }

        /// <summary>
        /// 订单片数
        /// </summary>
        public decimal? OrderPieces { get; set; }

        /// <summary>
        /// 生产片数（订单片数*良率）
        /// </summary>
        public decimal? ProductPieces { get; set; }

        /// <summary>
        /// 托盘规格
        /// </summary>
        public string TPGG { get; set; }

        /// <summary>
        /// 单柜拖数
        /// </summary>
        public decimal? OrderPallet { get; set; }

        /// <summary>
        /// 单托盒数
        /// </summary>
        public decimal? PerPallerBox { get; set; }

        /// <summary>
        /// 单盒片数
        /// </summary>
        public decimal? BZDHSL { get; set; }

        /// <summary>
        /// 说明书
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 托盘编号
        /// </summary>
        public string PalletNum { get; set; }

        /// <summary>
        /// 纸盒型号
        /// </summary>
        public string PaperBoxModel { get; set; }

        /// <summary>
        /// 纸盒
        /// </summary>
        public string BoxDate { get; set; }
        /// <summary>
        /// 流转卡状态
        /// </summary>
        public string CardStatus { get; set; }

        /// <summary>
        /// 打印状态 1:未打印  2：已打印 3:可打印
        /// </summary>
        public string PrintStatus { get; set; }

        /// <summary>
        /// 创建人编码
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改人编码
        /// </summary>
        public string ModifyBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 有效标记
        /// </summary>
        public bool? IsEnabled { get; set; }
        /// <summary>
        /// 托盘数量
        /// </summary>
        public decimal? PalletQty { get; set; }
        /// <summary>
        /// 新建类型
        /// </summary>
        public string NewType { get; set; }
        /// <summary>
        /// 起始工序
        /// </summary>
        public string StartProcess { get; set; }
        /// <summary>
        /// 拆托工序
        /// </summary>
        public string SplitProcess { get; set; }
        /// <summary>
        /// 流转方式
        /// </summary>
        public string TransferBy { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 片数
        /// </summary>
        public decimal? PieceQty { get; set; }
        /// <summary>
        /// 工单下的超产品数量
        /// </summary>
        [NotMapped]
        public string SuperQty { get; set; }
        /// <summary>
        /// 大小转换
        /// </summary>
        public decimal? DXZH { get; set; }
        /// <summary>
        /// 显示托盘数量（流转卡）
        /// </summary>
        public string ShowPalletQty { get; set; }
        /// <summary>
        /// 首工序托数
        /// </summary>
        public decimal? StartOperationPalletCount { get; set; }
    }
}
