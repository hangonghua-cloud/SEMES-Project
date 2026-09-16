using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-09-08
    /// 2.创建作者: admin
    /// 3.功能描述: MM_SupProductStockTransfer实体
    /// 4.任务编号: 超产品库存流转记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_SupProductStockTransferEntity : BaseEntity
    { 
        #region 表: MM_SupProductStockTransfer 实体类: MM_SupProductStockTransfer 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string ProcessCode {get; set; }
        /// <summary>
        /// 业务类型 1：转入 2：转出
        /// </summary>
        public string BusinessType { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec {get; set; }
 
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode {get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 面膜型号
        /// </summary>
        public string MMXH {get; set; }
        /// <summary>
        /// 大小转换
        /// </summary>
        public decimal? DXZH { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo {get; set; }
 
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhsCode {get; set; }
 
        /// <summary>
        /// 数量(片)
        /// </summary>
        public decimal? Qty {get; set; }
 
        /// <summary>
        /// 转出订单
        /// </summary>
        public string ProductOrder {get; set; }
 
        /// <summary>
        /// 转出柜号
        /// </summary>
        public string ContainerNO {get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyBy {get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }
        /// <summary>
        /// 转出类型 1：已锁定 0：未锁定
        /// </summary>
        public string OutType { get; set; }
        /// <summary>
        /// 执行工单号
        /// </summary>
        public string ExeWorkOrder { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WhsName { get; set; }
        /// <summary>
        /// 库位编码
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// 库位名称
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        /// 是否过账 1：已过账
        /// </summary>
        public string IsPosted { get; set; }
        /// <summary>
        /// 过账消息
        /// </summary>
        public string PostedMsg { get; set; }
        /// <summary>
        /// 过账时间
        /// </summary>
        public DateTime? PostedTime { get; set; }
        /// <summary>
        /// 过账人员
        /// </summary>
        public string PostedUser { get; set; }
        /// <summary>
        /// SAP物料凭证编号
        /// </summary>
        public string SAP_MBLNR { get; set; }
        /// <summary>
        /// SAP年份
        /// </summary>
        public string SAP_MJAHR { get; set; }

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
