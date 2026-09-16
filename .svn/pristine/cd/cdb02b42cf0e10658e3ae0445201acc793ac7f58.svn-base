using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-11-02
    /// 2.创建作者: jpf
    /// 3.功能描述: BS_TraitDetails实体
    /// 4.任务编号: 特征维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_TraitDetailsEntity : BaseEntity
    { 
        #region 表: BS_TraitDetails 实体类: BS_TraitDetails 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 主表关联ID
        /// </summary>
        public string TraitManageID {get; set; } = "";
 
        /// <summary>
        /// 特征值
        /// </summary>
        public string TraitValue {get; set; } = "";
        /// <summary>
        /// 属性模板名称
        /// </summary>
        public string AttrModleCode { get; set; } = "";

        /// <summary>
        /// 删除标识默认为0,删除1
        /// </summary>
        public bool? IsDeleted {get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateName {get; set; } = "";
 
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
        public DateTime?  ModifyTime {get; set; }
 
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string ModifyName {get; set; } = "";


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
