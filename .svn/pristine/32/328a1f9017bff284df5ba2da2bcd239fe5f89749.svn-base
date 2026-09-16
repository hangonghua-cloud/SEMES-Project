using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_OwnProductOrder实体
    /// 4.任务编号: 自制半成品工单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_OwnProductOrderEntity : BaseEntity
    { 
        #region 表: PM_OwnProductOrder 实体类: PM_OwnProductOrder 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode {get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string ProcessCode {get; set; } = "";
 
        /// <summary>
        /// 工单状态 1：创建  5：生产中 6：已完成 9：SAP结案
        /// </summary>
        public string OrderStatus { get; set; }
 
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder {get; set; } = "";
        /// <summary>
        /// bom
        /// </summary>
        public string BOMCode { get; set; } = "";
        /// <summary>
        /// 工艺路线
        /// </summary>
        public string ProcessRoute { get; set; } = "";

        /// <summary>
        /// 物料小类
        /// </summary>
        public string SmallClass {get; set; } = "";
        /// <summary>
        /// 物料小类名称
        /// </summary>
        public string SmallClassName { get; set; } = "";
        
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode {get; set; } = "";
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; } = "";
        

        /// <summary>
        /// 计划数量
        /// </summary>
        public decimal? PlanQty {get; set; }
 
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
        /// 是否过账 1：已过账
        /// </summary>
        public string IsPosted { get; set; }
        /// <summary>
        /// 过账消息
        /// </summary>
        public string PostedMsg { get; set; }
        /// <summary>
        /// 过账时间
        /// </summary>
        public DateTime? PostedTime { get; set; }
        /// <summary>
        /// 过账人员
        /// </summary>
        public string PostedUser { get; set; }
        /// <summary>
        /// SAP工单号
        /// </summary>
        public string SAP_AUFNR { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public string LineNum { get; set; }
        /// <summary>
        /// 更新是否过账 1：已过账
        /// </summary>
        public string Update_IsPosted { get; set; }
        /// <summary>
        /// 更新过账消息
        /// </summary>
        public string Update_PostedMsg { get; set; }
        /// <summary>
        /// 更新过账时间
        /// </summary>
        public DateTime? Update_PostedTime { get; set; }
        /// <summary>
        /// 更新过账人员
        /// </summary>
        public string Update_PostedUser { get; set; }
        /// <summary>
        /// 生产数量
        /// </summary>
        public decimal? ProductQty { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

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
