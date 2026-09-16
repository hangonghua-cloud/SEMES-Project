using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-11-16
    /// 2.创建作者: jpf
    /// 3.功能描述: PL_TransfersRecord实体
    /// 4.任务编号: 跨工厂调拨
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_TransfersRecordEntity : BaseEntity
    { 
        #region 表: PL_TransfersRecord 实体类: PL_TransfersRecord 
 
        /// <summary>
        /// ID
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 发起工厂编码
        /// </summary>
        public string SendFactoryCode {get; set; } = "";
 
        /// <summary>
        /// 发起工厂名称
        /// </summary>
        public string SendFactoryName {get; set; } = "";
 
        /// <summary>
        /// 接收工厂编码
        /// </summary>
        public string AcceptFactoryCode {get; set; } = "";
 
        /// <summary>
        /// 接收工厂名称
        /// </summary>
        public string AcceptFactoryName {get; set; } = "";
 
        /// <summary>
        /// 订单编码
        /// </summary>
        public string ProductOrder {get; set; } = "";
 
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder {get; set; } = "";
 
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode {get; set; } = "";
 
        /// <summary>
        /// 调拨类型:全工序调拨、部分工序调拨数据字典取值
        /// </summary>
        public string TransTypeCode {get; set; } = "";
 
        /// <summary>
        /// 调拨类型:全工序调拨、部分工序调拨数据字典取值
        /// </summary>
        public string TransTypeName {get; set; } = "";
 
        /// <summary>
        /// 调拨工序编码
        /// </summary>
        public string TransProcessCode {get; set; } = "";
 
        /// <summary>
        /// 调拨工序名称
        /// </summary>
        public string TransProcessName {get; set; } = "";
 
        /// <summary>
        /// 调拨状态：0待确认、1已确认、2已回退
        /// </summary>
        public string TransStateCode {get; set; } = "";
 
        /// <summary>
        /// 调拨状态：0待确认、1已确认、2已回退
        /// </summary>
        public string TransStateName {get; set; } = "";
 
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } = "";
 
        /// <summary>
        /// 删除标识1代表已删除0代表未删除
        /// </summary>
        public bool? IsDelete {get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateName {get; set; } = "";
 
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
