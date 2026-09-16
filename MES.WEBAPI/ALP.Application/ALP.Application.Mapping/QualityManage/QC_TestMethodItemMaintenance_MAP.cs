using ALP.Application.Entity.QualityManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-27
    /// 2.创建作者: 丁零
    /// 3.功能描述: QC_TestMethodItemMaintenanceMap数据映射类
    /// 4.任务编号: IQC检验项目配置表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_TestMethodItemMaintenance_MAP : EntityTypeConfiguration<QC_TestMethodItemMaintenanceEntity>
    { 
        public QC_TestMethodItemMaintenance_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("QC_TestMethodItemMaintenance");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
