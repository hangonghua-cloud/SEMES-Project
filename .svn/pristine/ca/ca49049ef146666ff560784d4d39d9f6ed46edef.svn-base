using ALP.Application.Code;
using System;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型层级扩展
    /// </summary>
    public class BsModelLevelExtendFieldsEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>		
        public string id { get; set; }
        /// <summary>
        /// 层级编号
        /// </summary>		
        public string LevelCode { get; set; }
        /// <summary>
        /// 字段编号
        /// </summary>		
        public string FieldCode { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>		
        public string FieldName { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>		
        public string FieldType { get; set; }
        /// <summary>
        /// 是否保留
        /// </summary>		
        public bool? IsReserve { get; set; }
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
            this.id = System.Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.id = keyValue;
        }
        #endregion
    }
}
