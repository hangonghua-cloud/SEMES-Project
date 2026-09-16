using ALP.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace ALP.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型资源扩展
    /// </summary>
    public class BsModelResourceExtendInfoMap : EntityTypeConfiguration<BsModelResourceExtendInfoEntity>
    {
        public BsModelResourceExtendInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("BS_ModelResourceExtendInfo");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
