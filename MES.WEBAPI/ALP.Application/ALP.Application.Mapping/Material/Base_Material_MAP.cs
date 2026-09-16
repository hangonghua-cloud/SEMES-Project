using ALP.Application.Entity.Material;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-23
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialMap数据映射类
    /// 4.任务编号: 物料主数据
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_Material_MAP : EntityTypeConfiguration<Base_MaterialEntity>
    { 
        public Base_Material_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Material");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
