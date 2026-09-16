using ALP.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace ALP.Application.Mapping.BaseManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-04 13:32
    /// 描 述：微应用预警配置表
    /// </summary>
    public class WebChatApplyConfigMap : EntityTypeConfiguration<WebChatApplyConfigEntity>
    {
        public WebChatApplyConfigMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_WebChatApplyConfig");
            //主键
            this.HasKey(t => t.ApplyConfigId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
