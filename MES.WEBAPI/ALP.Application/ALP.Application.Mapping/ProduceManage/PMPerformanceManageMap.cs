using ALP.Application.Entity.ProduceManage;
using System.Data.Entity.ModelConfiguration;
namespace ALP.Application.Mapping.ProduceManage
{
    /// <summary>
    /// [PM_PerformanceManage]数据映射类
    /// 描述:绩效管理
    /// 作者:Dragon
    /// 创建时间:2022-11-16 13:57:13
    /// </summary>
    public class PMPerformanceManageMap : EntityTypeConfiguration<PMPerformanceManageEntity>
    {
       public PMPerformanceManageMap()
       {
       
         #region 表、主键
         //表
         this.ToTable("PM_PerformanceManage");
         //主键
         this.HasKey(t => t.Id);
         #endregion
         #region 配置关系
         #endregion
      }
   }
}

