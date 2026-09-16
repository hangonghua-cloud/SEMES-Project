using ALP.Application.Entity.MaterialManage;
using System.Data.Entity.ModelConfiguration;
namespace ALP.Application.Mapping.MaterialManage
{
    /// <summary>
    /// [MM_SuperProductStockAdjust]数据映射类
    /// 描述:超产品库存校准
    /// 作者:dragon
    /// 创建时间:2022-01-10 19:49:52
    /// </summary>
    public class MMSuperProductStockAdjustMap : EntityTypeConfiguration<MMSuperProductStockAdjustEntity>
    {
       public MMSuperProductStockAdjustMap()
       {
       
         #region 表、主键
         //表
         this.ToTable("MM_SuperProductStockAdjust");
         //主键
         this.HasKey(t => t.Id);
         #endregion
         #region 配置关系
         #endregion
      }
   }
}

