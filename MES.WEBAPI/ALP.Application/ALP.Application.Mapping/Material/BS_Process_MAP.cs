using ALP.Application.Entity.Material;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: BS_ProcessMap数据映射类
    /// 4.任务编号: 供应商管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_Process_MAP : EntityTypeConfiguration<BS_ProcessEntity>
    { 
        public BS_Process_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("BS_Process");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
