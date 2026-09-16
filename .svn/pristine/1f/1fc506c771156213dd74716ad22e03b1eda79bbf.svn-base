using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-09-09
    /// 2.创建作者: liyongguo
    /// 3.功能描述: QC_TestMethodMaintenance实体
    /// 4.任务编号: 原料/IQC检测配置表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_TestMethodMaintenanceEntity : BaseEntity
    { 
        #region 表: QC_TestMethodMaintenance 实体类: QC_TestMethodMaintenance 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 工厂
        /// </summary>
        public string FactoryCode {get; set; } = "";
 
        /// <summary>
        /// 检测类型
        /// </summary>
        public string TestType {get; set; } = "";
 
        /// <summary>
        /// 检测方法编码
        /// </summary>
        public string TestMethodCoading {get; set; } = "";
 
        /// <summary>
        /// 检测方法名称
        /// </summary>
        public string TestMethodName {get; set; } = "";
 
        /// <summary>
        /// 检测方法描述
        /// </summary>
        public string TestMethodDescription {get; set; } = "";
 
        /// <summary>
        /// 关联物料小类
        /// </summary>
        public string MaterialGroup {get; set; } = "";
 
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled {get; set; }
 
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
