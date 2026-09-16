using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.QualityManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-23
    /// 2.创建作者: liyongguo
    /// 3.功能描述: QC_PollingDetail实体
    /// 4.任务编号: 巡检检验记录表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_PollingDetailEntity : BaseEntity
    {
        #region 表: QC_PollingDetail 实体类: QC_PollingDetail 

        /// <summary>
        /// 主键
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
        /// 流转卡编码
        /// </summary>
        public string FlowCardId { get; set; } = "";

        /// <summary>
        /// 检验方法
        /// </summary>
        public string CalibrationMethod { get; set; } = "";

        /// <summary>
        /// 检验工序
        /// </summary>
        public string TestProcess { get; set; } = "";

        /// <summary>
        /// 检验机台
        /// </summary>
        public string ProductionMachine { get; set; } = "";

        /// <summary>
        /// 实验室状态
        /// </summary>
        public string LaboratoryTestStatus { get; set; } = "";

        /// <summary>
        /// 判定结果
        /// </summary>
        public string Determination { get; set; } = "";

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = "";

        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment { get; set; } = "";

        /// <summary>
        /// 有效标志
        /// </summary>
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 检验员
        /// </summary>
        public string Inspector { get; set; } = "";

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? InspectionTime { get; set; }

        /// <summary>
        /// 实验室检验人
        /// </summary>
        public string LabInspector { get; set; } = "";

        /// <summary>
        /// 实验室检验时间
        /// </summary>
        public DateTime? LabInspectionTime { get; set; }

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
        /// 订单号
        /// </summary>
        public string ProductOrder { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }
        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO { get; set; }
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 面膜型号
        /// </summary>
        public string MMXH { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec { get; set; }
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
