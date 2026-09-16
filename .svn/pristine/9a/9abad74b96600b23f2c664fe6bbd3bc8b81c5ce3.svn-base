using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: BS_ProcessOfOperations实体
    /// 4.任务编号: 供应商管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_ProcessOfOperationsEntity : BaseEntity
    { 
        #region 表: BS_ProcessOfOperations 实体类: BS_ProcessOfOperations 
 
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
        /// 工艺编码
        /// </summary>
        public string ProcessCode {get; set; } = "";
 
        /// <summary>
        /// 工序编码
        /// </summary>
        public string OperationCode { get; set; }  
        /// <summary>
        /// 工序名称
        /// </summary>
        public string OperationName { get; set; }  

        /// <summary>
        /// 顺序号
        /// </summary>
        public int SN {get; set; }
 
        /// <summary>
        /// 产出仓库编码
        /// </summary>
        public string OutWarehouse {get; set; } = "";
 
        /// <summary>
        /// 养生周期(天)
        /// </summary>
        public decimal? CuringCycle {get; set; }
 
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
