using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2023-02-28
    /// 2.创建作者: jpf
    /// 3.功能描述: Base_DataFileCon实体
    /// 4.任务编号: 仓库安全库存
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_DataFileConEntity : BaseEntity
    { 
        #region 表: Base_DataFileCon 实体类: Base_DataFileCon 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 模块
        /// </summary>
        public string module {get; set; } = "";
 
        /// <summary>
        /// 表名
        /// </summary>
        public string tableName {get; set; } = "";
 
        /// <summary>
        /// 表实体
        /// </summary>
        public string TableEntity {get; set; } = "";
 
        /// <summary>
        /// 是否归档
        /// </summary>
        public bool isFile {get; set; }
 
        /// <summary>
        /// 创建人编码
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorName {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人编码
        /// </summary>
        public string ModifyBy {get; set; } = "";
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyByName {get; set; } = "";
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }
 
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
