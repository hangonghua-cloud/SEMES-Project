using ALP.Application.Entity.ProduceManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-11
    /// 2.创建作者: admin
    /// 3.功能描述: PM_TransferCardMap数据映射类
    /// 4.任务编号: 流转卡信息
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_TransferCard_MAP : EntityTypeConfiguration<PM_TransferCardEntity>
    { 
        public PM_TransferCard_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("PM_TransferCard");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
