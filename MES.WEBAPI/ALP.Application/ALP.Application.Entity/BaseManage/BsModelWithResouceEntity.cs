using ALP.Application.Code;
using System;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型资源
    /// </summary>
    public class BsModelWithResourceEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 资源编号
        /// </summary>		
        public string ResourceCode { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>		
        public string ResourceName { get; set; }
        /// <summary>
        /// 资源层级
        /// </summary>		
        public string ModelLeve { get; set; }
        /// <summary>
        /// 父级分类
        /// </summary>		
        public string ParentResource { get; set; }
        /// <summary>
        /// 描述
        /// </summary>		
        public string Describe { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? SortCode { get; set; }
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
