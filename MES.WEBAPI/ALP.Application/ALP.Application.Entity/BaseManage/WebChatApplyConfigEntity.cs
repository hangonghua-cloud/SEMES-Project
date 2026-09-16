using System;
using ALP.Application.Code;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-04 13:32
    /// 描 述：微应用预警配置表
    /// </summary>
    public class WebChatApplyConfigEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 应用推送主键
        /// </summary>
        /// <returns></returns>
        public string ApplyConfigId { get; set; }
        /// <summary>
        /// 预警所属分类
        /// </summary>
        /// <returns></returns>
        public string ApplyType { get; set; }
        /// <summary>
        /// 所属线体
        /// </summary>
        /// <returns></returns>
        public string LineCode { get; set; }
        /// <summary>
        /// 目标率值
        /// </summary>
        /// <returns></returns>
        public string TargetValue { get; set; }
  
        /// <summary>
        /// 是否定时推送时间
        /// </summary>
        /// <returns></returns>
        public int? IfSendTime { get; set; }

        /// <summary>
        /// 定时推送时间
        /// </summary>
        /// <returns></returns>
        public TimeSpan TimingSendTime { get; set; }
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
            this.ApplyConfigId = Guid.NewGuid().ToString();
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
            this.ApplyConfigId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}