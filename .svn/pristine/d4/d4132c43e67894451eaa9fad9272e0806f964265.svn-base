using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-24
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_FormulaRecordDetail实体
    /// 4.任务编号: 小料喂料记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_FormulaRecordDetailEntity : BaseEntity
    {
        #region 表: PM_FormulaRecordDetail 实体类: PM_FormulaRecordDetail 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; } = "";

        /// <summary>
        /// 报工ID
        /// </summary>
        public string FormulaId { get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = "";

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; } = "";

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = "";
        /// <summary>
        /// 单耗
        /// </summary>
        public decimal? UnitConsome { get; set; }

        /// <summary>
        /// 实际消耗
        /// </summary>
        public decimal? ActQty {get; set; }
 
        /// <summary>
        /// 理论消耗
        /// </summary>
        public decimal? TheoryQty {get; set; }
 
        /// <summary>
        /// 消耗批次
        /// </summary>
        public string BatchNo {get; set; } = "";

        [NotMapped]
        public string SmallClass { get; set; }
        [NotMapped]
        public string Spec { get; set; }
        [NotMapped]
        public string WhsCode { get; set; }

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
