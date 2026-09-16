using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.BaseManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-11-27
    /// 2.创建作者: admin
    /// 3.功能描述: Base_UpdateLog实体
    /// 4.任务编号: 升级日志
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_UpdateLogEntity : BaseEntity
    { 
        #region 表: Base_UpdateLog 实体类: Base_UpdateLog 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType {get; set; } = "";
 
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version {get; set; } = "";
 
        /// <summary>
        /// 更新内容
        /// </summary>
        public string Content {get; set; } = "";
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyBy { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }

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
 
        #endregion
    }
}
