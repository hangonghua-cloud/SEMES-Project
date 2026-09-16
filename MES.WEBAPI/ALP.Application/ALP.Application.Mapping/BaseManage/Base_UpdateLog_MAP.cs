using ALP.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.BaseManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-11-27
    /// 2.创建作者: admin
    /// 3.功能描述: Base_UpdateLogMap数据映射类
    /// 4.任务编号: 升级日志
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_UpdateLog_MAP : EntityTypeConfiguration<Base_UpdateLogEntity>
    { 
        public Base_UpdateLog_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("Base_UpdateLog");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
