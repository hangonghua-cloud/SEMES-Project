using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.BaseManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-30
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_KeyParameterItem实体
    /// 4.任务编号: 关键参数维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_KeyParameterItemEntity : BaseEntity
    { 
        #region 表: Base_KeyParameterItem 实体类: Base_KeyParameterItem 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
       
        /// <summary>
        /// EnCode
        /// </summary>
        public string EnCode {get; set; } = "";
 
        /// <summary>
        /// 编码
        /// </summary>
        public string ItemCode {get; set; } = "";
 
        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName {get; set; } = "";
 
        /// <summary>
        /// 值
        /// </summary>
        public string ItemValue {get; set; } = "";
 
        /// <summary>
        /// 字段1
        /// </summary>
        public string Col1 {get; set; } = "";
 
        /// <summary>
        /// 字段2
        /// </summary>
        public string Col2 {get; set; } = "";
 
        /// <summary>
        /// 字段3
        /// </summary>
        public string Col3 {get; set; } = "";
 
        /// <summary>
        /// 备注1
        /// </summary>
        public string Remark1 {get; set; } = "";
 
        /// <summary>
        /// 备注2
        /// </summary>
        public string Remark2 {get; set; } = "";
 
        /// <summary>
        /// 排序
        /// </summary>
        public int? SortCode {get; set; }
 
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEnabled {get; set; }
 
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
