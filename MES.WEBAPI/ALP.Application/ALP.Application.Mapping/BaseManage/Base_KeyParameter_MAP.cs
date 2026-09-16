using ALP.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.BaseManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-30
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_KeyParameterMap数据映射类
    /// 4.任务编号: 关键参数维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_KeyParameter_MAP : EntityTypeConfiguration<Base_KeyParameterEntity>
    { 
        public Base_KeyParameter_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("Base_KeyParameter");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
