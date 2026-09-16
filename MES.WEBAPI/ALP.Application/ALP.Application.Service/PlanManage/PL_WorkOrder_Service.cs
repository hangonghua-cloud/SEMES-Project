using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.PlanManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.PlanManage;
using ALP.Application.UtilExtend.Offices;
using System.Configuration;
using System.Dynamic;
using ALP.Application.Entity.Material;
using ALP.Application.Entity.SAP;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALP.Application.Service.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_WorkOrderService 业务服务类
    /// 4.任务编号: 生产工单表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_WorkOrder_Service : RepositoryFactory<PL_WorkOrderEntity>, PL_WorkOrderIService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;

        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_WorkOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FactoryCode]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[CustomerPO]
                      ,[ContainerNO]
                      ,[OrderStatus]
                      ,[POStatus]
                      ,[MaterialCode]
                      ,[OrderPieces]
                      ,[OrderBox]
                      ,[OrderPallet]
                      ,[OrderStartPallet]
                      ,[DeliveryPieces]
                      ,[DeliveryBox]
                      ,[DeliveryPallet]
                      ,[DeliveryStartPallet]
                      ,[TotalSheets]
                      ,[Yield]
                      ,[ActualSheets]
                      ,[Process]
                      ,[StartOperation]
                      ,[TransferBy]
                      ,[FirstInspectionConfirm]
                      ,[AvoidProduce]
                      ,[FreezeFlag]
                      ,[IsEnabled]
                      ,[DemandMaterial]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_WorkOrder] where 1=1  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                    sql.Append($" AND FactoryCode like N'%{queryParam["FactoryCode"]}%'");
                }
                //订单编码 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //工单号 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //客户PO号 是否为空进行查询
                if (!queryParam["CustomerPO"].IsEmpty())
                {
                    //sql.Append($" AND CustomerPO = N'{queryParam["CustomerPO"]}'");
                    sql.Append($" AND CustomerPO like N'%{queryParam["CustomerPO"]}%'");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    //sql.Append($" AND ContainerNO = N'{queryParam["ContainerNO"]}'");
                    sql.Append($" AND ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                //工单状态 是否为空进行查询
                if (!queryParam["OrderStatus"].IsEmpty())
                {
                    //sql.Append($" AND OrderStatus = N'{queryParam["OrderStatus"]}'");
                    sql.Append($" AND OrderStatus like N'%{queryParam["OrderStatus"]}%'");
                }
                //PO号状态 是否为空进行查询
                if (!queryParam["POStatus"].IsEmpty())
                {
                    //sql.Append($" AND POStatus = N'{queryParam["POStatus"]}'");
                    sql.Append($" AND POStatus like N'%{queryParam["POStatus"]}%'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //下单总片数 是否为空进行查询
                if (!queryParam["OrderPieces"].IsEmpty())
                {
                    //sql.Append($" AND OrderPieces = N'{queryParam["OrderPieces"]}'");
                    sql.Append($" AND OrderPieces like N'%{queryParam["OrderPieces"]}%'");
                }
                //下单总盒数 是否为空进行查询
                if (!queryParam["OrderBox"].IsEmpty())
                {
                    //sql.Append($" AND OrderBox = N'{queryParam["OrderBox"]}'");
                    sql.Append($" AND OrderBox like N'%{queryParam["OrderBox"]}%'");
                }
                //下单总托数 是否为空进行查询
                if (!queryParam["OrderPallet"].IsEmpty())
                {
                    //sql.Append($" AND OrderPallet = N'{queryParam["OrderPallet"]}'");
                    sql.Append($" AND OrderPallet like N'%{queryParam["OrderPallet"]}%'");
                }
                //下单起始托号 是否为空进行查询
                if (!queryParam["OrderStartPallet"].IsEmpty())
                {
                    //sql.Append($" AND OrderStartPallet = N'{queryParam["OrderStartPallet"]}'");
                    sql.Append($" AND OrderStartPallet like N'%{queryParam["OrderStartPallet"]}%'");
                }
                //发货总片数 是否为空进行查询
                if (!queryParam["DeliveryPieces"].IsEmpty())
                {
                    //sql.Append($" AND DeliveryPieces = N'{queryParam["DeliveryPieces"]}'");
                    sql.Append($" AND DeliveryPieces like N'%{queryParam["DeliveryPieces"]}%'");
                }
                //发货总盒数 是否为空进行查询
                if (!queryParam["DeliveryBox"].IsEmpty())
                {
                    //sql.Append($" AND DeliveryBox = N'{queryParam["DeliveryBox"]}'");
                    sql.Append($" AND DeliveryBox like N'%{queryParam["DeliveryBox"]}%'");
                }
                //发货总托数 是否为空进行查询
                if (!queryParam["DeliveryPallet"].IsEmpty())
                {
                    //sql.Append($" AND DeliveryPallet = N'{queryParam["DeliveryPallet"]}'");
                    sql.Append($" AND DeliveryPallet like N'%{queryParam["DeliveryPallet"]}%'");
                }
                //发货起始托号 是否为空进行查询
                if (!queryParam["DeliveryStartPallet"].IsEmpty())
                {
                    //sql.Append($" AND DeliveryStartPallet = N'{queryParam["DeliveryStartPallet"]}'");
                    sql.Append($" AND DeliveryStartPallet like N'%{queryParam["DeliveryStartPallet"]}%'");
                }
                //总张数 是否为空进行查询
                if (!queryParam["TotalSheets"].IsEmpty())
                {
                    //sql.Append($" AND TotalSheets = N'{queryParam["TotalSheets"]}'");
                    sql.Append($" AND TotalSheets like N'%{queryParam["TotalSheets"]}%'");
                }
                //良率 是否为空进行查询
                if (!queryParam["Yield"].IsEmpty())
                {
                    //sql.Append($" AND Yield = N'{queryParam["Yield"]}'");
                    sql.Append($" AND Yield like N'%{queryParam["Yield"]}%'");
                }
                //放量张数 是否为空进行查询
                if (!queryParam["ActualSheets"].IsEmpty())
                {
                    //sql.Append($" AND ActualSheets = N'{queryParam["ActualSheets"]}'");
                    sql.Append($" AND ActualSheets like N'%{queryParam["ActualSheets"]}%'");
                }
                //工艺路线 是否为空进行查询
                if (!queryParam["Process"].IsEmpty())
                {
                    //sql.Append($" AND Process = N'{queryParam["Process"]}'");
                    sql.Append($" AND Process like N'%{queryParam["Process"]}%'");
                }
                //起始工序 是否为空进行查询
                if (!queryParam["StartOperation"].IsEmpty())
                {
                    //sql.Append($" AND StartOperation = N'{queryParam["StartOperation"]}'");
                    sql.Append($" AND StartOperation like N'%{queryParam["StartOperation"]}%'");
                }
                //流转方式 是否为空进行查询
                if (!queryParam["TransferBy"].IsEmpty())
                {
                    //sql.Append($" AND TransferBy = N'{queryParam["TransferBy"]}'");
                    sql.Append($" AND TransferBy like N'%{queryParam["TransferBy"]}%'");
                }
                //质量首检确认 是否为空进行查询
                if (!queryParam["FirstInspectionConfirm"].IsEmpty())
                {
                    //sql.Append($" AND FirstInspectionConfirm = N'{queryParam["FirstInspectionConfirm"]}'");
                    sql.Append($" AND FirstInspectionConfirm like N'%{queryParam["FirstInspectionConfirm"]}%'");
                }
                //免产标志 是否为空进行查询
                if (!queryParam["AvoidProduce"].IsEmpty())
                {
                    //sql.Append($" AND AvoidProduce = N'{queryParam["AvoidProduce"]}'");
                    sql.Append($" AND AvoidProduce like N'%{queryParam["AvoidProduce"]}%'");
                }
                //冻结标记 是否为空进行查询
                if (!queryParam["FreezeFlag"].IsEmpty())
                {
                    //sql.Append($" AND FreezeFlag = N'{queryParam["FreezeFlag"]}'");
                    sql.Append($" AND FreezeFlag like N'%{queryParam["FreezeFlag"]}%'");
                }
                //删除标记 是否为空进行查询
                if (!queryParam["IsEnabled"].IsEmpty())
                {
                    //sql.Append($" AND IsEnabled = N'{queryParam["IsEnabled"]}'");
                    sql.Append($" AND IsEnabled = N'{queryParam["IsEnabled"]}'");
                }
                //要料状态 是否为空进行查询
                if (!queryParam["DemandMaterial"].IsEmpty())
                {
                    //sql.Append($" AND DemandMaterial = N'{queryParam["DemandMaterial"]}'");
                    sql.Append($" AND DemandMaterial like N'%{queryParam["DemandMaterial"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建时间 是否为空进行查询
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    //sql.Append($" AND CreateTime = N'{queryParam["CreateTime"]}'");
                    sql.Append($" AND CreateTime like N'%{queryParam["CreateTime"]}%'");
                }
                //最后修改人 是否为空进行查询
                if (!queryParam["ModifyBy"].IsEmpty())
                {
                    //sql.Append($" AND ModifyBy = N'{queryParam["ModifyBy"]}'");
                    sql.Append($" AND ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                }
                //最后修改时间 是否为空进行查询
                if (!queryParam["ModifyTime"].IsEmpty())
                {
                    //sql.Append($" AND ModifyTime = N'{queryParam["ModifyTime"]}'");
                    sql.Append($" AND ModifyTime like N'%{queryParam["ModifyTime"]}%'");
                }
                //queryName(选择弹窗关键名称) 是否为空进行查询
                if (!queryParam["queryName"].IsEmpty())
                {
                    //sql.Append($" AND 关键名称 = '{queryParam["queryName"]}'");
                    //sql.Append($" AND 关键名称 like N'%{queryParam["queryName"]}%'");
                }
                //queryCode(选择弹窗关键编码) 是否为空进行查询
                if (!queryParam["queryCode"].IsEmpty())
                {
                    //sql.Append($" AND 关键编码 = N'{queryParam["queryCode"]}'");
                    //sql.Append($" AND 关键编码 like N'%{queryParam["queryCode"]}%'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindList(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindList(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable PL_WorkOrderProcessTable(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT b.*
                            FROM dbo.PL_WorkOrder a
                                INNER JOIN dbo.fn_GetMaterialAttrs() c ON a.WorkOrder=c.WorkOrder
                                LEFT JOIN dbo.BS_Process b
                                    ON a.FactoryCode = b.FactoryCode
                                       AND
                                       (
                                           a.UV = b.SmallClass
                                           OR a.Spec = b.SmallClass
                                           OR a.MMXH = b.SmallClass
                                           OR a.KCKX = b.SmallClass
			                               OR c.BWXH=b.SmallClass
                                       )
                            WHERE b.ProcessType = '2' ");

            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //工厂编码 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    sql.Append($" AND a.WorkOrder = N'{queryParam["WorkOrder"]}'");
                }

            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PW.FactoryCode,
                               PW.FactoryName,
                               PW.OrderPiecesAll,
                               PW.OrderPiecesNum,
                               PO.Id OrderId,
                               PW.ProductOrder,
                               PO.Customer,
                               PO.OrderType,
                               PO.ProductPlanNo,
                               PO.OrderDate,
                               PO.DeliveryDate,
                               PO.BoxDate,
                               PW.Id WorkOrderId,
                               PW.Id,
                               PW.WorkOrder,
                               PW.CustomerPO,
                               PW.PackingEndTime,
                               PW.ContainerNO,
                               PW.OrderStatus,
                               v2.ItemName OrderStatusName,
                               PW.POStatus,
                               v1.ItemName POStatusName,
                               PW.MaterialCode,
                               PW.OrderPieces,
                               PW.OrderBox,
                               PW.OrderPallet,
                               PW.OrderStartPallet,
                               PW.WorkOrderType,
                               v3.ItemName WorkOrderTypeName,
                               PW.OrderWholePallet,
                               PW.DeliveryWholePallet,
                               PW.DeliveryPieces,
                               PW.DeliveryBox,
                               PW.DeliveryPallet,
                               PW.DeliveryStartPallet,
                               PW.TotalSheets,
                               PW.Yield,
                               PW.ActualSheets,
                               PW.Process,
                               PW.StartOperation,
                               MW.ResourceName StartOperationName,
                               PW.TransferBy,
                               PW.FirstInspectionConfirm,
                               PW.FirstInspectionOperation,
                               PW.AvoidProduce,
                               PW.FreezeFlag,
                               PW.IsEnabled,
                               PW.DemandMaterial,
                               PW.Remark,
                               PW.GiveTime,
                               BP.ProcessName,
							   CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.MaterialName
                                   ELSE
                                       PW.MMXH
                               END AS MaterialName,
                               CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.Spec
                                   ELSE
                                       PW.Spec
                               END AS Spec,
                               PMA.MaterialClass,
                                CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.SmallClass
                                   ELSE
                                       PMA.WLXL
                               END AS SmallClass,
                               PMA.Unit,
                               PMA.Warehouse,
                               CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.MaterialName
                                   ELSE
                                       PW.MMXH
                               END AS MMXH,
                               PMA.MMCJ,
                               PMA.HD,
                               CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.UV
                                   ELSE
                                       PW.UV
                               END AS UV,
                               PMA.BWXH,
                               CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.KCKX
                                   ELSE
                                       PW.KCKX
                               END AS KCKX,
                               PMA.DXZH,
                               PW.CreateTime,
                               PW.Creator,
                               u.Name CreatorName,
                               PW.IsVC,
                               PW.BatchWorkOrder,
                               ISNULL(PW.BatchStatus,0) BatchStatus,
							   PW.SAP_AUFNR,
								pw.IsPosted,
								CASE pw.IsPosted WHEN '1' THEN '已同步' ELSE '' END SAPSync,
								pw.PostedMsg,
								pw.PostedTime,
								pw.PostedUser,
								pw.UPdate_IsPosted,
								CASE pw.Update_IsPosted WHEN '1' THEN '已同步' ELSE '' END Update_SAPSync,
								pw.Update_PostedMsg,
								pw.Update_PostedTime,
								pw.Update_PostedUser,
								pw.OrderClosed,
								v4.ItemName ShowOrderClosed,
								PW.CaseTme
                        FROM dbo.PL_WorkOrder PW  
	                        LEFT JOIN dbo.PL_ProductionOrder PO
	                            ON PW.ProductOrder = PO.ProductOrder
                            LEFT JOIN dbo.fn_GetMaterialAttrs() PMA
                                ON PW.WorkOrder = PMA.WorkOrder AND  pw.FactoryCode=pma.FactoryCode
                            LEFT JOIN dbo.PL_Process BP
                                ON BP.WorkOrder = PW.WorkOrder  AND pw.FactoryCode=BP.FactoryCode
                            LEFT JOIN dbo.BS_ModelWithResource MW
                                ON MW.ResourceCode = PW.StartOperation
                            LEFT JOIN dbo.BS_People u
                                ON PW.Creator = u.Code
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'PoStatus'
                                   AND PW.POStatus = v1.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'WorkOrderStatus'
                                   AND PW.OrderStatus = v2.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v3
                                ON v3.EnCode = 'WorkOrderType'
                                   AND PW.WorkOrderType = v3.ItemValue
							LEFT JOIN dbo.V_DataDictionary v4  ON v4.EnCode='OrderClosed' AND pw.OrderClosed=v4.ItemValue
                        WHERE 1 = 1 ");

            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND PW.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //订单号
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND PO.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //工单号 
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    sql.Append($" AND PW.WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //客户PO号 
                if (!queryParam["CustomerPO"].IsEmpty())
                {
                    sql.Append($" AND PW.CustomerPO like N'%{queryParam["CustomerPO"]}%'");
                }
                //柜号 
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND PW.ContainerNO = N'{queryParam["ContainerNO"]}'");
                }
                //工单类型
                if (!queryParam["WorkOrderType"].IsEmpty())
                {
                    sql.Append($" AND PW.WorkOrderType= N'{queryParam["WorkOrderType"]}'");
                }
                //工单状态
                if (!queryParam["OrderStatus"].IsEmpty())
                {
                    sql.Append($" AND PW.OrderStatus = N'{queryParam["OrderStatus"]}'");
                }
                //PO号状态
                if (!queryParam["POStatus"].IsEmpty())
                {
                    sql.Append($" AND PW.POStatus = N'{queryParam["POStatus"]}'");
                }
                //物料编码 
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND PW.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                if (!queryParam["StartPrepay"].IsEmpty())
                {
                    sql.Append($" AND PO.DeliveryDate >= N'{queryParam["StartPrepay"]}'");
                }
                if (!queryParam["EndPrepay"].IsEmpty())
                {
                    sql.Append($" AND PO.DeliveryDate <= N'{queryParam["EndPrepay"]}'");
                }
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    sql.Append($" AND  CONVERT(VARCHAR(10),PW.CreateTime,120)  >= N'{queryParam["CreateTime"]}'");
                }
                if (!queryParam["EndCreateTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10),PW.CreateTime,120) <= N'{queryParam["EndCreateTime"]}'");
                }
                if (!queryParam["BoxStart"].IsEmpty())
                {
                    sql.Append($" AND PO.BoxDate >= N'{queryParam["BoxStart"]}'");
                }
                if (!queryParam["BoxEnd"].IsEmpty())
                {
                    sql.Append($" AND PO.BoxDate <= N'{queryParam["BoxEnd"]}'");
                }
                if (!queryParam["LookWorkOrder"].IsEmpty())
                {
                    sql.Append($" AND PW.OrderStatus >= N'{queryParam["LookWorkOrder"]}'");
                }
                //规格型号
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND PMA.Spec like N'%{queryParam["Spec"]}%'");
                    sql.Append($" AND (PMA.Spec like N'%{queryParam["Spec"]}%' OR PW.Spec like N'%{queryParam["Spec"]}%') ");
                }
                //版纹型号
                if (!queryParam["BWXH"].IsEmpty())
                {
                    sql.Append($" AND PMA.BWXH like N'%{queryParam["BWXH"]}%'");
                }
                //UV
                if (!queryParam["UV"].IsEmpty())
                {
                    sql.Append($" AND PMA.KCKX like N'%{queryParam["UV"]}%'");
                }
                //面膜型号 查询物料名称
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND PMA.MaterialName like N'%{queryParam["MMXH"]}%'");
                    //sql.Append($" AND (PMA.MaterialName like N'%{queryParam["MaterialName"]}%' OR PW.MMXH like N'%{queryParam["MaterialName"]}%')");
                }
                //开槽扣型
                if (!queryParam["KCKX"].IsEmpty())
                {
                    sql.Append($" AND PMA.KCKX like N'%{queryParam["KCKX"]}%'");
                }
                //订单类型
                if (!queryParam["OrderType"].IsEmpty())
                {
                    sql.Append($" AND PO.OrderType='{queryParam["OrderType"]}'");
                }
                //交货日期
                if (!queryParam["GiveTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10),PW.GiveTime,120)='{queryParam["GiveTime"]}'");
                }
                //IsVC
                if (!queryParam["IsVC"].IsEmpty())
                {
                    sql.Append($" AND ISNULL(pw.IsVC,0)={queryParam["IsVC"]}");
                }
                if (!queryParam["BatchWorkOrder"].IsEmpty())
                {
                    sql.Append($" AND PW.BatchWorkOrder = '{queryParam["BatchWorkOrder"]}'");
                }
                //工单合批查询：工单状态
                if (!queryParam["queryCode1"].IsEmpty())
                {
                    sql.Append($" AND pw.OrderStatus IN('2','3')");
                }
                //工单合批查询：未合批、合批工单
                if (!queryParam["queryCode2"].IsEmpty())
                {
                    sql.Append($" AND (PW.BatchStatus IS NULL OR PW.BatchStatus IN(0,1))");
                }
                //工单合批查询：无免产标记
                if (!queryParam["queryCode3"].IsEmpty())
                {
                    sql.Append($" AND pw.AvoidProduce=0");
                }
                //订单关闭
                if (!queryParam["OrderClosed"].IsEmpty())
                {
                    sql.Append($" AND PW.OrderClosed = N'{queryParam["OrderClosed"]}'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        ///销售订单明细列表
        public DataTable GetProductOrderPageDataTableList(Pagination pagination, string queryJson, string userCode = "")
        {
            StringBuilder sql = new StringBuilder();

            sql.Append($@"SELECT PW.OrderPiecesAll,
                               PW.OrderPiecesNum,
                               PO.Id OrderId,
                               PO.ProductOrder,
                               PO.Customer,
                               PO.OrderType,
                               PO.ProductPlanNo,
                               PO.OrderDate,
                               PO.DeliveryDate,
                               PO.BoxDate,
                               PW.Id WorkOrderId,
                               PW.Id,
                               PW.FactoryCode,
		                       PW.FactoryName,
                               PW.WorkOrder,
                               PW.CustomerPO,
                               PW.PackingEndTime,
                               PW.ContainerNO,
                               PW.OrderStatus,
                               v2.ItemName OrderStatusName,
                               PW.POStatus,
                               v1.ItemName POStatusName,
                               PW.MaterialCode,
                               PW.OrderPieces,
                               PW.OrderBox,
                               PW.OrderPallet,
                               PW.OrderStartPallet,
                               PW.WorkOrderType,
                               v3.ItemName WorkOrderTypeName,
                               PW.OrderWholePallet,
                               PW.DeliveryWholePallet,
                               PW.DeliveryPieces,
                               PW.DeliveryBox,
                               PW.DeliveryPallet,
                               PW.DeliveryStartPallet,
                               PW.TotalSheets,
                               PW.Yield,
                               PW.ActualSheets,
                               PW.Process,
                               PW.StartOperation,
                               PW.TransferBy,
                               PW.FirstInspectionConfirm,
                               PW.FirstInspectionOperation,
                               PW.AvoidProduce,
                               PW.FreezeFlag,
                               PW.IsEnabled,
                               PW.DemandMaterial,
                               PW.Remark,
                               PW.GiveTime,
                               BP.ProcessName,
                               PMA.MaterialName,
                               case WHEN ISNULL(pw.IsVC,0)=0 THEN PMA.Spec ELSE pw.Spec END AS Spec,
                               PMA.MaterialClass,
                               PMA.SmallClass,
                               PMA.Unit,
                               PMA.Warehouse,
                              case WHEN ISNULL(pw.IsVC,0)=0 THEN PMA.MaterialName ELSE pw.MMXH END AS MMXH,
                               PMA.MMCJ,
                               PMA.HD,
							   case WHEN ISNULL(pw.IsVC,0)=0 THEN PMA.UV ELSE pw.UV END AS UV,
                               PMA.BWXH,
							   case WHEN ISNULL(pw.IsVC,0)=0 THEN PMA.KCKX ELSE pw.KCKX END AS KCKX,
                               PMA.DXZH,
                               K.KCPieceQty,
                               pw.IsVC,
                                pw.ReleaseName,
                                pw.ReleaseTime,
								pw.PackPalletNum,
								PMA.BarCode,
								PMA.BZDHSL+'片/盒,'+PMA.BZTPSL+'盒/托' AS  Packing,
								pw.Harbour,
								kk.ItemName HarbourName,
								PW.SAP_AUFNR,
								pw.IsPosted,
								CASE pw.IsPosted WHEN '1' THEN '已同步' ELSE '' END SAPSync,
								pw.PostedMsg,
								pw.PostedTime,
								pw.PostedUser,
								pw.OrderClosed,
								v4.ItemName ShowOrderClosed
                        FROM dbo.PL_ProductionOrder PO
                            INNER JOIN dbo.PL_WorkOrder PW
                                ON PW.ProductOrder = PO.ProductOrder
                            LEFT JOIN dbo.fn_GetMaterialAttrs() PMA
                                ON PW.WorkOrder = PMA.WorkOrder AND  pw.FactoryCode=pma.FactoryCode
                            LEFT JOIN dbo.PL_Process BP
                                ON BP.WorkOrder = PW.WorkOrder  AND pw.FactoryCode=BP.FactoryCode
                            LEFT JOIN
                            (
                                SELECT MaterialCode,
                                       SUM(PieceQty) KCPieceQty
                                FROM dbo.MM_ProductStock
                                GROUP BY MaterialCode
                            ) K
                                ON K.MaterialCode = PW.MaterialCode
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'PoStatus'
                                   AND PW.POStatus = v1.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'WorkOrderStatus'
                                   AND PW.OrderStatus = v2.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v3
                                ON v3.EnCode = 'WorkOrderType'
                                   AND PW.WorkOrderType = v3.ItemValue
							LEFT JOIN dbo.Base_KeyParameterItem kk ON kk.EnCode = 'PORTINFOS' AND pw.Harbour=kk.ItemCode
							LEFT JOIN dbo.V_DataDictionary v4  ON v4.EnCode='OrderClosed' AND pw.OrderClosed=v4.ItemValue
                        WHERE 1=1 AND pw.WorkOrderType NOT IN('2','3') ");

            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND PW.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND PO.ProductOrder = N'{queryParam["ProductOrder"]}'");
                }
                //工单号 
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    sql.Append($" AND PW.WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //客户PO号 
                if (!queryParam["CustomerPO"].IsEmpty())
                {
                    sql.Append($" AND PW.CustomerPO like N'%{queryParam["CustomerPO"]}%'");
                }
                //柜号 
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND PW.ContainerNO = N'{queryParam["ContainerNO"]}'");
                }
                if (!queryParam["WorkOrderType"].IsEmpty())
                {
                    sql.Append($" AND PW.WorkOrderType= N'{queryParam["WorkOrderType"]}'");
                }

                //工单状态
                if (!queryParam["OrderStatus"].IsEmpty())
                {
                    sql.Append($" AND PW.OrderStatus = N'{queryParam["OrderStatus"]}'");
                }
                //PO号状态
                if (!queryParam["POStatus"].IsEmpty())
                {
                    sql.Append($" AND PW.POStatus = N'{queryParam["POStatus"]}'");
                }
                //物料编码 
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND PW.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }

                if (!queryParam["StartPrepay"].IsEmpty())
                {
                    sql.Append($" AND PO.DeliveryDate >= N'{queryParam["StartPrepay"]}'");
                }
                if (!queryParam["EndPrepay"].IsEmpty())
                {
                    sql.Append($" AND PO.DeliveryDate <= N'{queryParam["EndPrepay"]}'");
                }
                if (!queryParam["BoxStart"].IsEmpty())
                {
                    sql.Append($" AND PO.BoxDate >= N'{queryParam["BoxStart"]}'");
                }
                if (!queryParam["BoxEnd"].IsEmpty())
                {
                    sql.Append($" AND PO.BoxDate <= N'{queryParam["BoxEnd"]}'");
                }
                if (!queryParam["LookWorkOrder"].IsEmpty())
                {
                    sql.Append($" AND PW.OrderStatus >= N'{queryParam["LookWorkOrder"]}'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND PMA.Spec like N'%{queryParam["Spec"]}%'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND PMA.Spec like N'%{queryParam["Spec"]}%'");
                }

                if (!queryParam["BWXH"].IsEmpty())
                {
                    sql.Append($" AND PMA.BWXH like N'%{queryParam["BWXH"]}%'");
                }
                if (!queryParam["UV"].IsEmpty())
                {
                    sql.Append($" AND PMA.KCKX like N'%{queryParam["UV"]}%'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND PMA.MaterialName like N'%{queryParam["MMXH"]}%'");
                }
                if (!queryParam["KCKX"].IsEmpty())
                {
                    sql.Append($" AND PMA.KCKX like N'%{queryParam["KCKX"]}%'");
                }
                //库存片数
                if (!queryParam["LowerLimit"].IsEmpty())
                {
                    sql.Append($" AND K.KCPieceQty >= {queryParam["LowerLimit"]}");
                }
                if (!queryParam["UpperLimit"].IsEmpty())
                {
                    sql.Append($" AND K.KCPieceQty <= {queryParam["UpperLimit"]}");
                }
                //冻结标记
                if (!queryParam["FreezeFlag"].IsEmpty())
                {
                    sql.Append($" AND pw.FreezeFlag = N'{queryParam["FreezeFlag"]}'");
                }
                //订单关闭
                if (!queryParam["OrderClosed"].IsEmpty())
                {
                    sql.Append($" AND PW.OrderClosed = N'{queryParam["OrderClosed"]}'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_WorkOrderEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FactoryCode]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[CustomerPO]
                      ,[ContainerNO]
                      ,[OrderStatus]
                      ,[POStatus]
                      ,[MaterialCode]
                      ,[OrderPieces]
                      ,[OrderBox]
                      ,[OrderPallet]
                      ,[OrderStartPallet]
                      ,[DeliveryPieces]
                      ,[DeliveryBox]
                      ,[DeliveryPallet]
                      ,[DeliveryStartPallet]
                      ,[TotalSheets]
                      ,[Yield]
                      ,[ActualSheets]
                      ,[Process]
                      ,[StartOperation]
                      ,[TransferBy]
                      ,[FirstInspectionConfirm]
                      ,[AvoidProduce]
                      ,[FreezeFlag]
                      ,[IsEnabled]
                      ,[DemandMaterial]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_WorkOrder] where 1=1  ");
            if (!checkType.IsEmpty())
            {
                //sql.Append($@" and Id = N'{checkType}' ");
            }
            msg = "";
            try
            {
                return this.BaseRepository().FindList(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PL_WorkOrderEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    n = this.BaseRepository().Update(entity);
                }
                else
                {
                    if (string.IsNullOrEmpty(entity.Id))
                    {
                        entity.Create();
                    }
                    n = this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return n;
        }


        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PL_WorkOrderEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_WorkOrderEntity> entity_list, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (IsUpdate)
                {
                    //n = this.BaseRepository().Update(entity_list);
                    StringBuilder sql = new StringBuilder();
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            StringBuilder sql_temp = new StringBuilder();
                            sql_temp.Append("UPDATE [dbo].[PL_WorkOrder] set ");
                            string keyValue = "";
                            //循环实体
                            Save_obj.GetType().GetProperties().ToList().ForEach(x =>
                            {
                                if (x.Name == "Id")
                                {
                                    keyValue = x.GetValue(Save_obj, null).ToString();
                                }
                                else
                                {
                                    if (x.Name == "IsEnabled")
                                    {
                                        if (x.GetValue(Save_obj, null) != null)
                                        {

                                            sql_temp.Append(x.Name + "=" + (x.GetValue(Save_obj, null) == null ? 0 : (x.GetValue(Save_obj, null).ToString() == "True" ? 1 : 0)) + ",");
                                        }
                                    }
                                    else
                                    {
                                        var hasNotMapped = Attribute.IsDefined(x, typeof(NotMappedAttribute));
                                        if (!hasNotMapped)
                                        {
                                            if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                            {
                                                sql_temp.Append(x.Name + "=N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "',");
                                            }
                                        }
                                    }
                                }

                            });
                            sql.Append(sql_temp.ToString().TrimEnd(',') + $" WHERE Id='{keyValue}';");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    n = this.BaseRepository().Insert(entity_list);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            int n = 0;
            msg = "";
            try
            {
                //删除
                n = this.BaseRepository().Delete(keyValue);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return this.BaseRepository().Delete(keyValue);
        }
        public int RemoveForm(Expression<Func<PL_WorkOrderEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        public int Delete(List<PL_WorkOrderEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }
        /// <summary>
        /// 导出补料单EXCEL 
        /// </summary>
        /// <param name="checkType"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string WorkOrder_export(List<PL_WorkOrderEntity> entiy, out string msg)
        {
            var workorders = "";
            msg = "成功!";
            foreach (var item in entiy)
            {
                workorders = workorders + "," + "'" + item.WorkOrder + "'";
            }
            workorders = workorders.Substring(1, workorders.Length - 1);
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT pw.ProductOrder AS '销售订单',
                               pw.MaterialCode AS '客户型号',
                               CASE
                                   WHEN ISNULL(pw.MMXH, '') = '' THEN
                                       PMA.MaterialName
                                   ELSE
                                       pw.MMXH
                               END AS '面膜型号',
                               TotalSheets AS '大张数量',
                               OrderPieces AS '小片数量',
                               CASE
                                   WHEN ISNULL(pw.Spec, '') = '' THEN
                                       PMA.Spec
                                   ELSE
                                       pw.Spec
                               END AS '规格',
                               a.SumOrderPieces '下单片数',
                               pw.Remark AS '备注'
                        FROM PL_WorkOrder pw
                            LEFT JOIN dbo.fn_GetMaterialAttrs() PMA
                                ON pw.WorkOrder = PMA.WorkOrder
                                   AND pw.FactoryCode = PMA.FactoryCode
                            LEFT JOIN
                            (
                                SELECT a1.ProductOrder,
                                       a1.MaterialCode,
                                       SUM(a1.OrderPieces) SumOrderPieces
                                FROM dbo.PL_WorkOrder a1
                                WHERE a1.WorkOrderType = '1'
                                      AND a1.OrderStatus <= 6
                                GROUP BY a1.ProductOrder,
                                         a1.MaterialCode
                            ) a
                                ON pw.ProductOrder = a.ProductOrder
                                   AND pw.MaterialCode = a.MaterialCode
                        WHERE pw.WorkOrder IN ({workorders});");
            try
            {
                DataTable dt = this.BaseRepository().FindTable(sql.ToString());
                var virtualPath = "~/";
                var dirPath = "Upload/";
                string folder = DateTime.Now.ToString("yyyyMM") + "/";
                //文件全路径
                var fullDirPath = System.Web.HttpContext.Current.Server.MapPath(virtualPath + dirPath + folder);

                string sServerDir = fullDirPath;
                if (!Directory.Exists(sServerDir))
                {
                    Directory.CreateDirectory(sServerDir);
                }
                string saveFileName = "补料单" + "_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcelWorkOrder(dt, true);
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();
                return $@"{dirPath}{folder}{saveFileName}";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int Delete_SQL(string keyValue, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@"DELETE FROM [dbo].[PL_WorkOrder] WHERE Id=N'{keyValue}'");
                n = this.BaseRepository().ExecuteBySql(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PL_WorkOrderEntity</returns>
        public PL_WorkOrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PL_WorkOrderEntity</returns>
        public PL_WorkOrderEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PL_WorkOrderEntity 列表</returns>
        public IEnumerable<PL_WorkOrderEntity> Get_ExpressionList(Expression<Func<PL_WorkOrderEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition);
            //调用示例 var data = _Service.Get_ExpressionList(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false).OrderByDescending(t => t.PlanProNo).ToList();
        }

        ///// <summary>
        ///// 删除主表数据并同步删除子表数据, 假删除更新删除标记
        ///// </summary>
        ///// <param name="keyValue"></param>
        ///// <returns></returns>
        //public int RemoveForm(string keyValue)
        //{
        //    int result = 0;
        //    StringBuilder sql = new StringBuilder();
        //    //子表服务类
        //    RepositoryFactory<PL_WorkOrderEntity> bomService = new RepositoryFactory<PL_WorkOrderEntity>();

        //    PL_WorkOrderEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PL_WorkOrderDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PL_WorkOrder_Id == entity.Id).FirstOrDefault();
        //    if (entity != null)
        //    {
        //        //主表删除标记
        //        entity.IsEnabled = false;
        //        this.BaseRepository().Update(entity);
        //        if (bomEntity != null)
        //        {
        //            //子表删除标记
        //            bomEntity.IsEnabled = false;
        //            bomService.BaseRepository().Update(bomEntity);
        //        }
        //        result = 1;
        //    }

        //    return result;
        //}

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_WorkOrderEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT getdate() as CreatedDateTime ");
            if (string.IsNullOrEmpty(checkType) == false)
            {
                //sql.Append($@" and ID = '{checkType}'";
            }
            msg = "";
            try
            {
                //执行 
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                //实体映射查询
                IEnumerable<PL_WorkOrderEntity> PL_WorkOrderEntity_list = db2.FindList<PL_WorkOrderEntity>(sql.ToString());
                return PL_WorkOrderEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetDataTable_TestOtherEntity(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT getdate() as CreatedDateTime ");
            if (string.IsNullOrEmpty(checkType) == false)
            {
                //sql.Append($@" and ID = '{checkType}'";
            }
            msg = "";
            try
            {
                //执行 
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                //实体映射查询
                DataTable PL_WorkOrderEntity_DataTable = db2.FindTable(sql.ToString());
                return PL_WorkOrderEntity_DataTable;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据单据类型获取流水号 存储过程调用示例
        /// </summary>
        /// <param name="SeqCode">规则代码</param>
        /// <param name="returnNum">返回的流水号</param>
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
        public bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode)
        {
            bool b = false;
            returnNum = "";
            messageCode = "";
            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@SeqCode", SqlDbType.VarChar,60),
                new SqlParameter("@ReturnNum", SqlDbType.VarChar,40),
                new SqlParameter("@MessageCode", SqlDbType.VarChar,800)
            };
            parameters[0].Value = SeqCode;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.Output;

            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("P_GetSerialNO", parameters);
                //返回参数值
                returnNum = parameters[1].Value.ToString();
                messageCode = parameters[2].Value.ToString();
                b = true;
            }
            catch (Exception ex)
            {
                messageCode = ex.Message;
            }
            return b;
        }

        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [FactoryCode] as '工厂编码'
                      ,[ProductOrder] as '订单编码'
                      ,[WorkOrder] as '工单号'
                      ,[CustomerPO] as '客户PO号'
                      ,[ContainerNO] as '柜号'
                      ,[OrderStatus] as '工单状态'
                      ,[POStatus] as 'PO号状态'
                      ,[MaterialCode] as '物料编码'
                      ,[OrderPieces] as '下单总片数'
                      ,[OrderBox] as '下单总盒数'
                      ,[OrderPallet] as '下单总托数'
                      ,[OrderStartPallet] as '下单起始托号'
                      ,[DeliveryPieces] as '发货总片数'
                      ,[DeliveryBox] as '发货总盒数'
                      ,[DeliveryPallet] as '发货总托数'
                      ,[DeliveryStartPallet] as '发货起始托号'
                      ,[TotalSheets] as '总张数'
                      ,[Yield] as '良率'
                      ,[ActualSheets] as '放量张数'
                      ,[Process] as '工艺路线'
                      ,[StartOperation] as '起始工序'
                      ,[TransferBy] as '流转方式'
                      ,[FirstInspectionConfirm] as '质量首检确认'
                      ,[AvoidProduce] as '免产标志'
                      ,[FreezeFlag] as '冻结标记'
                      ,[IsEnabled] as '删除标记'
                      ,[DemandMaterial] as '要料状态'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[PL_WorkOrder] where 1=1  ");
            msg = "成功!";
            if (!checkType.IsEmpty())
            {
                //此处换上你的关键查询条件 也可以为空 查询全部
                sql.Append($@" and CreatedByCode = '{checkType}' ");
            }
            try
            {
                DataTable dt = this.BaseRepository().FindTable(sql.ToString());
                var virtualPath = "~/";
                var dirPath = "Upload/";
                string folder = DateTime.Now.ToString("yyyyMM") + "/";
                //文件全路径
                var fullDirPath = System.Web.HttpContext.Current.Server.MapPath(virtualPath + dirPath + folder);

                string sServerDir = fullDirPath;
                if (!Directory.Exists(sServerDir))
                {
                    Directory.CreateDirectory(sServerDir);
                }
                string saveFileName = "生产工单表_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("生产工单表", dt, true);
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();
                return $@"{dirPath}{folder}{saveFileName}";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        public PL_WorkOrderEntity Get_ExpressionEntity(Expression<Func<PL_WorkOrderEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        /// <summary>
        /// VC工单调拨接收生成工艺路线、BOM
        /// 创建：jpf 2022-12-20
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="listPrd"></param>
        /// <returns></returns>
        public string Save_TransferVCWorkProcessBom(string Creator, List<PL_WorkOrderEntity> listPrd)
        {
            var msg = "";
            DataTable dt = VCWorkOrderToDataTable(listPrd);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkOrders", dt),
                    new SqlParameter("@Creator",SqlDbType.VarChar, 50),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };
            parameters[1].Value = Creator;

            parameters[2].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("PL_VCTfWorkOrderProcessBOM", parameters);
                var Resultmsg = parameters[2].Value;
                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                {
                    return @Resultmsg.ToString();
                }
                return msg;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("工单调拨接收生成失败");
                }
            }
            return msg;
        }

        /// <summary>
        /// 工单调拨接收生成工艺路线、BOM
        /// 创建：jpf 2022-12-19 
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="listPrd"></param>
        /// <returns></returns>
        public string Save_TransferWorkProcessBom(string Creator, List<PL_WorkOrderEntity> listPrd)
        {
            var msg = "";
            DataTable dt = TFWorkOrderToDataTable(listPrd);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkOrders", dt),
                    new SqlParameter("@Creator",SqlDbType.VarChar, 50),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };
            parameters[1].Value = Creator;

            parameters[2].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("PL_TfWorkOrderProcessBOM", parameters);
                var Resultmsg = parameters[2].Value;
                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                {
                    return @Resultmsg.ToString();
                }
                return msg;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("工单调拨接收生成失败");
                }
            }
            return msg;
        }
        /// <summary>
        /// 将List转换为DataTable
        /// 创建：jpf 2022-11-21
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        private DataTable TFWorkOrderToDataTable(List<PL_WorkOrderEntity> list)
        {
            if (list == null || list.Count == 0) return null;
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("Id", System.Type.GetType("System.String"));
            dt.Columns.Add("FactoryCode", System.Type.GetType("System.String"));
            dt.Columns.Add("FactoryName", System.Type.GetType("System.String"));
            dt.Columns.Add("ProductOrder", System.Type.GetType("System.String"));
            dt.Columns.Add("CustomerPO", System.Type.GetType("System.String"));
            dt.Columns.Add("ContainerNO", System.Type.GetType("System.String"));

            dt.Columns.Add("MaterialCode", System.Type.GetType("System.String"));
            dt.Columns.Add("OrderPieces", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderBox", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderPallet", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderWholePallet", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderStartPallet", System.Type.GetType("System.Decimal"));

            dt.Columns.Add("AvoidProduce", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Creator", System.Type.GetType("System.String"));
            dt.Columns.Add("GiveTime", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Remark", System.Type.GetType("System.String"));
            dt.Columns.Add("MMXH", System.Type.GetType("System.String"));
            dt.Columns.Add("Spec", System.Type.GetType("System.String"));
            dt.Columns.Add("UV", System.Type.GetType("System.String"));
            dt.Columns.Add("KCKX", System.Type.GetType("System.String"));
            dt.Columns.Add("WorkOrder", System.Type.GetType("System.String"));


            foreach (PL_WorkOrderEntity item in list)
            {
                if (item.MMXH == null)
                {
                    item.MMXH = "";
                }
                if (item.Spec == null)
                {
                    item.Spec = "";
                }
                if (item.UV == null)
                {
                    item.UV = "";
                }
                if (item.KCKX == null)
                {
                    item.KCKX = "";
                }
            }

            foreach (PL_WorkOrderEntity item in list)
            {
                dt.Rows.Add(Guid.NewGuid().ToString(), item.FactoryCode, item.FactoryName, item.ProductOrder, item.CustomerPO, item.ContainerNO, item.MaterialCode
                    , item.OrderPieces, item.OrderBox, item.OrderPallet, item.OrderWholePallet, item.OrderStartPallet, item.AvoidProduce, item.Creator, item.GiveTime, item.Remark,
                    item.MMXH.Trim(), item.Spec.Trim()
                    , item.UV.Trim(), item.KCKX.Trim(), item.WorkOrder

                    );
            }
            return dt;
        }
        /// <summary>
        /// 修改VC工单工艺路线
        /// 创建 jpf 2022-11-29
        /// </summary>
        /// <returns></returns>
        public string Save_VCworkProcess(string Creator, string workorder, List<BS_ProcessEntity> listPrd)
        {
            var msg = "";
            DataTable dt = VCProcessToDataTable(listPrd);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@Process", dt),
                    new SqlParameter("@Creator",SqlDbType.VarChar, 50),
                    new SqlParameter("@workorder",SqlDbType.VarChar, 50),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };
            parameters[1].Value = listPrd[0].Creator;
            parameters[2].Value = workorder;
            parameters[3].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("P_VCSaveProcess", parameters);
                var Resultmsg = parameters[3].Value;
                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                {
                    return @Resultmsg.ToString();
                }
                return msg;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("工艺路线生成失败");
                }
            }
            return msg;

        }
        /// <summary>
        /// 将List转换为DataTable
        /// 创建：jpf 2022-11-21
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        private DataTable VCProcessToDataTable(List<BS_ProcessEntity> list)
        {
            if (list == null || list.Count == 0) return null;
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("Id", System.Type.GetType("System.String"));
            dt.Columns.Add("FactoryCode", System.Type.GetType("System.String"));
            dt.Columns.Add("FactoryName", System.Type.GetType("System.String"));
            dt.Columns.Add("ProcessCode", System.Type.GetType("System.String"));
            dt.Columns.Add("ProcessName", System.Type.GetType("System.String"));
            dt.Columns.Add("MaterialClass", System.Type.GetType("System.String"));

            dt.Columns.Add("SmallClass", System.Type.GetType("System.String"));
            dt.Columns.Add("ProcessType", System.Type.GetType("System.String"));
            dt.Columns.Add("IsDefault", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Remark", System.Type.GetType("System.String"));




            foreach (BS_ProcessEntity item in list)
            {
                dt.Rows.Add(Guid.NewGuid().ToString(), item.FactoryCode, item.FactoryName, item.ProcessCode, item.ProcessName, item.MaterialClass,
                    item.SmallClass, item.ProcessType, item.IsDefault, item.Remark
                    );
            }
            return dt;
        }
        /// <summary>
        /// 导入内销工单
        /// 创建：jpf 2022-11-18 add
        /// </summary>
        /// <returns></returns>
        public string VCWorkOrderImport(List<PL_WorkOrderEntity> listPrd, string isAdd)
        {
            var msg = "";
            DataTable dt = VCWorkOrderToDataTable(listPrd);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkOrders", dt),
                    new SqlParameter("@Creator",SqlDbType.VarChar, 50),
                    new SqlParameter("@IsAdd",SqlDbType.VarChar, 50),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };
            parameters[1].Value = listPrd[0].Creator;
            parameters[2].Value = isAdd;
            parameters[3].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("P_VCWorkOrderImport", parameters);
                var Resultmsg = parameters[3].Value;
                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                {
                    return @Resultmsg.ToString();
                }
                return msg;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("生产订单导入失败");
                }
            }
            return msg;
        }

        /// <summary>
        /// 将List转换为DataTable
        /// 创建：jpf 2022-11-21
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        private DataTable VCWorkOrderToDataTable(List<PL_WorkOrderEntity> list)
        {
            if (list == null || list.Count == 0) return null;
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("Id", System.Type.GetType("System.String"));
            dt.Columns.Add("FactoryCode", System.Type.GetType("System.String"));
            dt.Columns.Add("FactoryName", System.Type.GetType("System.String"));
            dt.Columns.Add("ProductOrder", System.Type.GetType("System.String"));
            dt.Columns.Add("CustomerPO", System.Type.GetType("System.String"));
            dt.Columns.Add("ContainerNO", System.Type.GetType("System.String"));

            dt.Columns.Add("MaterialCode", System.Type.GetType("System.String"));
            dt.Columns.Add("OrderPieces", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderBox", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderPallet", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderWholePallet", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderStartPallet", System.Type.GetType("System.Decimal"));

            dt.Columns.Add("AvoidProduce", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Creator", System.Type.GetType("System.String"));
            dt.Columns.Add("GiveTime", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Remark", System.Type.GetType("System.String"));
            dt.Columns.Add("PackPalletNum", System.Type.GetType("System.String"));
            dt.Columns.Add("MMXH", System.Type.GetType("System.String"));
            dt.Columns.Add("Spec", System.Type.GetType("System.String"));
            dt.Columns.Add("UV", System.Type.GetType("System.String"));
            dt.Columns.Add("KCKX", System.Type.GetType("System.String"));
            dt.Columns.Add("BW", System.Type.GetType("System.String"));

            foreach (PL_WorkOrderEntity item in list)
            {
                if (item.MMXH == null)
                {
                    item.MMXH = "";
                }
                if (item.Spec == null)
                {
                    item.Spec = "";
                }
                if (item.UV == null)
                {
                    item.UV = "";
                }
                if (item.KCKX == null)
                {
                    item.KCKX = "";
                }
                if (item.BW == null)
                {
                    item.BW = "";
                }
            }

            foreach (PL_WorkOrderEntity item in list)
            {
                dt.Rows.Add(Guid.NewGuid().ToString(), item.FactoryCode, item.FactoryName, item.ProductOrder, item.CustomerPO,
                    item.ContainerNO, item.MaterialCode, item.OrderPieces, item.OrderBox, item.OrderPallet, item.OrderWholePallet,
                    item.OrderStartPallet, item.AvoidProduce, item.Creator, item.GiveTime, item.Remark, item.PackPalletNum, item.MMXH.Trim(),
                    item.Spec.Trim(), item.UV.Trim(), item.KCKX.Trim(), item.BW.Trim()
                    );
            }
            return dt;
        }
        public string WorkOrderImport(List<PL_WorkOrderEntity> listPrd, string isAdd)
        {
            var msg = "";
            DataTable dt = WorkOrderToDataTable(listPrd);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkOrders", dt),
                    new SqlParameter("@Creator",SqlDbType.VarChar, 50),
                    new SqlParameter("@IsAdd",SqlDbType.VarChar, 50),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };
            parameters[1].Value = listPrd[0].Creator;
            parameters[2].Value = isAdd;
            parameters[3].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("P_WorkOrderImport", parameters);
                var Resultmsg = parameters[3].Value;
                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                {
                    return @Resultmsg.ToString();
                }
                return msg;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("生产订单导入失败");
                }
            }
            return msg;
        }


        /// <summary>
        /// 将List转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        private DataTable WorkOrderToDataTable(List<PL_WorkOrderEntity> list)
        {
            if (list == null || list.Count == 0) return null;
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("Id", System.Type.GetType("System.String"));
            dt.Columns.Add("FactoryCode", System.Type.GetType("System.String"));
            dt.Columns.Add("FactoryName", System.Type.GetType("System.String"));
            dt.Columns.Add("ProductOrder", System.Type.GetType("System.String"));
            dt.Columns.Add("CustomerPO", System.Type.GetType("System.String"));
            dt.Columns.Add("ContainerNO", System.Type.GetType("System.String"));

            dt.Columns.Add("MaterialCode", System.Type.GetType("System.String"));
            dt.Columns.Add("OrderPieces", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderBox", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderPallet", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderWholePallet", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderStartPallet", System.Type.GetType("System.Decimal"));

            dt.Columns.Add("AvoidProduce", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Creator", System.Type.GetType("System.String"));
            dt.Columns.Add("GiveTime", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Remark", System.Type.GetType("System.String"));
            dt.Columns.Add("PackPalletNum", System.Type.GetType("System.String"));
            dt.Columns.Add("Harbour", System.Type.GetType("System.String"));//港口
            dt.Columns.Add("BoxDate", System.Type.GetType("System.DateTime"));//纸盒日期

            foreach (PL_WorkOrderEntity item in list)
            {
                dt.Rows.Add(Guid.NewGuid().ToString(), item.FactoryCode, item.FactoryName, item.ProductOrder, item.CustomerPO,
                    item.ContainerNO, item.MaterialCode, item.OrderPieces, item.OrderBox, item.OrderPallet, item.OrderWholePallet,
                    item.OrderStartPallet, item.AvoidProduce, item.Creator, item.GiveTime, item.Remark, item.PackPalletNum, item.Harbour,
                    item.BoxDate);
            }
            return dt;
        }
        /// <summary>
        /// jpf 新增批量更新
        /// 2022-12-28 
        /// </summary>
        /// <param name="lstEntit"></param>
        /// <returns></returns>
        //public int UpdateListNew(List<PL_WorkOrderEntity> lstEntity)
        //{

        //}

        public int UpdateList(List<PL_WorkOrderEntity> lstEntity)
        {
            return this.BaseRepository().Update(lstEntity);
        }
        /// <summary>
        /// 获取良率
        /// </summary>
        /// <param name="ProductOrder"></param>
        /// <returns></returns>
        public List<dynamic> GetWorkOrderYield(string ProductOrder)
        {

            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@ProductOrder", SqlDbType.VarChar,60),
            };
            parameters[0].Value = ProductOrder;
            //parameters[1].Direction = ParameterDirection.Output;
            //parameters[2].Direction = ParameterDirection.Output;
            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                var dt = db2.ExecuteProc_Table("P_GetWorkOrderYield", parameters);
                var modelList = new List<dynamic>();
                foreach (DataRow row in dt.Rows)
                {
                    dynamic model = new ExpandoObject();
                    var dict = (IDictionary<string, object>)model;
                    foreach (DataColumn column in dt.Columns)
                    {
                        dict[column.ColumnName] = row[column];
                    }
                    modelList.Add(model);
                }

                return modelList;

            }
            catch (SqlException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
        /// <summary>
        /// 按照订单张数还是生产张数计算发料
        /// </summary>
        /// <param name="ProductOrder"></param>
        /// <returns></returns>
        public List<dynamic> GetSplitIsOrderOrProduct(string ProductOrder)
        {

            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@ProductOrder", SqlDbType.VarChar,60),
            };
            parameters[0].Value = ProductOrder;
            //parameters[1].Direction = ParameterDirection.Output;
            //parameters[2].Direction = ParameterDirection.Output;
            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                var dt = db2.ExecuteProc_Table("P_GetSplitIsOrderOrProduct", parameters);
                var modelList = new List<dynamic>();
                foreach (DataRow row in dt.Rows)
                {
                    dynamic model = new ExpandoObject();
                    var dict = (IDictionary<string, object>)model;
                    foreach (DataColumn column in dt.Columns)
                    {
                        dict[column.ColumnName] = row[column];
                    }
                    modelList.Add(model);
                }

                return modelList;

            }
            catch (SqlException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
        /// <summary>
        /// 查询工单的单(面膜单耗,耐磨层单耗)
        /// </summary>
        /// <param name="productOrder"></param>
        /// <returns></returns>
        public List<dynamic> GetWorkOrderMaterialPieceConsume(string productOrder)
        {
            var sql = $@"SELECT PW.WorkOrder,
                               CAST(bb.Num / bo.UnitNum AS DECIMAL(10, 5)) DanHao,
                               PMA.SmallClass,
                               'Mark' TypeName
                        FROM dbo.PL_WorkOrder PW
                            LEFT JOIN [dbo].[fn_GetMaterialAttrs]() PMA
                                ON PMA.WorkOrder = PW.WorkOrder
                                   AND PW.MaterialCode = PMA.MaterialCode
                            LEFT JOIN dbo.PL_BOM bo
                                ON bo.IsDeleted = 0
                                   AND PW.WorkOrder = bo.WorkOrder
                            LEFT JOIN dbo.PL_BOMItems bb
                                ON bo.Id = bb.BOMId
                         -- AND PMA.MMXH = bb.MaterialCode
                                   AND bb.SmallClass = 'MM'
                        WHERE PW.ProductOrder = '{productOrder}'
                        UNION ALL
                        SELECT PW.WorkOrder,
                               CAST(PBI.Num / PB.UnitNum AS DECIMAL(10, 5)) DanHao,
                               PBI.SmallClass,
                               'WearLayer' TypeName
                        FROM dbo.PL_WorkOrder PW
                            LEFT JOIN dbo.PL_BOM PB
                                ON PB.IsDeleted = 0
                                   AND PW.WorkOrder = PB.WorkOrder
                            LEFT JOIN dbo.PL_BOMItems PBI
                                ON PB.Id = PBI.BOMId
                                   AND PBI.SmallClass = 'NMC'
                        WHERE PW.ProductOrder = '{productOrder}' ";

            return this.BaseRepository().Query(sql);
        }

        #region 工单审核
        /// <summary>
        /// 工单审核更改工单状态
        /// </summary>
        /// <param name="productOrder"></param>
        /// <param name="status"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateWorkOrderStatus(string productOrder, string status, string userCode)
        {
            var sql = $@"UPDATE dbo.PL_WorkOrder SET OrderStatus='{status}',ModifyBy='{userCode}',ModifyTime=GETDATE() WHERE ProductOrder='{productOrder}'";
            return this.BaseRepository().ExecuteBySql(sql);
        }
        #endregion

        #region PDA方法
        /// <summary>
        /// 工单详情
        /// </summary>
        /// <param name="workOrder">工单号</param>
        /// <returns></returns>
        public DataTable GetWorkOrderInfoToPDA(string workOrder)
        {
            string sql = $@"SELECT a.ContainerNO,
                               a.CustomerPO,
                               a.WorkOrderType,
                               v1.ItemName WorkOrderTypeName,
                               a.MaterialCode,
                               b.MMXH,
                               b.Spec
                        FROM dbo.PL_WorkOrder a
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'WorkOrderType'
                                   AND a.WorkOrderType = v1.ItemValue
                            LEFT JOIN dbo.fn_GetMaterialAttrs() b
                                ON a.WorkOrder = b.WorkOrder
                        WHERE a.IsEnabled = 1
                              AND a.WorkOrder = '{workOrder}'";
            return this.BaseRepository().FindTable(sql);
        }

        #endregion

        #region 创建发货单查询
        /// <summary>
        /// 创建发货单查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetWorkOrderDataTable(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PW.FactoryCode,PW.FactoryName,PO.ProductOrder,
                               PW.CustomerPO,
                               PW.ContainerNO,
                               PW.DeliveryPallet,
							   pw.DeliveryBox,
                               PW.AvoidProduce,
                               100 GrossWeight,
							   PW.WorkOrder,
							   pw.DeliveryPieces
                        FROM dbo.PL_ProductionOrder PO
                            INNER JOIN
                            (
                                SELECT a.FactoryCode,
								       a.FactoryName,
									   a.ProductOrder,
                                       a.ContainerNO,
                                       a.CustomerPO,
                                       a.AvoidProduce,
									   a.WorkOrder,
                                       SUM(a.DeliveryPallet) DeliveryPallet,
									   SUM(a.DeliveryBox) DeliveryBox,
									   SUM(a.DeliveryPieces) DeliveryPieces
                                FROM dbo.PL_WorkOrder a
                                WHERE a.POStatus
                                      BETWEEN '1' AND '4'
                                      AND a.WorkOrderType = '1'
									  AND ISNULL(a.FreezeFlag,0)=0
									  AND a.IsEnabled=1
                                GROUP BY a.FactoryCode,
								         a.FactoryName,
								         a.ProductOrder,
                                         a.ContainerNO,
                                         a.CustomerPO,
                                         a.AvoidProduce,
										 a.WorkOrder
                            ) PW
                                ON PW.ProductOrder = PO.ProductOrder
                        WHERE PO.OrderType = '1'
                              AND NOT EXISTS
                        (
                            SELECT 1
                            FROM dbo.MM_ProductDispatchItem PD
                            WHERE PD.ProductOrder = PO.ProductOrder
                                  AND PD.ContainerNO = PW.ContainerNO
                                  AND PD.CustomerPO = PW.CustomerPO
                        ) ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //工厂 
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND PW.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND PO.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }

                //客户PO号 
                if (!queryParam["CustomerPO"].IsEmpty())
                {
                    sql.Append($" AND PW.CustomerPO like N'%{queryParam["CustomerPO"]}%'");
                }
                //柜号 
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND PW.ContainerNO = N'{queryParam["ContainerNO"]}'");
                }

                if (!queryParam["AvoidProduce"].IsEmpty())
                {
                    sql.Append($" AND PW.AvoidProduce = N'{queryParam["AvoidProduce"]}'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

    }
}
