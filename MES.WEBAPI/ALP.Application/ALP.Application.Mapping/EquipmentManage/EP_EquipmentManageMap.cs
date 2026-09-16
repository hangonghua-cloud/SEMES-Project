using ALP.Application.Entity.EquipmentManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Mapping.EquipmentManage
{
    public class EP_EquipmentManageMap : EntityTypeConfiguration<EP_EquipmentManage>
    {
        public EP_EquipmentManageMap()
        {
            #region 表、主键
            //表
            this.ToTable("EP_EquipmentManage");
            //主键
            this.HasKey(t => t.ID);
            #endregion
        }
    }
}
