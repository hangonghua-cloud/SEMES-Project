using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ALP.Application.Code;

namespace ALP.Application.Entity.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.3.13 16:19
    /// 描 述：用户信息与登录
    /// </summary>
    public class BSAppLogUserInfoEntity : BaseEntity
    {
        #region 实体成员
 
        public int Id { get; set; }

        public string UserId { get; set; }
 
        public string APPRole { get; set; }
        [NotMapped]
        public string RoleName { get; set; }


        public string PWD { get; set; }
   
        public bool EnabledMark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        /// <returns></returns>
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        /// <returns></returns>
        public string ModifyUser { get; set; }

        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            //this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(int keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}
