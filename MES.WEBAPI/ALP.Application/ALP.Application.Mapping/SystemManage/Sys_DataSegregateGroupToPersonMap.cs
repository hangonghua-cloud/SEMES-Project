using ALP.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace ALP.Application.Mapping.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.2.22 16:19
    /// 描 述：
    /// </summary>
    public class Sys_DataSegregateGroupToPersonMap : EntityTypeConfiguration<Sys_DataSegregateGroupToPerson>
    {
        public Sys_DataSegregateGroupToPersonMap()
        {
            #region 表、主键
            //表
            this.ToTable("Sys_DataSegregateGroupToPerson");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
