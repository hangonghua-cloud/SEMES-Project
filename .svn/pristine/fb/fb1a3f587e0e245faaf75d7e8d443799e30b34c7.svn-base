using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-18
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_ProcessOfOperationsAttr实体
    /// 4.任务编号: 订单采购
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Serializable]
    public class PL_ProcessOfOperationsAttrEntity : BaseEntity
    { 
        #region 表: PL_ProcessOfOperationsAttr 实体类: PL_ProcessOfOperationsAttr 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
        /// <summary>
        /// 祖父键
        /// </summary>
        public string ProcessId { get; set; } = "";

        /// <summary>
        /// 外键
        /// </summary>
        public string OperationsId {get; set; } = "";
 
        /// <summary>
        /// 属性编码
        /// </summary>
        public string AttrCode {get; set; } = "";
 
        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttrName {get; set; } = "";
 
        /// <summary>
        /// 属性类型
        /// </summary>
        public string AttrType {get; set; } = "";
 
        /// <summary>
        /// 属性类型名称
        /// </summary>
        public string AttrTypeName {get; set; } = "";
 
        /// <summary>
        /// 顺序号
        /// </summary>
        public int? SortCode {get; set; }
 
        /// <summary>
        /// 录入值
        /// </summary>
        public string AttrValue {get; set; } = "";
 
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool? IsEnabled {get; set; }
 
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
