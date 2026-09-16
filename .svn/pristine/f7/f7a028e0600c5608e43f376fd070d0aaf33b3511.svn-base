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
    /// 3.功能描述: EP_EquipmentCheckItemDetail实体
    /// 4.任务编号: 设备点检详情
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentCheckItemDetailEntity : BaseEntity
    { 
        #region 表: EP_EquipmentCheckItemDetail 实体类: EP_EquipmentCheckItemDetail 
 
        /// <summary>
        /// 主键
        /// </summary>
        public string Id {get; set; } = "";
        
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
        public string CheckItemName {get; set; } = "";
 
        /// <summary>
        /// 点检项目名称
        /// </summary>
        public string CheckItemStandard {get; set; } = "";
 
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType {get; set; } = "";

        /// <summary>
        /// 数据类型名称
        /// </summary>
        public string DataTypeName {get; set; } = "";
 
        /// <summary>
        /// 数据类型
        /// </summary>
        public bool EnabledMark {get; set; } = true;

        /// <summary>
        /// 排序字段
        /// </summary>
        public int SortCode { get; set; } = 0;
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 修改检
        /// </summary>
        public string ModifyBy {get; set; } = "";
 
        /// <summary>
        /// 修改时间
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
