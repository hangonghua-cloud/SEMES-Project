using ALP.Application.Code;
using System;

namespace ALP.Application.Entity.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.4.9 16:19
    /// 描 述：打印服务人员绑定
    /// </summary>
    public class BsPeopleByPrintServerEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PrintServerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PeopleCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsEnable { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.CreatedTime = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = int.Parse(keyValue);
            this.CreatedTime = DateTime.Now;
        }
        #endregion
    }
}
