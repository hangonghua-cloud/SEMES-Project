using ALP.Application.Entity.MaterialManage;
using System.Data.Entity.ModelConfiguration;
namespace ALP.Application.Mapping.MaterialManage
{
    /// <summary>
    /// [Base_ProductOrderFileCon]数据映射类
    /// 描述:Base_按订单归档
    /// 作者:Dragon
    /// 创建时间:2023-05-18 10:21:29
    /// </summary>
    public class BaseProductOrderFileConMap : EntityTypeConfiguration<BaseProductOrderFileConEntity>
    {
        public BaseProductOrderFileConMap()
        {
        
        #region 表、主键
        //表
        this.ToTable("Base_ProductOrderFileCon");
        //主键
        this.HasKey(t => t.Id);
        #endregion
        #region 配置关系
        #endregion
    }
    }
}

