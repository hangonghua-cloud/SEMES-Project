using ALP.Application.Entity.MaterialManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-09-08
    /// 2.创建作者: admin
    /// 3.功能描述: MM_SupProductStockTransferMap数据映射类
    /// 4.任务编号: 超产品库存流转记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_SupProductStockTransfer_MAP : EntityTypeConfiguration<MM_SupProductStockTransferEntity>
    { 
        public MM_SupProductStockTransfer_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("MM_SupProductStockTransfer");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
