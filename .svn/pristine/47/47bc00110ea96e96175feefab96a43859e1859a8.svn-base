using System;
using System.ComponentModel.DataAnnotations.Schema;
using ALP.Application.Code;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-09-19 11:53
    /// 描 述：MESBaseAccount
    /// </summary>
    public class MESBaseAccountEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public Guid ID { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        /// <returns></returns>
        public string FirstName { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        /// <returns></returns>
        public string LastName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        /// <returns></returns>
        public string UserCode { get; set; }
        /// <summary>
        /// 工牌编码
        /// </summary>
        /// <returns></returns>
        public string UserEnCode { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? CreateDate { get; set; }
        /// <summary>
        /// CreateUserCode
        /// </summary>
        /// <returns></returns>
        public string CreateUserCode { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? ModifyDate { get; set; }
        /// <summary>
        /// ModifyUserCode
        /// </summary>
        /// <returns></returns>
        public string ModifyUserCode { get; set; }
        /// <summary>
        /// ModifyUserName
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }

        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion

        #region 拓展属性
        /// <summary>
        /// 主键ID
        /// </summary>
        [NotMapped]
        public string PrimaryKey { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ID = Guid.NewGuid();
            this.CreateDate = DateTime.Now;
            if (OperatorProvider.AppUserId != null)
            {
                this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            }
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = Guid.Parse(keyValue);
            this.ModifyDate = DateTime.Now;
            if (OperatorProvider.AppUserId != null)
            {
                this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
            }
        }
        #endregion
    }
}