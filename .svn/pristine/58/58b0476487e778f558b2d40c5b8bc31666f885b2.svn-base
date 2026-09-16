using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.EquipmentManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-24
    /// 2.创建作者: liyongguo
    /// 3.功能描述: EP_EquipmentMalfunctionRepair实体
    /// 4.任务编号: 设备报修
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentMalfunctionRepairEntity : BaseEntity
    {
        #region 表: EP_EquipmentMalfunctionRepair 实体类: EP_EquipmentMalfunctionRepair 

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
        /// 设备编码
        /// </summary>
        public string EquipmentId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; }

        /// <summary>
        /// 报修类别
        /// </summary>
        public string RepairingType { get; set; }

        /// <summary>
        /// 报修状态
        /// </summary>
        public string RepairingStatus { get; set; }

        /// <summary>
        /// 故障描述
        /// </summary>
        public string MalfunctionDescription { get; set; }

        /// <summary>
        /// 维修时长
        /// </summary>
        public string TimeLength { get; set; }

        /// <summary>
        /// 维修内容
        /// </summary>
        public string RepairingContent { get; set; }

        /// <summary>
        /// 维修人
        /// </summary>
        public string RepairingPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 报修人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 报修时间
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
