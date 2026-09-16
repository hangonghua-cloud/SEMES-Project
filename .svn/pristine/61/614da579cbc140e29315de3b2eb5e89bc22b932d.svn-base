using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.AppManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-04-10
    /// 2.创建作者: why
    /// 3.功能描述: AppModelEntity_Sie实体
    /// 4.任务编号: 
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class AppRolesConfEntity : BaseEntity
    {
        #region 表: V_Sy_AppRolesConf 实体类: V_Sy_AppRolesConf 

        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; } = new Guid();

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; } = "";

        /// <summary>
        /// UserCode
        /// </summary>
        public string UserCode { get; set; } = "";

        /// <summary>
        /// RoleCode
        /// </summary>
        public string RoleCode { get; set; } = "";

        /// <summary>
        /// RoleName
        /// </summary>
        public string RoleName { get; set; } = "";

        /// <summary>
        /// ModelCode
        /// </summary>
        public string ModelCode { get; set; } = "";

        /// <summary>
        /// ModelName
        /// </summary>
        public string ModelName { get; set; } = "";

        /// <summary>
        /// ModelType
        /// </summary>
        public string ModelType { get; set; } = "";

        /// <summary>
        /// OrderByNum
        /// </summary>
        public int OrderByNum { get; set; }

        /// <summary>
        /// ModelColor
        /// </summary>
        public string ModelColor { get; set; } = "";

        /// <summary>
        /// ModelIco
        /// </summary>
        public string ModelIco { get; set; } = "";

        /// <summary>
        /// OwnedPage
        /// </summary>
        public string OwnedPage { get; set; } = "";

        /// <summary>
        /// OwnedPageName
        /// </summary>
        public string OwnedPageName { get; set; } = "";

        /// <summary>
        /// SortNum
        /// </summary>
        public int SortNum { get; set; }

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.UserId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.UserId = keyValue;
        }
        #endregion

        #endregion
    }
}
