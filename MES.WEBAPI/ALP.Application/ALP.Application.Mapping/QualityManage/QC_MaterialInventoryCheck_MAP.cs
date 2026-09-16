using ALP.Application.Entity.QualityManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-09-11
    /// 2.创建作者: liyongguo
    /// 3.功能描述: QC_MaterialInventoryCheckMap数据映射类
    /// 4.任务编号: 原材料库存检验
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_MaterialInventoryCheck_MAP : EntityTypeConfiguration<QC_MaterialInventoryCheckEntity>
    { 
        public QC_MaterialInventoryCheck_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("QC_MaterialInventoryCheck");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
