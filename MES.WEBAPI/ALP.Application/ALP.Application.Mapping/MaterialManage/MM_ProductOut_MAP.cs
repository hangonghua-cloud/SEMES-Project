using ALP.Application.Entity.MaterialManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-10-11
    /// 2.创建作者: admin
    /// 3.功能描述: MM_ProductOut_MAP
    /// 4.任务编号: 成品入库记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_ProductOut_MAP : EntityTypeConfiguration<MM_ProductOutEntity>
    { 
        public MM_ProductOut_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("MM_ProductOut");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
