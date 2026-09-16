using ALP.Application.Entity.ProduceManage;
using System.Data.Entity.ModelConfiguration;
namespace ALP.Application.Mapping.ProduceManage
{
    /// <summary>
    /// [PM_ProductPrice]数据映射类
    /// 描述:产品工价维护
    /// 作者:Dragon
    /// 创建时间:2022-11-08 14:54:29
    /// </summary>
    public class PMProductPriceMap : EntityTypeConfiguration<PMProductPriceEntity>
    {
       public PMProductPriceMap()
       {
       
         #region 表、主键
         //表
         this.ToTable("PM_ProductPrice");
         //主键
         this.HasKey(t => t.Id);
         #endregion
         #region 配置关系
         #endregion
      }
   }
}

