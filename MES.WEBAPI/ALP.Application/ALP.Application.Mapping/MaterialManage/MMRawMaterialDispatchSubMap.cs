using ALP.Application.Entity.MaterialManage;
using System.Data.Entity.ModelConfiguration;
namespace ALP.Application.Mapping.MaterialManage
{
    /// <summary>
    /// [MM_RawMaterialDispatchSub]数据映射类
    /// 描述:MM_原材料半成品发货单子表
    /// 作者:Dragon
    /// 创建时间:2024-03-13 09:27:07
    /// </summary>
    public class MMRawMaterialDispatchSubMap : EntityTypeConfiguration<MMRawMaterialDispatchSubEntity>
    {
       public MMRawMaterialDispatchSubMap()
       {
       
         #region 表、主键
         //表
         this.ToTable("MM_RawMaterialDispatchSub");
         //主键
         this.HasKey(t => t.Id);
         #endregion
         #region 配置关系
         #endregion
      }
   }
}

