using ALP.Application.Entity.SAP;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-12-05
    /// 2.创建作者: jpf
    /// 3.功能描述: MM_WarehouseSafetyStockMap数据映射类
    /// 4.任务编号: 仓库安全库存
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_WarehouseSafetyStock_MAP : EntityTypeConfiguration<MM_WarehouseSafetyStockEntity>
    { 
        public MM_WarehouseSafetyStock_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("MM_WarehouseSafetyStock");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
