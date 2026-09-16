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
    public class SubEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string Id { get; set; }
        /// <summary>
        /// 父级编码
        /// </summary>
        /// <returns></returns>
        [Column("PARENTCODE")]
        public string ParentCode { get; set; }
        /// <summary>
        /// 工作
        /// </summary>
        /// <returns></returns>
        [Column("COCO")]
        public string COCO { get; set; }
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
            this.CreateTime = DateTime.Now;
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