using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 4.任务编号: 质量首检确认工序表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_FirstInspectionConfirmEntity : BaseEntity
    {
        #region 表: PL_FirstInspectionConfirm 实体类: PL_FirstInspectionConfirm 

        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 订单编码
        /// </summary>
        public string ProductOrder {get; set; } 
 
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder {get; set; } 
 
        /// <summary>
        /// 客户型号
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 面膜型号
        /// </summary>
        public string MMXH { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNO {get; set; } 
 
  
        /// <summary>
        /// 首检确认工序
        /// </summary>
        public string Process { get; set; }
        /// <summary>
        /// 检验状态
        /// </summary>
        public string FirstStatus { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } 
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } 
 
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
