using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-11-23
    /// 2.创建作者: jpf
    /// 3.功能描述: BS_TraitDetailsAttr实体
    /// 4.任务编号: 跨工厂调拨
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_TraitDetailsAttrEntity : BaseEntity
    { 
        #region 表: BS_TraitDetailsAttr 实体类: BS_TraitDetailsAttr 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// TraitDetailId
        /// </summary>
        public string TraitDetailId {get; set; } = "";
 
        /// <summary>
        /// AttrCode
        /// </summary>
        public string AttrCode {get; set; } = "";
 
        /// <summary>
        /// AttrName
        /// </summary>
        public string AttrName {get; set; } = "";
 
        /// <summary>
        /// AttrType
        /// </summary>
        public string AttrType {get; set; } = "";
 
        /// <summary>
        /// AttrValue
        /// </summary>
        public string AttrValue {get; set; } = "";
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; } = "";

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorName { get; set; } = "";

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy { get; set; } = "";

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        public string ModifyByName { get; set; } = "";
        /// <summary>
        /// 特征值便于接受前台数据
        /// </summary>
        [NotMapped]
        public string TraitValue { get; set; } = "";

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
