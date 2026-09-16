using ALP.Application.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.AppManage
{
    /// <summary>
    /// App_AppVersion
    /// </summary>
    
    public class AppVersionEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// app类型
        /// </summary>
        public int? AppType { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadURL { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool? EnableFlag { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Creator = OperatorProvider.Provider.Current().Account;
            this.CreateTime = DateTime.Now;
            this.EnableFlag = true;
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