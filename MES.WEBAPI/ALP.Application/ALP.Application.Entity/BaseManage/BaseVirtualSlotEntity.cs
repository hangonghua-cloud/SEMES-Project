using ALP.Application.Code;
using System;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2019.06.06 16:19
    /// 描 述：虚拟槽
    /// </summary>
    public class BaseVirtualSlotEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 虚拟槽ID
        /// </summary>		
        public int SlotId { get; set; }
        /// <summary>
        /// 模架数量
        /// </summary>		
        public int? FixtureNumber { get; set; }
        /// <summary>
        /// 线体编号
        /// </summary>		
        public string LineCode { get; set; }
        // /// <summary>
        // /// 模具编码
        // /// </summary>		
        //public string MouldCode { get; set; }
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