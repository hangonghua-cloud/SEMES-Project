using ALP.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace ALP.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.1.4 16:19
    /// 描 述：物料主数据
    /// </summary>
    public class MaterialManageMap : EntityTypeConfiguration<MaterialManageEntity>
    {
        public MaterialManageMap()
        {
            #region 表、主键
            //表
            this.ToTable("BS_MaterialManage");
            //主键
            this.HasKey(t => t.MaterialCode);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
