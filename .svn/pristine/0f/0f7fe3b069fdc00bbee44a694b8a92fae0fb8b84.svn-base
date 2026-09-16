using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.BaseManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-11-30
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_ReportRecord实体
    /// 4.任务编号: Base报表页面
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_ReportRecordEntity : BaseEntity
    { 
        #region 表: Base_ReportRecord 实体类: Base_ReportRecord 
 
        /// <summary>
        /// Guid
        /// </summary>
        public string Id {get; set; } 
 
        /// <summary>
        /// 父级
        /// </summary>
        public string ParentId {get; set; } 
 
        /// <summary>
        /// 地址
        /// </summary>
        public string Address {get; set; } 
 
        /// <summary>
        /// 名称
        /// </summary>
        public string ReportName {get; set; } 
 
        /// <summary>
        /// 排序
        /// </summary>
        public int SortCode {get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } 
 
        /// <summary>
        /// 显示0  隐藏 1
        /// </summary>
        public string EnabledMark {get; set; } 
 
        /// <summary>
        /// 菜单0  地址1
        /// </summary>
        public int? Superior {get; set; } 
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } 
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } 
 
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
