using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-11-02
    /// 2.创建作者: jpf
    /// 3.功能描述: BS_TraitManage实体
    /// 4.任务编号: 特征维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_TraitManageEntity : BaseEntity
    { 
        #region 表: BS_TraitManage 实体类: BS_TraitManage 
 
        /// <summary>
        /// ID
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode {get; set; } = "";
 
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName {get; set; } = "";
        /// <summary>
        /// 特征编码
        /// </summary>
        public string TraitCode { get; set; } = "";
        /// <summary>
        /// 特征名称
        /// </summary>
        public string TraitName {get; set; } = "";
 
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable {get; set; }
 
        /// <summary>
        /// 默认为0删除为1
        /// </summary>
        public bool? IsDeleted {get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateName {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } = "";
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }
 
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string ModifyName {get; set; } = "";
 
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
