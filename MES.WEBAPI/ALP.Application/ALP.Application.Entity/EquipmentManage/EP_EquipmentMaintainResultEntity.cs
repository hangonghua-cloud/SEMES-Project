using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.EquipmentManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-05
    /// 2.创建作者: 王坤
    /// 3.功能描述: EP_EquipmentMaintainResult实体
    /// 4.任务编号: 设备保养结果
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentMaintainResultEntity : BaseEntity
    { 
        #region 表: EP_EquipmentMaintainResult 实体类: EP_EquipmentMaintainResult 
 
        /// <summary>
        /// 主键
        /// </summary>
        public string Id {get; set; } = "";
        /// <summary>
        /// 父键
        /// </summary>
        public string ParentId {get; set; } = "";
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
        public string EquipmentMaintainIdentifyCode {get; set; } = "";
 
        /// <summary>
        /// 保养项目编号
        /// </summary>
        public string EquipmentMaintainId {get; set; } = "";
 
        /// <summary>
        /// 保养项目名称
        /// </summary>
        public string EquipmentMaintainName {get; set; } = "";
 
        /// <summary>
        /// 保养结果
        /// </summary>
        public string EquipmentMaintainResult {get; set; } = "";

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
            this.CreateTime = DateTime.Now;
            this.EnabledMark = true;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyTime = DateTime.Now;
            this.EnabledMark = true;
        }
        #endregion
 
        #endregion
    }
}
