using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.EquipmentManage
{
    /// <summary>
    /// 1.创建日期: 2021-10-26
    /// 2.创建作者: huxiao
    /// 3.功能描述: V_EP_EquipmentManage实体
    /// 4.任务编号: 任务编号或模块名称
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class V_EP_EquipmentManageEntity : BaseEntity
    {
        #region 表: V_EP_EquipmentManage 实体类: V_EP_EquipmentManage 
        public V_EP_EquipmentManageEntity()
        { }

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string TestMethodCoadin { get; set; } = "";
        /// <summary>
        /// TestMethodCoadinName
        /// </summary>
        public string TestMethodCoadinName { get; set; } = "";
        /// <summary>
        /// TestMethodCoadinName
        /// </summary>
        public string InstallationSite { get; set; } = "";

        /// <summary>
        /// InstallationSiteName
        /// </summary>
        public string InstallationSiteName { get; set; } = "";
        /// <summary>
        /// 工序编码
        /// </summary>
        public string ProcessBelong { get; set; }

        /// <summary>
        /// ProcessBelongName
        /// </summary>
        public string ProcessBelongName { get; set; } = "";

        /// <summary>
        /// EquipmentId
        /// </summary>
        public string EquipmentId { get; set; } = "";

        /// <summary>
        /// EquipmentName
        /// </summary>
        public string EquipmentName { get; set; } = "";

        /// <summary>
        /// EquipmentTypeName
        /// </summary>
        public string EquipmentTypeName { get; set; } = "";

        /// <summary>
        /// EquipmentType
        /// </summary>
        public string EquipmentType { get; set; } = "";

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion

        #endregion
    }
}
