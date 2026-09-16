using ALP.Application.Entity.QualityManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-09-10
    /// 2.创建作者: liyongguo
    /// 3.功能描述: QC_IQCQualityCheckMap数据映射类
    /// 4.任务编号: IQC检测记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_IQCQualityCheck_MAP : EntityTypeConfiguration<QC_IQCQualityCheckEntity>
    { 
        public QC_IQCQualityCheck_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("QC_IQCQualityCheck");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
