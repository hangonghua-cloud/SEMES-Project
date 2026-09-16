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
    /// 3.功能描述: EP_EquipmentCheckResult实体
    /// 4.任务编号: 设备点检结果
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentCheckResultEntity : BaseEntity
    { 
        #region 表: EP_EquipmentCheckResult 实体类: EP_EquipmentCheckResult 
 
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
        /// 点检任务编码
        /// </summary>
        public string CheckTaskId {get; set; } = "";
 
        /// <summary>
        /// 点检项目编码
        /// </summary>
        public string CheckItemId {get; set; } = "";

        /// <summary>
        /// 点检项目编码
        /// </summary>
        [NotMapped]
        public string CheckItemName {get; set; } = "";
 
        /// <summary>
        /// 点检项目标准
        /// </summary>
        public string CheckItemStandard {get; set; } = "";
 
        /// <summary>
        /// 点检结果
        /// </summary>
        public string CheckResult {get; set; } = "";

        /// <summary>
        /// 点检结果
        /// </summary>
        [NotMapped]
        public string CheckResultValue {get; set; } = "";

        /// <summary>
        /// 是否有效
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
