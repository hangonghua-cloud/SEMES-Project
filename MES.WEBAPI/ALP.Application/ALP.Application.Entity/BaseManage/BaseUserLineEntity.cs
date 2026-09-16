using ALP.Application.Code;
using System;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.12.21 16:19
    /// 描 述：用户线体
    /// </summary>
    public class BaseUserLineEntity : BaseEntity
    {
        #region 实体成员
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“BaseUserLineEntity.Id”的 XML 注释
        public string Id { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“BaseUserLineEntity.Id”的 XML 注释
        /// <summary>
        /// 用户ID
        /// </summary>		
        public string UserId { get; set; }
        /// <summary>
        /// 线体编号
        /// </summary>		
        public string LineCode { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>		
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime? CreateTime { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CreateTime = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
        }
        #endregion
    }
}