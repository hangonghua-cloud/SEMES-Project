using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-18
    /// 2.创建作者: admin
    /// 3.功能描述: PM_TranferCardBGRecord实体
    /// 4.任务编号: 报工信息
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_TranferCardBGRecordEntity : BaseEntity
    {
        #region 表: PM_TranferCardBGRecord 实体类: PM_TranferCardBGRecord 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }
        /// <summary>
        /// 执行工单
        /// </summary>
        public string ExeWorkOrder { get; set; }

        /// <summary>
        /// 流转卡编码
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// 报工工序编码
        /// </summary>
        public string ProcessCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        [NotMapped]
        public string ProcessName { get; set; }

        /// <summary>
        /// 报工机台编码
        /// </summary>
        public string MachineCode { get; set; }

        /// <summary>
        /// 报工数量
        /// </summary>
        public decimal? Qty { get; set; }

        /// <summary>
        /// 报工单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal? BadQty { get; set; }
        /// <summary>
        /// 养生时间
        /// </summary>
        public DateTime? HealthTime { get; set; }
        /// <summary>
        /// 报工人名称
        /// </summary>
        public string BGUser { get; set; }
        /// <summary>
        /// 返工记录明细Id
        /// </summary>
        public string ReworkDId { get; set; }
        /// <summary>
        /// 返工标识 1：是 0：否
        /// </summary>
        public string IsRework { get; set; }
        /// <summary>
        /// 大小转换
        /// </summary>
        public decimal? DXZH { get; set; }
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
        /// 分拣口
        /// </summary>
        public string SortingPort { get; set; }
        /// <summary>
        /// 是否计算工资
        /// </summary>
        public bool? IsCalculated { get; set; }
        /// <summary>
        /// 是否生成工资 1:未生成 2：已生成  3：计算错误
        /// </summary>
        public int? IsGenerated { get; set; }
        /// <summary>
        /// 错误原因 1:产品工价未维护  2：岗位系数未维护 3：设备系数未维护
        /// </summary>
        public string ErrorReason { get; set; }
        /// <summary>
        /// 有效标记
        /// </summary>
        public bool? IsEnabled { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
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
        /// 报工人员
        /// </summary>
        [NotMapped]
        public string UserNames { get; set; }
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
