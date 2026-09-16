using ALP.Application.Code;
using System;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.1.4 16:19
    /// 描 述：物料主数据
    /// </summary>
    public class MaterialManageEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 料号
        /// </summary>		
        public string MaterialCode { get; set; }
        /// <summary>
        /// 品名
        /// </summary>		
        public string MaterialName { get; set; }
        /// <summary>
        /// 规格
        /// </summary>		
        public string Spec { get; set; }
        /// <summary>
        /// 形态属性
        /// </summary>		
        public string Abbr { get; set; }
        /// <summary>
        /// 标准等级
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// 标准成分
        /// </summary>
        public string Composition { get; set; }
        /// <summary>
        /// 料品状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 默认采购单位
        /// </summary>
        public string PurchasingDept { get; set; }
        /// <summary>
        /// 默认领料单位
        /// </summary>
        public string PickingDept { get; set; }
        /// <summary>
        /// 是否生效
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 批号参数
        /// </summary>
        public bool? IsCheckValidity { get; set; }
        /// <summary>
        /// 批号有效期天数
        /// </summary>
        public int? ValidityDays { get; set; }
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
            this.MaterialCode = keyValue;
        }
        #endregion
    }
}
