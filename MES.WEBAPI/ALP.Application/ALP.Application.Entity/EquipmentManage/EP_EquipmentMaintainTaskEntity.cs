using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.EquipmentManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-05
    /// 2.创建作者: 王坤
    /// 3.功能描述: EP_EquipmentMaintainTask实体
    /// 4.任务编号: 设备保养项目详情
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentMaintainTaskEntity : BaseEntity
    { 
        #region 表: EP_EquipmentMaintainTask 实体类: EP_EquipmentMaintainTask 
 
        /// <summary>
        /// 主键
        /// </summary>
        public string Id {get; set; } = "";

        /// <summary>
        /// 工厂
        /// </summary>
        [NotMapped]
        public string Factory { get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
 
        /// <summary>
        /// 保养工单号
        /// </summary>
        public string EquipmentIdentifyCode {get; set; } = "";
 
        /// <summary>
        /// 工单状态
        /// </summary>
        public string EquipmentIdentifyStatus {get; set; } = "";
 
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipmentId {get; set; } = "";

        /// <summary>
        /// 设备名称
        /// </summary>
        [NotMapped]
        public string EquipmentName { get; set; } = "";
 
        /// <summary>
        /// 保养任务编号
        /// </summary>
        public string EquipmentMaintainTaskId {get; set; } = "";

        /// <summary>
        /// 保养任务名称
        /// </summary>
        [NotMapped]
        public string EquipmentMaintainTaskName { get; set; } = "";

        /// <summary>
        /// 所属工序编码
        /// </summary>
        public string ProcessCode { get; set; } = "";

        /// <summary>
        /// 所属工序名称
        /// </summary>
        [NotMapped]
        public string ProcessName { get; set; } = "";
 
        /// <summary>
        /// 计划保养日期
        /// </summary>
        public DateTime? PlanDate {get; set; }
 
        /// <summary>
        /// 保养人员
        /// </summary>
        public string MaintainPerson {get; set; } = "";
 
        /// <summary>
        /// 保养人员
        /// </summary>
        [NotMapped]
        public string MaintainPersonName {get; set; } = "";

        /// <summary>
        /// 实际保养日期
        /// </summary>
        public DateTime? ActiveDate {get; set; }

        /// <summary>
        /// 保养状态 1:已保养 2：未保养
        /// </summary>
        public string MaintenanceStatus { get; set; } = "";

        /// <summary>
        /// 保养状态
        /// </summary>
        [NotMapped]
        public string MaintenanceStatusName { get; set; } = "";

        /// <summary>
        /// 保养详情
        /// </summary>
        public string Remark {get; set; } = "";

        /// <summary>
        /// 有效标志
        /// </summary>
        public bool EnabledMark { get; set; } = true;

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建人
        /// </summary>
        [NotMapped]
        public string CreatorName {get; set; } = "";
 
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
            this.CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyTime = DateTime.Now;
        }
        #endregion
 
        #endregion
    }
}
