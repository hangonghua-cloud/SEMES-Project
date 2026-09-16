using ALP.Application.Entity.Calendar;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Mapping.Calendar
{
    class BS_ShiftManageMap : EntityTypeConfiguration<BS_ShiftManage>
    {
        public BS_ShiftManageMap()
        {
            #region 表、主键
            //表
            this.ToTable("BS_ShiftManage");
            //主键
            this.HasKey(t => t.ShifCode);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
