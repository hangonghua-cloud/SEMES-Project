using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-21
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_PackingBGTransferCard实体
    /// 4.任务编号: 包装记录表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_PackingBGTransferCardEntity : BaseEntity
    { 
        #region 表: PM_PackingBGTransferCard 实体类: PM_PackingBGTransferCard 
 
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
        /// 订单号
        /// </summary>
        public string ProductOrder {get; set; } = "";
 
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder {get; set; } = "";
 
        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO {get; set; } = "";
 
        /// <summary>
        /// PO号
        /// </summary>
        public string CustomerPO {get; set; } = "";
 
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode {get; set; } = "";
        /// <summary>
        /// 纸盒型号
        /// </summary>
        public string PaperBox { get; set; }

        /// <summary>
        /// 流转卡编码
        /// </summary>
        public string CardCode { get; set; } = "";
        /// <summary>
        /// 流转卡名称
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// 成品返工单号
        /// </summary>
        public string ReWorkOrder {get; set; } = "";
 
        /// <summary>
        /// 报工数量
        /// </summary>
        public decimal? Qty {get; set; }
        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal? BadQty { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
 
        /// <summary>
        /// 报工工序编码
        /// </summary>
        public string ProcessCode {get; set; } = "";
        /// <summary>
        /// 机台编码
        /// </summary>
        public string MachineCode { get; set; }
        /// <summary>
        /// 总岗位系数
        /// </summary>
        public decimal? TotalCoefficient { get; set; }
        /// <summary>
        /// 班组人数
        /// </summary>
        public int? PeopleQty { get; set; }
      
        /// <summary>
        /// 设备系数
        /// </summary>
        public decimal? EquipCoefficient { get; set; }

        /// <summary>
        /// 是否计算工资
        /// </summary>
        public bool? IsCalculated { get; set; }
        /// <summary>
        /// 是否生成工资
        /// </summary>
        public int? IsGenerated { get; set; }
        /// <summary>
        /// 错误原因
        /// </summary>
        public string ErrorReason { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 报工人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 报工时间
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
        /// 直接人工
        /// </summary>
        public decimal? VGW01 { get; set; }
        /// <summary>
        /// 间接人工
        /// </summary>
        public decimal? VGW02 { get; set; }
        /// <summary>
        /// 燃料动力
        /// </summary>
        public decimal? VGW03 { get; set; }
        /// <summary>
        /// 折旧摊销
        /// </summary>
        public decimal? VGW04 { get; set; }
        /// <summary>
        /// 备品备件
        /// </summary>
        public decimal? VGW05 { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal? VGW06 { get; set; }

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
