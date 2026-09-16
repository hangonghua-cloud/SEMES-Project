using ALP.Application.Entity.BaseManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Mapping.BaseManage
{
  public  class F_FilesMap : EntityTypeConfiguration<F_FilesEntity>
    {
        public F_FilesMap()
        {
            #region 表、主键
            //表
            this.ToTable("F_Files");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
