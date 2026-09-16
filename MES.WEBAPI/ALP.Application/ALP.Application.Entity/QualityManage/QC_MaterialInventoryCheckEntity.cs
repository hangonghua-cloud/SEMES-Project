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
    /// 3.功能描述: QC_MaterialInventoryCheck实体
    /// 4.任务编号: 原材料库存检验
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_MaterialInventoryCheckEntity : BaseEntity
    { 
        #region 表: QC_MaterialInventoryCheck 实体类: QC_MaterialInventoryCheck 
 
        /// <summary>
        /// 主键
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
        /// 检验方法Id
        /// </summary>
        public string TestMethodId {get; set; } = "";
 
        /// <summary>
        /// 检验单号
        /// </summary>
        public string InspectNo {get; set; } = "";
 
        /// <summary>
        /// 检测部门
        /// </summary>
        public string TestDepartment {get; set; } = "";
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 物料小类
        /// </summary>
        public string SmallClass { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string BatchNo {get; set; } = "";
        /// <summary>
        /// 库存仓库
        /// </summary>
        public string WhsCode { get; set; }

        /// <summary>
        /// 判定结果
        /// </summary>
        public string TestResult {get; set; } = "";
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } = "";
 
        /// <summary>
        /// 有效标志
        /// </summary>
        public bool IsEnabled {get; set; }
 
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment {get; set; } = "";
 
        /// <summary>
        /// 附件地址
        /// </summary>
        public string Address {get; set; } = "";
 
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
