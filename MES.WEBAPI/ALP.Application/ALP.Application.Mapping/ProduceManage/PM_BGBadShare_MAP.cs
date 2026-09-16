using ALP.Application.Entity.ProduceManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Mapping.ProduceManage
{
    public class PM_BGBadShare_MAP : EntityTypeConfiguration<PM_BGBadShareEntity>
    {
        public PM_BGBadShare_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("PM_BGBadShare");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系

            #endregion
        }
    }
}
