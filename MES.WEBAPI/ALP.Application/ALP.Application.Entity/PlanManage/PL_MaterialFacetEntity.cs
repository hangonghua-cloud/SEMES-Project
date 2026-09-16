using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.PlanManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-05
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_MaterialFacet实体
    /// 4.任务编号: 工单发料表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Serializable]
    public class PL_MaterialFacetEntity : BaseEntity
    { 
        #region 表: PL_MaterialFacet 实体类: PL_MaterialFacet 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 物料主键
        /// </summary>
        public string MaterialId {get; set; } = "";
 
        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttrCode {get; set; } = "";
 
        /// <summary>
        /// 属性类型
        /// </summary>
        public string AttrType {get; set; } = "";
 
        /// <summary>
        /// 属性值
        /// </summary>
        public string AttrValue {get; set; } = "";
 
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
