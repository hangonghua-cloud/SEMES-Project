using ALP.Application.Entity.MaterialManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-29
    /// 2.创建作者: liyongguo
    /// 3.功能描述: MM_SaleDomesticOutDetailMap数据映射类
    /// 4.任务编号: 内销发货单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_SaleDomesticOutDetail_MAP : EntityTypeConfiguration<MM_SaleDomesticOutDetailEntity>
    { 
        public MM_SaleDomesticOutDetail_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("MM_SaleDomesticOutDetail");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
