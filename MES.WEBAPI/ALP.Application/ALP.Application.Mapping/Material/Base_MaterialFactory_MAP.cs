using ALP.Application.Entity.Material;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-30
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialFactoryMap数据映射类
    /// 4.任务编号: 工厂物料数据维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_MaterialFactory_MAP : EntityTypeConfiguration<Base_MaterialFactoryEntity>
    { 
        public Base_MaterialFactory_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("Base_MaterialFactory");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
