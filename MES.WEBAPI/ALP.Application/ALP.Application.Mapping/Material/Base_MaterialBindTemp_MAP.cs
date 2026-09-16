using ALP.Application.Entity.Material;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-21
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialBindTempMap数据映射类
    /// 4.任务编号: 任务名称或编号
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_MaterialBindTemp_MAP : EntityTypeConfiguration<Base_MaterialBindTempEntity>
    { 
        public Base_MaterialBindTemp_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("Base_MaterialBindTemp");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
