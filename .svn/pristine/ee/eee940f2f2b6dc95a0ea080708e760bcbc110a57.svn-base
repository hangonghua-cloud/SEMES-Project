using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-24
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_SupplierManage实体
    /// 4.任务编号: 供应商管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_SupplierManageEntity : BaseEntity
    { 
        #region 表: Base_SupplierManage 实体类: Base_SupplierManage 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string SupplierCode {get; set; } = "";
 
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName {get; set; } = "";

        /// <summary>
        /// 供应商类别
        /// </summary>
        public string Category {get; set; } = "";
 
        /// <summary>
        /// 简称
        /// </summary>
        public string Abbr {get; set; } = "";

        /// <summary>
        /// 是否生效 1生效 0失效
        /// </summary>
        public bool? IsEnabled { get; set; }


        /// <summary>
        /// 是否生效 1生效 0失效
        /// </summary>
        [NotMapped]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } = "";
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTimeOffset? ModifyTime {get; set; }
        /// <summary>
        /// 供应商等级
        /// </summary>
        public string SupplierLevel { get; set; }
        /// <summary>
        /// 供应商仓库
        /// sap需要
        /// </summary>
        public string SupplierWarehouse { get; set; }
        /// <summary>
        /// 父级供应商编码
        /// </summary>
        public string ParentSupplierCode { get; set; }

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
