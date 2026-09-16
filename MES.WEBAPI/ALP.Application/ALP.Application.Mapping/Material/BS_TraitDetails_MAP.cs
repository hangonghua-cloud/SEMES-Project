using ALP.Application.Entity.SAP;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-11-02
    /// 2.创建作者: jpf
    /// 3.功能描述: BS_TraitDetailsMap数据映射类
    /// 4.任务编号: 特征维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_TraitDetails_MAP : EntityTypeConfiguration<BS_TraitDetailsEntity>
    { 
        public BS_TraitDetails_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("BS_TraitDetails");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
