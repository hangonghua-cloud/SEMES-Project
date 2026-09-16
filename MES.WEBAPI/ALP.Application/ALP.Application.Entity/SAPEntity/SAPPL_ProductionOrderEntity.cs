using ALP.Application.Entity.PlanManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity
{
    /// <summary>
    /// 创建：jpf
    /// 时间：2024-3-20 09:18:35
    /// 描述：接收SAP销售订单信息
    /// </summary>
 public    class SAPPL_ProductionOrderEntity
    {
        ///// <summary>
        ///// 是否修改
        ///// </summary>
        //public bool isUpdate { get; set; }

        public PL_ProductionOrderEntity ProductionOrderEntity;

        public List<PL_WorkOrderEntity> WorkOrders;
    }
}
