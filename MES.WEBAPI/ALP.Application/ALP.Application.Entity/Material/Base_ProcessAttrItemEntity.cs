using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-09
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_ProcessAttrItem实体
    /// 4.任务编号: 工单发料表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_ProcessAttrItemEntity : BaseEntity
    { 
        #region 表: Base_ProcessAttrItem 实体类: Base_ProcessAttrItem 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } 
 
        /// <summary>
        /// 外键
        /// </summary>
        public string ProcessAttrId {get; set; } 
 
        /// <summary>
        /// 属性编码
        /// </summary>
        public string AttrCode {get; set; } 
 
        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttrName {get; set; } 
 
        /// <summary>
        /// 属性类型
        /// </summary>
        public string AttrType {get; set; } 
        /// <summary>
        /// 属性类型名称
        /// </summary>
        public string AttrTypeName { get; set; }
        /// <summary>
        /// 顺序号
        /// </summary>
        public int? SortCode { get; set; }

        /// <summary>
        /// 上限
        /// </summary>
        public decimal? UpperLimit {get; set; }
 
        /// <summary>
        /// 下限
        /// </summary>
        public decimal? LowerLimit {get; set; }
 
        /// <summary>
        /// 长度
        /// </summary>
        public int Length {get; set; }
 
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled {get; set; }
 
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
