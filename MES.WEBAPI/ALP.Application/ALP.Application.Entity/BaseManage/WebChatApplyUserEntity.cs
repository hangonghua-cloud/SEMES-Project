using System;
using ALP.Application.Code;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-03 11:53
    /// 描 述：微应用人员管理
    /// </summary>
    public class WebChatApplyUserEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 微应用人员主键
        /// </summary>
        /// <returns></returns>
        public string ApplyUserId { get; set; }

        /// <summary>
        /// 预警类型主键
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }

        /// <summary>
        /// 所属线体
        /// </summary>
        /// <returns></returns>
        public string LineCode { get; set; }

        /// <summary>
        /// 员工主键
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 员工代码
        /// </summary>
        /// <returns></returns>
        public string EnCode { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        /// <returns></returns>
        public string RealName { get; set; }
        /// <summary>
        /// 员工电话
        /// </summary>
        /// <returns></returns>
        public string OuterPhone { get; set; }

        /// <summary>
        /// 是否负责人
        /// </summary>
        /// <returns></returns>
        public int? IfManager { get; set; } 
 
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ApplyUserId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ApplyUserId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}