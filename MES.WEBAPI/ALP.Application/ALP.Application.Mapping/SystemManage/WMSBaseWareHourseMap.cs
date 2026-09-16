using ALP.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace ALP.Application.Mapping.SystemManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-09-02 10:39
    /// 描 述：仓库表 别名：WH
    /// </summary>
    public class WMSBaseWareHourseMap : EntityTypeConfiguration<WMSBaseWareHourseEntity>
    {
        public WMSBaseWareHourseMap()
        {
            #region 表、主键
            //表
            this.ToTable("WMS_Base_WareHourse");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
