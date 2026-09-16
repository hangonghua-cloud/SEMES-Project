using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-09
    /// 2.创建作者: liyongguo
    /// 3.功能描述: QC_OQCCheckConfigItem实体
    /// 4.任务编号: OQC检验配置
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_OQCCheckConfigItemEntity : BaseEntity
    { 
        #region 表: QC_OQCCheckConfigItem 实体类: QC_OQCCheckConfigItem 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// OQCId
        /// </summary>
        public string OQCCheckConfigId {get; set; } = "";
 
        /// <summary>
        /// 检测项目编码
        /// </summary>
        public string TestItemCoading {get; set; } = "";
 
        /// <summary>
        /// 检测项目名称
        /// </summary>
        public string TestItemName {get; set; } = "";
 
        /// <summary>
        /// 合格指标
        /// </summary>
        public string TestItemStandard {get; set; } = "";
 
        /// <summary>
        /// 合格上限
        /// </summary>
        public decimal? UpperLimit {get; set; }
 
        /// <summary>
        /// 合格下限
        /// </summary>
        public decimal? LowerLimit {get; set; }
 
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType {get; set; } = "";
 
        /// <summary>
        /// 数据类型名称
        /// </summary>
        public string DataTypeName {get; set; } = "";
 
        /// <summary>
        /// 检测部门
        /// </summary>
        public string TestDepartment {get; set; } = "";
 
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
