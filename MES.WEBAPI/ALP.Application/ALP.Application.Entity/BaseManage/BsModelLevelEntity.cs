using ALP.Application.Code;
using System;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型层级
    /// </summary>
    public class BsModelLevelEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 层级编号
        /// </summary>		
        public string LevelCode { get; set; }
        /// <summary>
        /// 层级名称
        /// </summary>		
        public string LevelName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>		
        public string Describe { get; set; }
        /// <summary>
        /// 层级
        /// </summary>		
        public int? Level { get; set; }
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

        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
        }
        #endregion
    }
}
