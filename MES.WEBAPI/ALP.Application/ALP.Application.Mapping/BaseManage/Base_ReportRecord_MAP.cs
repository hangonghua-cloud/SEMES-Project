using ALP.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.BaseManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-11-30
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_ReportRecordMap数据映射类
    /// 4.任务编号: Base报表页面
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_ReportRecord_MAP : EntityTypeConfiguration<Base_ReportRecordEntity>
    { 
        public Base_ReportRecord_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("Base_ReportRecord");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
