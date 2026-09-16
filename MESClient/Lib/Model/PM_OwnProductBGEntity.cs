using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_OwnProductBG实体
    /// 4.任务编号: 自制半成品报工
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_OwnProductBGEntity 
    {

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 报工ID
        /// </summary>
        public string OwnProductId { get; set; }
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
        /// 物料类别
        /// </summary>
        public string MaterialClass { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 流转卡编码
        /// </summary>
        public string TransferCode { get; set; }

        /// <summary>
        /// 流转卡名称
        /// </summary>
        public string TransferName { get; set; }

        /// <summary>
        /// 报工工序
        /// </summary>
        public string BGProcess { get; set; }

        /// <summary>
        /// 生产机台
        /// </summary>
        public string BGMachine { get; set; }

        /// <summary>
        /// 报工数量
        /// </summary>
        public decimal? BGQty { get; set; }
        /// <summary>
        /// 报工米数
        /// </summary>
        public decimal? Meters { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal? BadQty { get; set; }

        /// <summary>
        /// 报工班次
        /// </summary>
        public string BGShift { get; set; }

        /// <summary>
        /// 报工小组
        /// </summary>
        public string UserGroup { get; set; }
        /// <summary>
        /// 报工人
        /// </summary>
        public string BGUser { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 返工任务明细Id
        /// </summary>
        public string ReworkDId { get; set; }
        /// <summary>
        /// 总岗位系数
        /// </summary>
        public decimal? TotalCoefficient { get; set; }
        /// <summary>
        /// 班组人数
        /// </summary>
        public int? PeopleQty { get; set; }
        /// <summary>
        /// 产品单价
        /// </summary>
        public decimal? Price { get; set; }
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
        /// 错误原因 1:产品工价未维护  2：岗位系数未维护 3：设备系数未维护
        /// </summary>
        public string ErrorReason { get; set; }

        /// <summary>
        /// 单卷条码号
        /// </summary>
        public string RollCode { get; set; }
        /// <summary>
        /// 检验员
        /// </summary>
        public string Inspector { get; set; }
        /// <summary>
        /// 班组人员
        /// </summary>
        public string UserNames { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 报工日期
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
        /// 已报工数量
        /// </summary>
        [NotMapped]
        public decimal? HasBGQty { get; set; }
        /// <summary>
        /// 物料小类编码
        /// </summary>
        [NotMapped]
        public string SmallClass { get; set; }
        /// <summary>
        /// 物料小类名称
        /// </summary>
        [NotMapped]
        public string SmallClassName { get; set; }
    }
}
