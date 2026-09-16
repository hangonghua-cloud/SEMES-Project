using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: BS_Process实体
    /// 4.任务编号: 供应商管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_ProcessEntity : BaseEntity
    { 
        #region 表: BS_Process 实体类: BS_Process 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode {get; set; } = "";
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 工艺编码
        /// </summary>
        public string ProcessCode {get; set; } = "";
 
        /// <summary>
        /// 工艺名称
        /// </summary>
        public string ProcessName {get; set; } = "";
 
        /// <summary>
        /// 物料分类
        /// </summary>
        public string MaterialClass {get; set; } = "";
 
        /// <summary>
        /// SmallClass
        /// </summary>
        public string SmallClass {get; set; } = "";
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } = "";
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } = "";
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTimeOffset? ModifyTime {get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string ProcessType { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// 是否删除
        /// SAP标识
        /// </summary>
        [NotMapped]
        public bool IsDeleted { get; set; }
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
