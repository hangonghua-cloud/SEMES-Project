using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_ExeWorkOrder实体
    /// 4.任务编号: 生产执行工单表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_ExeWorkOrderEntity : BaseEntity
    {
        #region 表: PL_ExeWorkOrder 实体类: PL_ExeWorkOrder 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 发料工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 发料工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [NotMapped]
        public string ProductOrder  { get;set;}
        /// <summary>
        /// 柜号
        /// </summary>
        [NotMapped]
        public string ContainerNO { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }

        /// <summary>
        /// 执行工单号
        /// </summary>
        public string ExeWorkOrder { get; set; }

        /// <summary>
        /// 执行工单类型
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 执行工单状态 1：未开始 2：正在生产 3：已完成
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 派工状态
        /// </summary>
        public string AssignStatus { get; set; }
        /// <summary>
        /// 生产张数
        /// </summary>
        [NotMapped]
        public decimal? OrderSheets { get; set; }
        /// <summary>
        /// 生产张数
        /// </summary>
        public decimal? PSheetsQty { get; set; }
        /// <summary>
        /// 发料张数
        /// </summary>
        public decimal? SheetsQty { get; set; }

        /// <summary>
        /// 生产片数
        /// </summary>
        public decimal? PiecesQty { get; set; }

        /// <summary>
        /// 良率
        /// </summary>
        public decimal? Yield { get; set; }

        /// <summary>
        /// 应发米数
        /// </summary>
        public decimal? ShouldNum { get; set; }

        /// <summary>
        /// 实发米数
        /// </summary>
        public decimal? ActualNum { get; set; }

        ///// <summary>
        ///// 超产品抵扣
        ///// </summary>
        //public decimal? DeductionNum { get; set; }

        /// <summary>
        /// 超发米数
        /// </summary>
        public decimal? SuperNum { get; set; }

        /// <summary>
        /// 退库米数
        /// </summary>
        public decimal? CancellingNum { get; set; }

        /// <summary>
        /// 实际消耗米数
        /// </summary>
        public decimal? ConsumeNum { get; set; }
        /// <summary>
        /// 工艺路线
        /// </summary>
        public string Process { get; set; }

        /// <summary>
        /// 起始工序
        /// </summary>
        public string StartOperation { get; set; }

        /// <summary>
        /// 流转方式
        /// </summary>
        public string TransferBy { get; set; }
        /// <summary>
        /// 有效标记
        /// </summary>
        public bool? IsEnabled {get;set;}
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
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 柜数
        /// </summary>
        public decimal? OrderPiecesNum { get; set; }

        /// <summary>
        /// 柜总片数
        /// </summary>
        public decimal? OrderPiecesAll { get; set; }
        /// <summary>
        /// 超产品批次
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 超产品库存Id
        /// </summary>
        public string SupId { get; set; }
        /// <summary>
        /// 发料批次
        /// </summary>
        public string SendOutBatch { get; set; }

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
