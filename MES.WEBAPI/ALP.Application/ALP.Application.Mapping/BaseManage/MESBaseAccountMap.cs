using ALP.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace ALP.Application.Mapping.BaseManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-09-19 11:53
    /// 描 述：MESBaseAccount
    /// </summary>
    public class MESBaseAccountMap : EntityTypeConfiguration<MESBaseAccountEntity>
    {
        public MESBaseAccountMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_Base_Account");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
