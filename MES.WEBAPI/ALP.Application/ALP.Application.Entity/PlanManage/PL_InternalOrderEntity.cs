using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.PlanManage
{
    /// <summary>
    /// 1.创建日期:2024-3-8 14:48:08
    /// 2.创建作者: jpf
    /// 3.功能描述: PL_InternalOrder实体
    /// 4.任务编号: 内部订单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_InternalOrderEntity :BaseEntity
    {
        #region 表:  PL_InternalOrder 实体类:  PL_InternalOrderEntity

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 内部订单
        /// </summary>
        public string InternalOrder { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 订单描述
        /// </summary>
        public string OrderDescription { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string Companycode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        #endregion
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
    }
}
