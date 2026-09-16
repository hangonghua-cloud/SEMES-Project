using ALP.Application.Entity.MaterialManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-09-01
    /// 2.创建作者: admin
    /// 3.功能描述: MM_RawMaterialOutMap数据映射类
    /// 4.任务编号: 原材料出库记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_RawMaterialOut_MAP : EntityTypeConfiguration<MM_RawMaterialOutEntity>
    { 
        public MM_RawMaterialOut_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("MM_RawMaterialOut");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
