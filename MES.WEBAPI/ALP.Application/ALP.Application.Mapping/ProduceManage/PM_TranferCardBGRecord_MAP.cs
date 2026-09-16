using ALP.Application.Entity.ProduceManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-18
    /// 2.创建作者: admin
    /// 3.功能描述: PM_TranferCardBGRecordMap数据映射类
    /// 4.任务编号: 报工信息
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_TranferCardBGRecord_MAP : EntityTypeConfiguration<PM_TranferCardBGRecordEntity>
    { 
        public PM_TranferCardBGRecord_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("PM_TranferCardBGRecord");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
