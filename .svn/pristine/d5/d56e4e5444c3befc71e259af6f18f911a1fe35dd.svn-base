using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-18
    /// 2.创建作者: admin
    /// 3.功能描述: PM_TransferBGPersonRecord实体
    /// 4.任务编号: 流转报工人员记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_TransferBGPersonRecordEntity : BaseEntity
    { 
        #region 表: PM_TransferBGPersonRecord 实体类: PM_TransferBGPersonRecord 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; }
 
        /// <summary>
        /// 报工ID
        /// </summary>
        public string BGID {get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 生产小组编码
        /// </summary>
        public string PTeamCode {get; set; }
 
        /// <summary>
        /// 生产小组名称
        /// </summary>
        public string PTeamName {get; set; }
 
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string PostCode {get; set; }
 
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string PostName {get; set; }
 
        /// <summary>
        /// 人员编码
        /// </summary>
        public string UserCode {get; set; }
 
        /// <summary>
        /// 人员名称
        /// </summary>
        public string UserName {get; set; }
        /// <summary>
        /// 岗位系数
        /// </summary>
        public decimal? Coefficient { get; set; }
        /// <summary>
        /// 产品单价
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// 有效标记
        /// </summary>
        public bool? IsEnabled { get; set; }
        /// <summary>
        /// 创建人编码
        /// </summary>
        public string Creator {get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 修改人编码
        /// </summary>
        public string ModifyBy {get; set; }
 
        /// <summary>
        /// 修改时间
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
