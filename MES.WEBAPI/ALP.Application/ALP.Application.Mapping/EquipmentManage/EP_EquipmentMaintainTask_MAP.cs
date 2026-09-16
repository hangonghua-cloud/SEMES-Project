using ALP.Application.Entity.EquipmentManage;
using System.Data.Entity.ModelConfiguration;
 
namespace ALP.Application.Mapping.EquipmentManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-05
    /// 2.创建作者: 王坤
    /// 3.功能描述: EP_EquipmentMaintainTaskMap数据映射类
    /// 4.任务编号: 设备保养项目详情
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentMaintainTask_MAP : EntityTypeConfiguration<EP_EquipmentMaintainTaskEntity>
    { 
        public EP_EquipmentMaintainTask_MAP()
        {
            #region 表、主键
            //表
            this.ToTable("EP_EquipmentMaintainTask");
            //主键
            this.HasKey(t => t.Id);
            #endregion
            
            #region 配置关系
            
            #endregion
        }
    }
}
