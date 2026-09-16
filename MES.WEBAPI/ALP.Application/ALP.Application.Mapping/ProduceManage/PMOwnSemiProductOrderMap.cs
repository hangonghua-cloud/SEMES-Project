using ALP.Application.Entity.ProduceManage;
using System.Data.Entity.ModelConfiguration;
namespace ALP.Application.Mapping.ProduceManage
{
    /// <summary>
    /// [PM_OwnSemiProductOrder]数据映射类
    /// 描述:自制半成品订单管理
    /// 作者:Dragon
    /// 创建时间:2022-12-06 10:24:28
    /// </summary>
    public class PMOwnSemiProductOrderMap : EntityTypeConfiguration<PMOwnSemiProductOrderEntity>
    {
       public PMOwnSemiProductOrderMap()
       {
       
         #region 表、主键
         //表
         this.ToTable("PM_OwnSemiProductOrder");
         //主键
         this.HasKey(t => t.Id);
         #endregion
         #region 配置关系
         #endregion
      }
   }
}

