using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ALP.Application.Code;

namespace ALP.Application.Entity.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典分类
    /// </summary>
    public class DataItemEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 分类主键
        /// </summary>		
        public string ItemId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public string ParentId { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>		
        public string ItemCode { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>		
        public string ItemName { get; set; }
        /// <summary>
        /// 明细分类值
        /// </summary>		
        [NotMapped]
        public string ItemValue { get; set; }
        /// <summary>
        /// 树型结构
        /// </summary>		
        public int? IsTree { get; set; }
        /// <summary>
        /// 是否内置
        /// </summary>		
        public int? IsDefault { get; set; }
        /// <summary>
        /// 导航标记
        /// </summary>		
        public int? IsNav { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime ? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>		
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>		
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>		
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>		
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>		
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ItemId = Guid.NewGuid().ToString();
            this.CreateDate = this.CreateDate ?? DateTime.Now;
            this.DeleteMark = 0;
            if (OperatorProvider.AppUserId != null)
            {
                this.CreateUserId = OperatorProvider.Provider.Current().UserId;
                this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            }
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ItemId = keyValue;
            this.ModifyDate = this.ModifyDate ?? DateTime.Now;
            if (OperatorProvider.AppUserId != null)
            {
                this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
                this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
            }
        }
        #endregion
    }
}