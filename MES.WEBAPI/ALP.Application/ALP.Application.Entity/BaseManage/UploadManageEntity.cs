using System;
using ALP.Application.Code;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-07-18 16:58
    /// 描 述：文件上传管理
    /// </summary>
    public class UploadManageEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }

        /// <summary>
        /// FId
        /// </summary>
        /// <returns></returns>
        public string FId { get; set; }
   
        /// <summary>
        /// FileName
        /// </summary>
        /// <returns></returns>
        public string FileName { get; set; }
        /// <summary>
        /// UploadBy
        /// </summary>
        /// <returns></returns>
        public string UploadBy { get; set; }
        /// <summary>
        /// UploadTime
        /// </summary>
        /// <returns></returns>
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// DeleteBy
        /// </summary>
        /// <returns></returns>
        public string DeleteBy { get; set; }
        /// <summary>
        /// DeleteTime
        /// </summary>
        /// <returns></returns>
        public DateTime? DeleteTime { get; set; }
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