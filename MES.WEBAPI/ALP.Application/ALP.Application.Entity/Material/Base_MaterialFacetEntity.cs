using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-23
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialFacet实体
    /// 4.任务编号: 物料主数据
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_MaterialFacetEntity : BaseEntity
    { 
        #region 表: Base_MaterialFacet 实体类: Base_MaterialFacet 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
        /// <summary>
        /// 物料编码
        /// </summary>
        [NotMapped]
        public string MaterialCode { get; set; } = "";

        ///// <summary>
        ///// 物料主键
        ///// </summary>
        //public string MaterialId {get; set; } = "";
        /// <summary>
        /// 物料工厂Id
        /// </summary>
        public string MaterialFactoryId { get; set; }
        /// <summary>
        /// 属性编码
        /// </summary>
      
        public string AttrCode { get; set; } = "";

        /// <summary>
        /// 属性名称
        /// </summary>
       
        public string AttrName {get; set; } = "";

        /// <summary>
        /// 属性类型
        /// </summary>
       
        public string AttrType {get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        
        public string AttrValue {get; set; } = "";
        /// <summary>
        /// 模板分类
        /// </summary>
        public string MateriaBindTempId { get; set; }
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
