using ALP.Application.Entity.PlanManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Application.IService.PlanManage;
using System.Data.SqlClient;

namespace ALP.Application.Service.PlanManage
{
    public class PL_PlanStoreIssue_Service: RepositoryFactory<PL_PlanStoreIssueEntity>, PL_PlanStoreIssueIService
    {
        #region 生产工单-旧版本
        public DataTable GetListWithPageOld(Pagination pagination, string queryJson)
        {
            var sql = new StringBuilder();
            var parameter = new List<DbParameter>();
            sql.Append(@"SELECT 
	                        PW.FactoryCode,PW.ProductOrder,PW.WorkOrder,PW.WorkOrderType,PW.ContainerNO,PW.MaterialCode,Pw.OrderPieces,PW.OrderBox,PW.DemandMaterial,PW.OrderStatus,
							PW.OrderPallet,PW.OrderStartPallet,PW.Yield,PW.ActualSheets,PW.Process,PW.StartOperation,PW.TransferBy,PW.FirstInspectionConfirm,PW.FirstInspectionOperation,
	                        PS.Id,PS.MaskStatus,PS.WearLayerStatus,PS.ProductNum,PS.UnProductNum,PS.Remark,PS.WhsCode,PS.LocationCode,PMA.DXZH,
							SUM(PE.ShouldNum) ShouldNum,SUM(PE.ActualNum) ActualNum,SUM(PE.SuperNum) SuperNum,SUM(PE.CancellingNum) CancellingNum,SUM(PE.ConsumeNum)ConsumeNum ,
							bb.MaterialName,PMA.MMXH,bb.Unit,AVG(bb.Num/bo.UnitNum) DanHao,
                            PMA.SmallClass
                         FROM dbo.PL_WorkOrder PW
                         INNER JOIN dbo.PL_PlanStoreIssue PS ON PW.WorkOrder=PS.WorkOrder
						 LEFT JOIN dbo.PL_ExeWorkOrder PE ON PW.WorkOrder=PE.WorkOrder AND PE.IsEnabled=1
						 LEFT JOIN [dbo].[fn_GetMaterialAttrs]() PMA ON PMA.WorkOrder=pw.WorkOrder AND pw.MaterialCode=PMA.MaterialCode
						 LEFT JOIN dbo.PL_BOM bo ON pw.WorkOrder=bo.WorkOrder
						 LEFT JOIN dbo.PL_BOMItems bb ON bo.Id=bb.BOMId AND PMA.MMXH=bb.MaterialCode
                         WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
 
                if (!queryParam["MaskStatus"].IsEmpty())
                {
                    sql.Append($" AND MaskStatus = N'{queryParam["MaskStatus"]}'");
                }
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    sql.Append($" AND PMA.SmallClass = N'{queryParam["SmallClass"]}'");
                }
                if (!queryParam["SupeNum"].IsEmpty())
                {
                    sql.Append($" AND PE.SuperNum  {queryParam["SupeNum"]}");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND ContainerNO = '{queryParam["ContainerNO"]}'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND PMA.MMXH like N'%{queryParam["MMXH"]}%'");
                }
                if (!queryParam["DemandMaterial"].IsEmpty())
                {
                    sql.Append($" AND PW.DemandMaterial = N'{queryParam["DemandMaterial"]}'");
                }
                if (!queryParam["DemandMaterial"].IsEmpty())
                {
                    sql.Append($" AND PW.DemandMaterial = N'{queryParam["DemandMaterial"]}'");
                }
                if (!queryParam["DemandStatus"].IsEmpty())
                {
                    sql.Append($" AND PW.WorkOrderType  in ({queryParam["DemandStatus"]})");
                }
                if (!queryParam["workOrderStatusStr"].IsEmpty())
                {
                    sql.Append($" AND PW.OrderStatus in({queryParam["workOrderStatusStr"]})");
                }

                sql.Append(@" GROUP BY  PW.FactoryCode,PW.ProductOrder,PW.WorkOrder,PW.WorkOrderType,PW.ContainerNO,PW.MaterialCode,Pw.OrderPieces,PW.OrderBox,PW.DemandMaterial,PW.OrderStatus,
							PW.OrderPallet,PW.OrderStartPallet,PW.Yield,PW.ActualSheets,PW.Process,PW.StartOperation,PW.TransferBy,PW.FirstInspectionConfirm,PW.FirstInspectionOperation,
	                        PS.Id,PS.MaskStatus,PS.WearLayerStatus,PS.ProductNum,PS.UnProductNum,bb.MaterialName,PMA.MMXH,bb.Unit,PS.Remark,PS.WhsCode,PS.LocationCode,PMA.DXZH,PMA.SmallClass");
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

        #region 生产工单
        public DataTable GetListWithPage(Pagination pagination, string queryJson)
        {
            var sql = new StringBuilder();
            var parameter = new List<DbParameter>();
            sql.Append(@"SELECT PW.FactoryCode,
                               PW.FactoryName,
                               PW.ProductOrder,
                               PW.WorkOrder,
                               PW.WorkOrderType,
                               PW.ContainerNO,
                               PW.MaterialCode,
                               CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.MaterialName
                                   ELSE
                                       PW.MMXH
                               END AS MaterialName,
							    CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.MaterialName
                                   ELSE
                                       PW.MMXH
                               END AS MMXH,
                               CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.Spec
                                   ELSE
                                       PW.Spec
                               END AS Spec,
							    CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.SmallClass
                                   ELSE
                                       PMA.WLXL
                               END AS SmallClass,
                               PW.OrderPieces,
                               PW.OrderBox,
                               PW.DemandMaterial,
                               PW.OrderStatus,
                               PW.OrderPallet,
                               PW.OrderStartPallet,
                               PW.Yield,
                               PW.ActualSheets,
                               PW.Process,
                               PW.StartOperation,
                               PW.TransferBy,
                               PW.FirstInspectionConfirm,
                               PW.FirstInspectionOperation,
                               PW.FreezeFlag,
                               PS.Id,
                               PS.MaskStatus,
                               PS.WearLayerStatus,
                               PS.ProductNum,
                               PS.UnProductNum,
                               PS.Remark,
                               PS.WhsCode,
                               PS.LocationCode,
                               PS.DXZH,
                               PS.OrderNum,
                               PS.ShouldNum,
                               PE.ActualNum,
                               PE.SuperNum,
                               PE.CancellingNum,
                               PE.ConsumeNum,
                               PS.MaskConsume,
                               PS.StoreIssueNo,
                               CASE
                                   WHEN PE.ConsumeNum > 0 THEN
                                       CAST(PS.ShouldNum * 1.0 / PE.ConsumeNum AS DECIMAL(10, 2))
                               END StoreIssueParam,
                               PE.ModifyTime,
							   PMA.MMCJ
                        FROM dbo.PL_WorkOrder PW
                            INNER JOIN dbo.PL_PlanStoreIssue PS
                                ON PW.FactoryCode = PS.FactoryCode
                                   AND PW.WorkOrder = PS.WorkOrder
                            LEFT JOIN
                            (
                                SELECT WorkOrder,
								       MAX(CreateTime) ModifyTime,
                                       SUM(ActualNum) ActualNum,
                                       SUM(SuperNum) SuperNum,
                                       SUM(CancellingNum) CancellingNum,
                                       SUM(ConsumeNum) ConsumeNum
                                FROM dbo.PL_ExeWorkOrder
                                WHERE IsEnabled = 1
                                GROUP BY WorkOrder
                            ) PE
                                ON PW.WorkOrder = PE.WorkOrder
                            LEFT JOIN [dbo].[fn_GetMaterialAttrs]() PMA
                                ON PMA.WorkOrder = PW.WorkOrder
                                    AND  pw.FactoryCode=pma.FactoryCode
                        WHERE 1 = 1
						AND PW.OrderStatus<>'40' ");  
            //工单已完成的不显示，由于有退料业务，所以不再限制
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND PW.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["MaskStatus"].IsEmpty())
                {
                    sql.Append($" AND PS.MaskStatus = N'{queryParam["MaskStatus"]}'");
                }
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    sql.Append($" AND (PMA.SmallClass = N'{queryParam["SmallClass"]}' OR PMA.WLXL = N'{queryParam["SmallClass"]}') ");
                }
                if (!queryParam["SupeNum"].IsEmpty())
                {
                    sql.Append($" AND PE.SuperNum  {queryParam["SupeNum"]}");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND PW.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND PW.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    sql.Append($" AND (PMA.MaterialName like N'%{queryParam["MaterialName"]}%' OR PW.MMXH like N'%{queryParam["MaterialName"]}%')");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND PW.ContainerNO = '{queryParam["ContainerNO"]}'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND PMA.MMXH like N'%{queryParam["MMXH"]}%'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND (PMA.Spec like N'%{queryParam["Spec"]}%' OR PW.Spec like N'%{queryParam["Spec"]}%') ");
                }
                if (!queryParam["DemandMaterial"].IsEmpty())
                {
                    sql.Append($" AND PW.DemandMaterial = N'{queryParam["DemandMaterial"]}'");
                }
                if (!queryParam["DemandMaterial"].IsEmpty())
                {
                    sql.Append($" AND PW.DemandMaterial = N'{queryParam["DemandMaterial"]}'");
                }
                if (!queryParam["DemandStatus"].IsEmpty())
                {
                    sql.Append($" AND PW.WorkOrderType  in ({queryParam["DemandStatus"]})");
                }
                if (!queryParam["WorkOrderType"].IsEmpty())
                {
                    sql.Append($" AND PW.WorkOrderType   = N'{queryParam["WorkOrderType"]}'");
                }
                if (!queryParam["workOrderStatusStr"].IsEmpty())
                {
                    sql.Append($" AND PW.OrderStatus in({queryParam["workOrderStatusStr"]})");
                }
                if (!queryParam["StoreIssueNo"].IsEmpty())
                {
                    sql.Append($" AND PS.StoreIssueNo like N'%{queryParam["StoreIssueNo"]}%'");
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

        #region 执行工单
        public DataTable GetListWithPageExeWorkOrder(Pagination pagination, string queryJson)
        {
            var sql = new StringBuilder();
            var parameter = new List<DbParameter>();
            sql.Append(@" SELECT PE.FactoryCode,pe.FactoryName,
                               PW.ProductOrder,
                               PW.WorkOrder,
                               PW.WorkOrderType,
                               PW.ContainerNO,
                               PW.MaterialCode,
                               PW.OrderPieces,
                               PW.OrderBox,
                               PW.DemandMaterial,
                               PW.OrderPallet,
                               PW.OrderStartPallet,
                               PW.Yield,
                               PW.ActualSheets,
                               PW.Process,
                               PW.TransferBy,
                               PW.FirstInspectionConfirm,
                               PW.FirstInspectionOperation,
                               PS.MaskStatus,
                               PS.WearLayerStatus,
                               PS.ProductNum,
                               CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.MaterialName
                                   ELSE
                                       PW.MMXH
                               END AS MaterialName,
                               bb.MaterialCode MMXH, 
                               PMA.DXZH,
                               bb.Unit,
							   bb.UnitName,
                                CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.SmallClass
                                   ELSE
                                       PMA.WLXL
                               END AS SmallClass,
							    CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                        V.ItemName
                                   ELSE
                                       PMA.WLXL
                               END AS SmallClassName,
                               CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.Spec
                                   ELSE
                                       PW.Spec
                               END AS Spec,
                               PS.UnProductNum,
                               PE.ShouldNum,
                               PE.ActualNum,
                               PE.SuperNum,
                               PE.CancellingNum,
                               PE.ConsumeNum,
                               PE.SheetsQty,
                               PE.PSheetsQty,
                               PE.PiecesQty,
                               PE.Id,
                               PE.StartOperation,
                               PE.ExeWorkOrder,
                               PE.OrderType ExeOrderType,
							   pe.CreateTime,
							   pe.Creator,
							   pe.SendOutBatch,
							   u.Name CreatorName,
							   pe.SupId,
                               MR.ResourceName StartOperationName
                        FROM dbo.PL_WorkOrder PW
                            INNER JOIN dbo.PL_PlanStoreIssue PS
                                ON pw.FactoryCode=ps.FactoryCode 
								AND PW.WorkOrder = PS.WorkOrder
                            INNER JOIN dbo.PL_ExeWorkOrder PE
                                ON PE.WorkOrder = PW.WorkOrder
                                   AND PE.IsEnabled = 1
                            LEFT JOIN [dbo].[fn_GetMaterialAttrs]() PMA
                                ON PMA.WorkOrder = PW.WorkOrder
                                and pw.FactoryCode=pma.FactoryCode
                            LEFT JOIN dbo.BS_ModelWithResource MR
                                ON MR.ResourceCode = PE.StartOperation
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnCode = 'MaterialSmall'
                                   AND V.ItemValue = PMA.SmallClass
                            LEFT JOIN dbo.PL_BOM bo
                                ON PW.WorkOrder = bo.WorkOrder
								AND bo.IsDeleted=0
               and bo.FactoryCode=pw.FactoryCode
                            LEFT JOIN dbo.PL_BOMItems bb
                                ON bo.Id = bb.BOMId
                              AND bb.SmallClass = 'MM'
							LEFT JOIN  dbo.BS_People u ON pe.Creator=u.Code
                        WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    sql.Append($" AND PW.WorkOrder = N'{queryParam["WorkOrder"]}'");
                }
                if (!queryParam["MaskStatus"].IsEmpty())
                {
                    sql.Append($" AND MaskStatus = N'{queryParam["MaskStatus"]}'");
                }
                if (!queryParam["SupeNum"].IsEmpty())
                {
                    sql.Append($" AND SupeNum  '{queryParam["SupeNum"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND PW.ProductOrder = N'{queryParam["ProductOrder"]}'");
                }
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    sql.Append($" AND PMA.MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND ContainerNO = '{queryParam["ContainerNO"]}'");
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

        #region 修改保存
        public int UpdateForm(List<PL_PlanStoreIssueEntity> list)
        {
            return this.BaseRepository().Update(list);
        }
        public int SaveForm(string keyvalue, PL_PlanStoreIssueEntity entity)
        {
            if (string.IsNullOrEmpty(keyvalue))
            {
               return this.BaseRepository().Insert(entity);
            }
            else
            {
                return this.BaseRepository().Update(entity);
            }
        }
        public int RemoveForm(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        public PL_PlanStoreIssueEntity GetEntity(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }

        public int InsertList(List<PL_PlanStoreIssueEntity> lstEntity)
        {
           return this.BaseRepository().Insert(lstEntity);
        }
        public int UpdateList(List<PL_PlanStoreIssueEntity> lstEntity)
        {
            return this.BaseRepository().Update(lstEntity);
        }
        public IEnumerable<PL_PlanStoreIssueEntity> GetList(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_PlanStoreIssueEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PL_PlanStoreIssue] set ");
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
                                    if (x.Name == "IsDeleted")
                                    {
                                        if (x.GetValue(Save_obj, null) != null)
                                        {
                                            sql_temp.Append(x.Name + "=" + (x.GetValue(Save_obj, null) == null ? 0 : (x.GetValue(Save_obj, null).ToString() == "true" ? 1 : 0)) + ",");
                                        }
                                    }
                                    else
                                    {
                                        if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                        {
                                            sql_temp.Append(x.Name + "=N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "',");
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
                throw ex;
            }
            return n;
        }
        #endregion

        #region 获取流水号
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
        #endregion

        #region 获取拆解工单 超产品 应发数量等--旧版本
        public DataTable GetStoreIssueWorkOrderOld(string index,string queryJson)
        {
            var sql = new StringBuilder();
            var parameter = new List<DbParameter>();
            sql.Append(@"SELECT 
	                         PW.ProductOrder,PW.WorkOrder,PW.ContainerNO,PW.WorkOrderType,PW.StartOperation,S.SuperProdunction,AVG(bb.Num/bo.UnitNum) DanHao,
							 bb.Unit,PW.Process,PW.TransferBy,
							 PS.Id, PS.ProductNum,PS.UnProductNum,PW.Yield,
	                         PW.MaterialCode,PMA.MaterialName,PMA.DXZH,PMA.MMXH,
							 PS.ProductNum-SUM(ISNULL(PE.SheetsQty,0)) ActNum,
							 CEILING((PS.ProductNum-SUM(ISNULL(PE.SheetsQty,0)))*PMA.DXZH*AVG(bb.Num/BO.UnitNum)) ShouldNum
                        FROM dbo.PL_WorkOrder PW
                        INNER JOIN dbo.PL_PlanStoreIssue PS ON PS.WorkOrder = PW.WorkOrder
                        LEFT JOIN dbo.PL_ExeWorkOrder PE ON PS.WorkOrder=PE.WorkOrder AND PE.IsEnabled=1
                        LEFT JOIN dbo.fn_GetMaterialAttrs() PMA ON PMA.WorkOrder = PW.WorkOrder 
						LEFT JOIN dbo.PL_BOM bo ON pw.WorkOrder=bo.WorkOrder
						LEFT JOIN dbo.PL_BOMItems bb ON bo.Id=bb.BOMId AND PMA.MMXH=bb.MaterialCode
						LEFT JOIN (
							SELECT MMXH,SUM(CEILING(StockQty/DXZH-ISNULL(LockedQty,0)/DXZH)) SuperProdunction FROM dbo.MM_SuperProductStock
	                        GROUP BY MMXH
						) S ON S.MMXH = PMA.MMXH
                        WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();

                if (!queryParam["WorkOrderStr"].IsEmpty())
                {
                    sql.Append($" AND PW.WorkOrder in ( N{queryParam["WorkOrderStr"]})");
                }
            }

            sql.Append(@" GROUP BY    PW.ProductOrder,PW.WorkOrder,PW.ContainerNO,PW.WorkOrderType,PW.StartOperation,
							 bb.Unit,PW.Process,PW.TransferBy,PS.Id,PS.ProductNum,PS.UnProductNum,PW.Yield,
	                         PW.MaterialCode,PMA.MaterialName,PMA.DXZH,PMA.MMXH,S.SuperProdunction");

            return this.BaseRepository().FindTable(sql.ToString());
        }
        #endregion

        #region 获取拆解工单 超产品 应发数量等
        public DataTable GetStoreIssueWorkOrder(List<dynamic> list)
        {
            var msg = "";
            DataTable dt = WorkOrderToDataTable(list);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkOrder", dt)
            };
            //parameters[1].Value = listPrd[0].Creator;
            //parameters[2].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
               return db.ExecuteProc_Table("PL_GetStoreIssueWorkOrder", parameters);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("生产订单导入失败");
                }
            }
            return null;
        }
        /// <summary>
        /// 将List转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        private DataTable WorkOrderToDataTable(List<dynamic> list)
        {
            if (list == null || list.Count == 0) return null;
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("WorkOrder", System.Type.GetType("System.String"));
            foreach (var item in list)
            {
                dt.Rows.Add(item.WorkOrder);
            }
            return dt;
        }
        #endregion

        public int Delete(List<PL_PlanStoreIssueEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }
        public IEnumerable<PL_PlanStoreIssueEntity> Get_ExpressionList(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();
           
        }
    }
}
