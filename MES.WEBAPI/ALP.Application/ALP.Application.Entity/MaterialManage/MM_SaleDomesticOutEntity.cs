using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-29
    /// 2.创建作者: liyongguo
    /// 3.功能描述: MM_SaleDomesticOut实体
    /// 4.任务编号: 内销发货单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_SaleDomesticOutEntity : BaseEntity
    { 
        #region 表: MM_SaleDomesticOut 实体类: MM_SaleDomesticOut 
 
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
        /// 发货单号
        /// </summary>
        public string DocNum {get; set; } = "";
 
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer {get; set; } = "";
 
        /// <summary>
        /// 发货单状态
        /// </summary>
        public string Status {get; set; } = "";
        /// <summary>
        /// 车牌
        /// </summary>
        public string LicensePlate { get; set; } = "";
        

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } = "";
 
        /// <summary>
        /// 发货日期
        /// </summary>
        public string DeliveryDate {get; set; } = "";
 
        /// <summary>
        /// 发货人
        /// </summary>
        public string Operator {get; set; } = "";
 
        /// <summary>
        /// 发货人名称
        /// </summary>
        public string OperatorName {get; set; } = "";
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorName {get; set; } = "";
 
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
