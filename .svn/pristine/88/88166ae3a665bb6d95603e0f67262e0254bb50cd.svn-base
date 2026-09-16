using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-18
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_ProcessOfOperations实体
    /// 4.任务编号: 订单采购
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Serializable]
    public class PL_ProcessOfOperationsEntity : BaseEntity
    { 
        #region 表: PL_ProcessOfOperations 实体类: PL_ProcessOfOperations 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 工艺Id
        /// </summary>
        public string ProcessId {get; set; } = "";
 
        /// <summary>
        /// 工艺编码
        /// </summary>
        public string ProcessCode {get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string OperationCode {get; set; } = "";
 
        /// <summary>
        /// 工序名称
        /// </summary>
        public string OperationName {get; set; } = "";
 
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
        /// 是否删除
        /// jpf add 2022-11-30 
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 外发标识  1：外发 2：自制
        /// </summary>
        public string WFMark { get; set; }

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
