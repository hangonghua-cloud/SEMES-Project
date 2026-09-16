using ALP.Application.Code;
using System;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型资源扩展
    /// </summary>
    public class BsModelResourceExtendInfoEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>		
        public string Id { get; set; }
        /// <summary>
        /// 资源编码
        /// </summary>		
        public string ResourceCode { get; set; }
        /// <summary>
        /// 字段编码
        /// </summary>		
        public string FieldCode { get; set; }
        /// <summary>
        /// 字段值
        /// </summary>		
        public string FieldValue { get; set; }
        /// <summary>
        /// 描述
        /// </summary>		
        //public string Describe { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyUser { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyDate { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}
