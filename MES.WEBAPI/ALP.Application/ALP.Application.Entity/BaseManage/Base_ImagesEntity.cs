using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.BaseManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-13
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_Images实体
    /// 4.任务编号: 文件管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_ImagesEntity : BaseEntity
    { 
        #region 表: Base_Images 实体类: Base_Images 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 所属模块
        /// </summary>
        public string Module {get; set; } = "";
 
        /// <summary>
        /// 关联表
        /// </summary>
        public string TableName {get; set; } = "";
 
        /// <summary>
        /// 关联表Id
        /// </summary>
        public string ParentId {get; set; } = "";
 
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName {get; set; } = "";
 
        /// <summary>
        /// 文件地址
        /// </summary>
        public string FilePath {get; set; } = "";
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; } = "";

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string ImgType {get; set; } = "";
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.CreateTime = DateTime.Now;
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
