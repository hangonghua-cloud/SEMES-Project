using ALP.Application.Entity.SAP;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-12-03
    /// 2.创建作者: jpf
    /// 3.功能描述: PL_MarkUploadMap数据映射类
    /// 4.任务编号: 唛头配置
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_MarkUpload_MAP : EntityTypeConfiguration<PL_MarkUploadEntity>
    { 
        public PL_MarkUpload_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("PL_MarkUpload");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
