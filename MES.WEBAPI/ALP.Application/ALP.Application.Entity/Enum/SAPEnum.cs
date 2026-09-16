using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.Enum
{
    /// <summary>
    /// SAP接口枚举
    /// </summary>
    public enum SAPInterface
    {
        /// <summary>
        /// 调拨过账
        /// </summary>
        [Description("调拨过账")]
        IF102 = 0,
        /// <summary>
        /// 销售交货单过账/冲销
        /// </summary>
        [Description("销售交货单过账/冲销")]
        IF106 = 1,
        /// <summary>
        /// 采购订单入库
        /// </summary>
        [Description("采购订单入库")]
        IF108 = 2,
        /// <summary>
        /// 采购订单冲销
        /// </summary>
        [Description("采购订单冲销")]
        IF109 = 3,
        /// <summary>
        /// 单创建及下达
        /// </summary>
        [Description("单创建及下达")]
        IF110 = 4,
        /// <summary>
        /// 工单更改
        /// </summary>
        [Description("工单更改")]
        IF111 = 5,
        /// <summary>
        /// 工单领退料
        /// </summary>
        [Description("工单领退料")]
        IF112 = 6,
        /// <summary>
        /// 领退料过账
        /// </summary>
        [Description("领退料过账")]
        IF116 = 7,
        /// <summary>
        /// 工单入库
        /// </summary>
        [Description("工单入库")]
        IF119 = 8,
        /// <summary>
        /// 成本收集器物料消耗与冲销
        /// </summary>
        [Description("成本收集器物料消耗与冲销")]
        IF121 = 9,
        /// <summary>
        /// 工单取消入库
        /// </summary>
        [Description("工单取消入库")]
        IF122 = 10,
        /// <summary>
        /// 交货单查询
        /// </summary>
        [Description("交货单查询")]
        IF123 = 11,
        /// <summary>
        /// 工单报工
        /// </summary>
        [Description("工单报工")]
        IF126 = 12,
        /// <summary>
        /// 工单状态维护
        /// </summary>
        [Description("工单状态维护")]
        IF128 = 13,
        /// <summary>
        /// 库存查询
        /// </summary>
        [Description("库存查询")]
        IF135 = 14,
        /// <summary>
        /// 库存查询
        /// </summary>
        [Description("库存查询")]
        IF150 = 15,
        /// <summary>
        /// 工单报工、入库、领退料
        /// </summary>
        [Description("工单报工、入库、领退料")]
        IF152 = 16,
    }

    /// <summary>
    /// SAP业务类型
    /// </summary>
    public enum SAPBusinessType
    {
        /// <summary>
        /// 流转卡报工
        /// </summary>
        [Description("流转卡报工")]
        流转卡报工 = 1,
        /// <summary>
        /// 包装报工
        /// </summary>
        [Description("包装报工")]
        包装报工 = 2,
        /// <summary>
        /// 自制半成品报工
        /// </summary>
        [Description("自制半成品报工")]
        自制半成品报工 = 3,
        /// <summary>
        /// 领退料
        /// </summary>
        [Description("领退料")]
        领退料 = 4,
        /// <summary>
        /// 包装入库
        /// </summary>
        [Description("包装入库")]
        包装入库 = 5,
        /// <summary>
        /// 自制半成品入库
        /// </summary>
        [Description("自制半成品入库")]
        自制半成品入库 = 6,
        /// <summary>
        /// 超产品入库
        /// </summary>
        [Description("超产品入库")]
        超产品入库 = 7,
        /// <summary>
        /// 超产品领退料
        /// </summary>
        [Description("超产品领退料")]
        超产品领退料 = 8,
    }
}
