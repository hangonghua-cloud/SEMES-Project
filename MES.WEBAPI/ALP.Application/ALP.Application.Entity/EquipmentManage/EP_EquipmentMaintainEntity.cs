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
    /// 3.功能描述: EP_EquipmentMaintain实体
    /// 4.任务编号: 设备保养项目维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentMaintainEntity : BaseEntity
    { 
        #region 表: EP_EquipmentMaintain 实体类: EP_EquipmentMaintain 
 
        /// <summary>
        /// 主键
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 工厂
        /// </summary>
        public string Factory { get; set; }

        /// <summary>
        /// 设备类别
        /// </summary>
        public string EquipmentType {get; set; } = "";
 
        /// <summary>
        /// 保养任务编码
        /// </summary>
        public string EquipmentTaskId {get; set; } = "";
 
        /// <summary>
        /// 保养任务名称
        /// </summary>
        public string EquipmentTaskName {get; set; } = "";
 
        /// <summary>
        /// 保养周期
        /// </summary>
        public string Period {get; set; } = "";
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } = "";
 
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsUsed {get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public bool EnabledMark { get; set; } = true;
 
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
