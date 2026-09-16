using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-21
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialBindTemp实体
    /// 4.任务编号: 任务名称或编号
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_MaterialBindTempEntity : BaseEntity
    { 
        #region 表: Base_MaterialBindTemp 实体类: Base_MaterialBindTemp 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 模板编码
        /// </summary>
        public string TempCode {get; set; } = "";
 
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TempName {get; set; } = "";
 
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort {get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } = "";
 
        /// <summary>
        /// 是否生效1是0否
        /// </summary>
        public bool IsEnabled {get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } = "";
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTimeOffset? ModifyTime {get; set; }
 
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
