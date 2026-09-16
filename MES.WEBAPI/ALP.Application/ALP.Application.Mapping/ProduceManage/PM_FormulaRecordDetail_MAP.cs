using ALP.Application.Entity.ProduceManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-24
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_FormulaRecordDetailMap数据映射类
    /// 4.任务编号: 小料喂料记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_FormulaRecordDetail_MAP : EntityTypeConfiguration<PM_FormulaRecordDetailEntity>
    { 
        public PM_FormulaRecordDetail_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("PM_FormulaRecordDetail");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
