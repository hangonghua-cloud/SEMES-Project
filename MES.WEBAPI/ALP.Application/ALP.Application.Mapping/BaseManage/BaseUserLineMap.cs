using ALP.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace ALP.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.12.21 16:19
    /// 描 述：编号规则
    /// </summary>
    public class BaseUserLineMap : EntityTypeConfiguration<BaseUserLineEntity>
    {
        public BaseUserLineMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_UserLineRelation");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
