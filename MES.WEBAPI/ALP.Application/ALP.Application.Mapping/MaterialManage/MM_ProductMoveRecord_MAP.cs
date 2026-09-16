using ALP.Application.Entity.MaterialManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-14
    /// 2.创建作者: admin
    /// 3.功能描述: MM_ProductMoveRecordMap数据映射类
    /// 4.任务编号: 成品移库记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_ProductMoveRecord_MAP : EntityTypeConfiguration<MM_ProductMoveRecordEntity>
    { 
        public MM_ProductMoveRecord_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("MM_ProductMoveRecord");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
