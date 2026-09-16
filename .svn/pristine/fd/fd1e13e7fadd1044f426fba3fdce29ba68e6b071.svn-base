using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-09-11
    /// 2.创建作者: liyongguo
    /// 3.功能描述: QC_MaterialInventoryCheckItem实体
    /// 4.任务编号: 原材料库存检验
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_MaterialInventoryCheckItemEntity : BaseEntity
    { 
        #region 表: QC_MaterialInventoryCheckItem 实体类: QC_MaterialInventoryCheckItem 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// MaterialInventoryId
        /// </summary>
        public string MaterialInventoryId {get; set; } = "";
 
        /// <summary>
        /// TestItemCoading
        /// </summary>
        public string TestItemCoading {get; set; } = "";
 
        /// <summary>
        /// TestItemName
        /// </summary>
        public string TestItemName {get; set; } = "";
 
        /// <summary>
        /// TestItemStandard
        /// </summary>
        public string TestItemStandard {get; set; } = "";
 
        /// <summary>
        /// TestDepartment
        /// </summary>
        public string TestDepartment {get; set; } = "";
 
        /// <summary>
        /// ItemValue
        /// </summary>
        public string ItemValue {get; set; } = "";
 
        /// <summary>
        /// DataType
        /// </summary>
        public string DataType {get; set; } = "";
 
        /// <summary>
        /// DataTypeName
        /// </summary>
        public string DataTypeName {get; set; } = "";
 
        /// <summary>
        /// BadNum
        /// </summary>
        public string BadNum {get; set; } = "";
 
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
