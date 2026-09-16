using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-20
    /// 2.创建作者: admin
    /// 3.功能描述: MM_ProductReworkBG实体
    /// 4.任务编号: 成品返工报工
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_ProductReworkBGEntity : BaseEntity
    { 
        #region 表: MM_ProductReworkBG 实体类: MM_ProductReworkBG 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 成品返工单号
        /// </summary>
        public string ReworkOrder {get; set; } = "";
 
        /// <summary>
        /// 工序编码
        /// </summary>
        public string ProcessCode {get; set; } = "";
 
        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder {get; set; } = "";
 
        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO {get; set; } = "";
 
        /// <summary>
        /// 客户PO号
        /// </summary>
        public string CustomerPO {get; set; } = "";
 
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode {get; set; } = "";
 
        /// <summary>
        /// 本次返工良品片数
        /// </summary>
        public decimal? PieceQty {get; set; }
        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal? BadQty { get; set; }

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
