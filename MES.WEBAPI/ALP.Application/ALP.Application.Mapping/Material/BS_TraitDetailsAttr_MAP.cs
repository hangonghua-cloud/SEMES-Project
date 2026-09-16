using ALP.Application.Entity.SAP;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-11-23
    /// 2.创建作者: jpf
    /// 3.功能描述: BS_TraitDetailsAttrMap数据映射类
    /// 4.任务编号: 跨工厂调拨
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_TraitDetailsAttr_MAP : EntityTypeConfiguration<BS_TraitDetailsAttrEntity>
    { 
        public BS_TraitDetailsAttr_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("BS_TraitDetailsAttr");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
