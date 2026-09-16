using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-11
    /// 2.创建作者: admin
    /// 3.功能描述: MM_ProductStock实体
    /// 4.任务编号: 成品库存详表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_ProductStockEntity : BaseEntity
    { 
        #region 表: MM_ProductStock 实体类: MM_ProductStock 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode {get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 唛头码
        /// </summary>
        public string MarkCode {get; set; } = "";
 
        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder {get; set; } = "";
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO {get; set; } = "";
 
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode {get; set; } = "";
 
        /// <summary>
        /// 客户PO号
        /// </summary>
        public string CustomerPO {get; set; } = "";
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhsCode {get; set; } = "";
 
        /// <summary>
        /// 库位编码
        /// </summary>
        public string LocationCode {get; set; } = "";
        
        /// <summary>
        /// 库存托数
        /// </summary>
        public decimal? PalletQty { get; set; }
        /// <summary>
        /// 单托片数
        /// </summary>
        public decimal? PerPalletPieceQty { get; set; }

        /// <summary>
        /// 库存片数
        /// </summary>
        public decimal? PieceQty { get; set; }
        /// <summary>
        /// 单托盒数
        /// </summary>
        public decimal? PerPalletBoxQty { get; set; }

        /// <summary>
        /// 库存盒数
        /// </summary>
        public decimal? BoxQty {get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } = "";
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }
 
        /// <summary>
        /// 冻结标记
        /// </summary>
        public bool IsEnabled {get; set; }
        /// <summary>
        /// 特殊库存标识
        /// </summary>
        public string Specialmark { get; set; }
        /// <summary>
        /// 订单行号
        /// </summary>
        public string LineNum { get; set; }

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
 
        #endregion
    }
}
