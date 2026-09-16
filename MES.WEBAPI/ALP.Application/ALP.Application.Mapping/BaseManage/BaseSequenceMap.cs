using ALP.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;
namespace ALP.Application.Mapping.BaseManage
{
    /// <summary>
    /// [Base_Sequence]数据映射类
    /// 描述:流水号管理
    /// 作者:liyonguo
    /// 创建时间:2021-12-29 14:48:28
    /// </summary>
    public class BaseSequenceMap : EntityTypeConfiguration<BaseSequenceEntity>
    {
        public BaseSequenceMap()
        {

            #region 表、主键
            //表
            this.ToTable("Base_Sequence");
            //主键
            this.HasKey(t => t.SeqCode);
            #endregion
            #region 配置关系
            #endregion
        }
    }
}

