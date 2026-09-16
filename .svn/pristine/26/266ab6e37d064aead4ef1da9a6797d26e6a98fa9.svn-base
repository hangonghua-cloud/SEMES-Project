using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.EquipManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-12
    /// 2.创建作者: liyongguo
    /// 3.功能描述: EP_EquipmentManageItem实体
    /// 4.任务编号: 设备台账
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentManageItemEntity : BaseEntity
    { 
        #region 表: EP_EquipmentManageItem 实体类: EP_EquipmentManageItem 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 设备台账Id
        /// </summary>
        public string EMId {get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 配套设备编码
        /// </summary>
        public string EquipCode {get; set; } = "";
 
        /// <summary>
        /// 配套设备名称
        /// </summary>
        public string EquipName {get; set; } = "";
 
        /// <summary>
        /// 设备状态
        /// </summary>
        public string EquipStatus {get; set; } = "";

        /// <summary>
        ///配套设备类型
        /// </summary>
        public string EquipClass { get; set; } = "";

        /// <summary>
        /// 规格型号
        /// </summary>
        public string EquipSpec {get; set; } = "";
 
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string Manufacturer {get; set; } = "";
 
        /// <summary>
        /// 出厂日期
        /// </summary>
        public string ProducedDate {get; set; } = "";
 
        /// <summary>
        /// 使用日期
        /// </summary>
        public string UserDate {get; set; } = "";
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } = "";
 
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment {get; set; } = "";
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModfiyBy {get; set; } = "";
 
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }
 
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
