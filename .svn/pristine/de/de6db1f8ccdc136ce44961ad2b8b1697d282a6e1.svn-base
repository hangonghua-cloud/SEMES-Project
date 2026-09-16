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
    /// 3.功能描述: EP_EquipmentTool实体
    /// 4.任务编号: 设备刀具更换记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentToolEntity : BaseEntity
    { 
        #region 表: EP_EquipmentTool 实体类: EP_EquipmentTool 
 
        /// <summary>
        /// 主键
        /// </summary>
        public string Id {get; set; } = "";

        /// <summary>
        /// 工厂
        /// </summary>
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
        /// 设备编码
        /// </summary>
        public string EquipmentId {get; set; } = "";
 
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName {get; set; } = "";

        /// <summary>
        /// 机台编码
        /// </summary>
        public string LineCode {get; set; } = "";
 
        /// <summary>
        /// 机台名称
        /// </summary>
        public string LineName {get; set; } = "";
 
        /// <summary>
        /// 刀具编码
        /// </summary>
        public string ToolId {get; set; } = "";
 
        /// <summary>
        /// 刀具名称
        /// </summary>
        public string ToolsName {get; set; } = "";
 
        /// <summary>
        /// 规格型号
        /// </summary>
        public string SpecificationsModels {get; set; } = "";
 
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string Manufacturer { get; set; } = "";
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } = "";
 
        /// <summary>
        /// 使用状态
        /// </summary>
        public bool IsUsed {get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public bool EnabledMark { get; set; } = true;
 
        /// <summary>
        /// 更换日期
        /// </summary>
        public DateTime? DateOfReplace {get; set; }
 
        /// <summary>
        /// 更换人
        /// </summary>
        public string PersonOfReplace {get; set; } = "";
 
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
