using ALP.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-02
    /// 2.创建作者: liyongguo
    /// 3.功能描述: MM_RawMaterialOut实体
    /// 4.任务编号: 原材料出库表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_RawMaterialOutEntity : BaseEntity
    {
        #region 表: MM_RawMaterialOut 实体类: MM_RawMaterialOut 

        /// <summary>
        /// 原材料出库表Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 业务表Id
        /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
        /// 业务表名称
        /// </summary>
        public string BusinessTable { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 报工类型 1：流转卡 2：印刷报工 3：包装报工 4：自制半成品报工 5：喂料小料报工 6：磨粉料报工
        /// </summary>
        public string BGType { get; set; }

        /// <summary>
        /// 报工批次
        /// </summary>
        public string BGBatchNo { get; set; }

        /// <summary>
        /// 流转卡号
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// 订单单号
        /// </summary>
        public string ProductOrder { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }

        /// <summary>
        /// 客户PO号
        /// </summary>
        public string CustomerPO { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO { get; set; }

        /// <summary>
        /// 执行工单号
        /// </summary>
        public string ExeWorkOrder { get; set; }

        /// <summary>
        /// 工序
        /// </summary>
        public string ProcessCode { get; set; }

        /// <summary>
        ///半成品规格型号
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 客户型号名称
        /// </summary>
        public string CustomerModelName { get; set; }

        /// <summary>
        /// 报工数量
        /// </summary>
        public decimal? BGQty { get; set; }

        /// <summary>
        /// 成品单位
        /// </summary>
        public string ProductUnit { get; set; }

        /// <summary>
        /// 客户型号
        /// </summary>
        public string CustomerModel { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string DocNum { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhsCode { get; set; }

        /// <summary>
        /// 库位编码
        /// </summary>
        public string LocationCode { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 物料小类
        /// </summary>
        public string SmallClass { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        public string SupplierCode { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 出库类型 1:报工 2：移库 3：调拨 4：盘库 5：销售 6：发料 7：跨工厂调拨 8：发料退库 10：原材料出库 11：原材料发货 12：合批出库
        /// </summary>
        public string OutType { get; set; }

        /// <summary>
        /// 出库数量
        /// </summary>
        [DecimalPrecision(18, 3)]
        public decimal? Qty { get; set; }
        /// <summary>
        /// 出库前数量
        /// </summary>
        [DecimalPrecision(18, 3)]
        public decimal? BeforeQty { get; set; }
        /// <summary>
        /// 出库后数量
        /// </summary>
        [DecimalPrecision(18, 3)]
        public decimal? AfterQty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 关联号
        /// </summary>
        public string AssociateNo { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 车间编码
        /// </summary>
        public string WorkShopCode { get; set; }
        /// <summary>
        /// 车间名称
        /// </summary>
        public string WorkShopName { get; set; }
        /// <summary>
        /// 过账日期
        /// </summary>
        public DateTime? PostDate { get; set; }
        /// <summary>
        /// 移动类型
        /// </summary>
        public string MoveType { get; set; }
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
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 物料类型
        /// </summary>
        public string MaterialType { get; set; }
        /// <summary>
        /// 发货单号
        /// </summary>
        public string BaseNum { get; set; }
        /// <summary>
        /// 发货单行号
        /// </summary>
        public string BaseLine { get; set; }
        /// <summary>
        /// 订单行号
        /// </summary>
        public string OrderLine { get; set; }

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
