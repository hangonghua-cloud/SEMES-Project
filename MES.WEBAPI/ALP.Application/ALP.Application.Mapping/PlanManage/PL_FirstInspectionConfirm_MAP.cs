using ALP.Application.Entity.PlanManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.PlanManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_FirstInspectionConfirm_MAP : EntityTypeConfiguration<PL_FirstInspectionConfirmEntity>
    { 
        public PL_FirstInspectionConfirm_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("PL_FirstInspectionConfirm");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
