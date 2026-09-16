using ALP.Application.Entity.EquipManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.EquipManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-12
    /// 2.创建作者: liyongguo
    /// 3.功能描述: EP_EquipmentManageItemMap数据映射类
    /// 4.任务编号: 设备台账
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentManageItem_MAP : EntityTypeConfiguration<EP_EquipmentManageItemEntity>
    { 
        public EP_EquipmentManageItem_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("EP_EquipmentManageItem");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
