using ALP.Application.Entity.MaterialManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-29
    /// 2.创建作者: liyongguo
    /// 3.功能描述: MM_SaleDomesticInMap数据映射类
    /// 4.任务编号: 内销成品入库记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_SaleDomesticIn_MAP : EntityTypeConfiguration<MM_SaleDomesticInEntity>
    { 
        public MM_SaleDomesticIn_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("MM_SaleDomesticIn");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
