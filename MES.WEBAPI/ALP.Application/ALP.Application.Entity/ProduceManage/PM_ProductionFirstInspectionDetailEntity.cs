using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-11-09
    /// 2.创建作者: huxiao
    /// 3.功能描述: PM_ProductionFirstInspectionDetail实体
    /// 4.任务编号: 任务编号或模块名称
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_ProductionFirstInspectionDetailEntity : BaseEntity
    {
        #region 表: PM_ProductionFirstInspectionDetail 实体类: PM_ProductionFirstInspectionDetail 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; } = "";

        /// <summary>
        /// 首检ID
        /// </summary>
        public string FirstInspectionId { get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 检测项目id
        /// </summary>
        public string TestItemId { get; set; } = "";

        /// <summary>
        /// 首检项目编码
        /// </summary>
        public string TestItemCoading { get; set; } = "";

        /// <summary>
        /// 首检项目名称
        /// </summary>
        public string TestItemName { get; set; } = "";

        /// <summary>
        /// 检测类型
        /// </summary>
        public string DataType { get; set; } = "";

        /// <summary>
        /// 检测类型名称
        /// </summary>
        public string DataTypeName { get; set; } = "";

        /// <summary>
        /// 首检标准
        /// </summary>
        public string TestItemStandard { get; set; } = "";

        /// <summary>
        /// 车间结果
        /// </summary>
        public string WorkShopResult { get; set; } = "";

        /// <summary>
        /// 质量结果
        /// </summary>
        public string QualityResult { get; set; } = "";

        /// <summary>
        /// InspectClass
        /// </summary>
        public string InspectClass { get; set; } = "";

        /// <summary>
        /// Creator
        /// </summary>
        public string Creator { get; set; } = "";

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// TestDepartment
        /// </summary>
        public string TestDepartment { get; set; } = "";

        /// <summary>
        /// TestItemResult
        /// </summary>
        public string TestItemResult { get; set; } = "";

      

        /// <summary>
        /// EnabledMark
        /// </summary>
        public bool EnabledMark { get; set; }

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
