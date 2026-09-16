using System;
using ALP.Application.Code;

namespace ALP.Application.Entity.SystemManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-09-02 10:39
    /// 描 述：仓库表 别名：WH
    /// </summary>
    public class WMSBaseWareHourseEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键ID
        /// </summary>
        /// <returns></returns>
        public Guid id { get; set; }
        /// <summary>
        /// 仓库代码
        /// </summary>
        /// <returns></returns>
        public string whCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        /// <returns></returns>
        public string whName { get; set; }
        /// <summary>
        /// ERP仓库编码
        /// </summary>
        /// <returns></returns>
        public string erpWHCode { get; set; }
        /// <summary>
        /// 可用标识
        /// </summary>
        /// <returns></returns>
        public int? enabledFlag { get; set; }
        /// <summary>
        /// 盘点标识
        /// </summary>
        /// <returns></returns>
        public int? checkFlag { get; set; }
        /// <summary>
        /// 锁定标识
        /// </summary>
        /// <returns></returns>
        public bool? isLock { get; set; }
        /// <summary>
        /// 位置编号
        /// </summary>
        /// <returns></returns>
        public string locationCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string remark { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? createDate { get; set; }
        /// <summary>
        /// 创建人Code
        /// </summary>
        /// <returns></returns>
        public string createUserCode { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        /// <returns></returns>
        public string createUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? modifyDate { get; set; }
        /// <summary>
        /// 修改人Code
        /// </summary>
        /// <returns></returns>
        public string modifyUserCode { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        /// <returns></returns>
        public string modifyUserName { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public bool isDeleted { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.id = Guid.NewGuid();
            this.createDate = DateTimeOffset.Now;
            if (OperatorProvider.AppUserId != null)
            {
                this.createUserName = OperatorProvider.Provider.Current().UserName;
            }
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.id = Guid.Parse(keyValue);
            this.modifyDate = DateTime.Now;
            if (OperatorProvider.AppUserId != null)
            {
                this.modifyUserName = OperatorProvider.Provider.Current().UserName;
            }
        }
        #endregion
    }
}