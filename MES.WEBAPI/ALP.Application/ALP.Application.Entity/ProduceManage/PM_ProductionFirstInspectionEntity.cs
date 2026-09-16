using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-11-09
    /// 2.创建作者: huxiao
    /// 3.功能描述: PM_ProductionFirstInspection实体
    /// 4.任务编号: 任务编号或模块名称
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_ProductionFirstInspectionEntity : BaseEntity
    {
        #region 表: PM_ProductionFirstInspection 实体类: PM_ProductionFirstInspection 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; } = "";
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
        public string ProductOrder { get; set; } = "";

        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; } = "";

        /// <summary>
        /// 执行工单号
        /// </summary>
        public string ExeWorkOrder { get; set; } = "";
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 首检工序编码
        /// </summary>
        public string FirstProcessCode { get; set; } = "";

        /// <summary>
        /// 首检机台
        /// </summary>
        public string FirstMachine { get; set; } = "";

        /// <summary>
        /// 车间主任确认标记
        /// </summary>
        public string SecondMark { get; set; } = "";

        /// <summary>
        /// 车间首检结果 1：合格 2：不合格
        /// </summary>
        public string FirstResult { get; set; } = "";

        /// <summary>
        /// 车间首检人
        /// </summary>
        public string FirstUser { get; set; } = "";

        /// <summary>
        /// 车间首检时间
        /// </summary>
        public DateTime? FirstTime { get; set; }

        /// <summary>
        /// 质量复检结果
        /// </summary>
        public string SecondResult { get; set; } = "";

        /// <summary>
        /// 质量复检人
        /// </summary>
        public string SecondUser { get; set; } = "";

        /// <summary>
        /// 质量复检时间
        /// </summary>
        public DateTime? SecondTime { get; set; }
        /// <summary>
        /// 首检类型 1：车间首检 2:质量首检
        /// </summary>
        public string InspectClass { get; set; }
        /// <summary>
        /// 班次编码
        /// </summary>
        public string ShiftCode { get; set; }
        /// <summary>
        /// 班次名称
        /// </summary>
        public string ShiftName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; } = "";

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy { get; set; } = "";

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }


        /// <summary>
        /// LaboratoryTestStatus
        /// </summary>
        public string LaboratoryTestStatus { get; set; } = "";

        /// <summary>
        /// 车间首检结果 1：合格 2：不合格
        /// </summary>
        public string Determination { get; set; } = "";

        /// <summary>
        /// Attachment
        /// </summary>
        public string Attachment { get; set; } = "";

        /// <summary>
        /// Remarks
        /// </summary>
        public string Remarks { get; set; } = "";

        /// <summary>
        /// CalibrationMethod
        /// </summary>
        public string CalibrationMethod { get; set; }

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
