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
    /// 3.功能描述: EP_EquipmentAttrib实体
    /// 4.任务编号: 设备台账属性
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentAttribEntity : BaseEntity
    { 
        #region 表: EP_EquipmentAttrib 实体类: EP_EquipmentAttrib 
 
        /// <summary>
        /// 主键
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 设备编码
        /// </summary>
        public string EquipmentId {get; set; } = "";
 
        /// <summary>
        /// 设备类型
        /// </summary>
        public string EquipmentType {get; set; } = "";
 
        /// <summary>
        /// 属性编码
        /// </summary>
        public string AttribCode {get; set; } = "";
 
        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttribName {get; set; } = "";
 
        /// <summary>
        /// 属性值
        /// </summary>
        public string AttribValue {get; set; } = "";
 
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
