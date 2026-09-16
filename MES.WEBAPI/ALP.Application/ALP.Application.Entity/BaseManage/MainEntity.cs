using System;
using System.ComponentModel.DataAnnotations.Schema;
using ALP.Application.Code;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 
    /// 创 建：超级管理员
    /// 日 期：2019-05-10 15:01
    /// 描 述：Main
    /// </summary>
    public class MainEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        [Column("CODE")]
        public string Code { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("CREATETIME")]
        public DateTime? CreateTime { get; set; }
        #endregion

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
    }
}