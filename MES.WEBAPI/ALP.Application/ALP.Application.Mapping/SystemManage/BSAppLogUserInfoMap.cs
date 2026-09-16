using ALP.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace ALP.Application.Mapping.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.3.13 16:19
    /// 描 述：用户信息与登录
    /// </summary>
    public class BSAppLogUserInfoMap : EntityTypeConfiguration<BSAppLogUserInfoEntity>
    {
        public BSAppLogUserInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("BS_APPLogUserInfo");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
