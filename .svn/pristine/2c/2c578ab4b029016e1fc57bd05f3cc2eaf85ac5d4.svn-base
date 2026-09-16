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
    /// 3.功能描述: Base_ProcessAttr实体
    /// 4.任务编号: 工单发料表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_ProcessAttrEntity : BaseEntity
    { 
        #region 表: Base_ProcessAttr 实体类: Base_ProcessAttr 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } 
 
        /// <summary>
        /// 编码
        /// </summary>
        public string ItemCode {get; set; } 
 
        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName {get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }

        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string Process { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string ProcessName { get; set; }

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
