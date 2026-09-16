using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.ProduceManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.ProduceManage;
using ALP.Application.UtilExtend.Offices;

namespace ALP.Application.Service.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-06
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_TransferCardResumeService 业务服务类
    /// 4.任务编号: 流转卡履历表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_TransferCardResume_Service : RepositoryFactory<PM_TransferCardResumeEntity>, PM_TransferCardResumeIService
    {

        #region 查询分页列表List
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_TransferCardResumeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProcessCode]
                      ,[CardCode]
                      ,[BusinessType]
                      ,[OperationId]
                      ,[Flag]
                      ,[WhsCode]
                      ,[LocationCode]
                      ,[IsInWHs]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[IsEnabled]
                  FROM [dbo].[PM_TransferCardResume] where 1=1  ");
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
                //工序 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND ProcessCode = N'{queryParam["ProcessCode"]}'");
                    sql.Append($" AND ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                }
                //流转卡编码 是否为空进行查询
                if (!queryParam["CardCode"].IsEmpty())
                {
                    //sql.Append($" AND CardCode = N'{queryParam["CardCode"]}'");
                    sql.Append($" AND CardCode like N'%{queryParam["CardCode"]}%'");
                }
                //业务类型 是否为空进行查询
                if (!queryParam["BusinessType"].IsEmpty())
                {
                    //sql.Append($" AND BusinessType = N'{queryParam["BusinessType"]}'");
                    sql.Append($" AND BusinessType like N'%{queryParam["BusinessType"]}%'");
                }
                //操作Id 是否为空进行查询
                if (!queryParam["OperationId"].IsEmpty())
                {
                    //sql.Append($" AND OperationId = N'{queryParam["OperationId"]}'");
                    sql.Append($" AND OperationId like N'%{queryParam["OperationId"]}%'");
                }
                //流转标识 是否为空进行查询
                if (!queryParam["Flag"].IsEmpty())
                {
                    //sql.Append($" AND Flag = N'{queryParam["Flag"]}'");
                    sql.Append($" AND Flag like N'%{queryParam["Flag"]}%'");
                }
                //仓库 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    //sql.Append($" AND WhsCode = N'{queryParam["WhsCode"]}'");
                    sql.Append($" AND WhsCode like N'%{queryParam["WhsCode"]}%'");
                }
                //库位 是否为空进行查询
                if (!queryParam["LocationCode"].IsEmpty())
                {
                    //sql.Append($" AND LocationCode = N'{queryParam["LocationCode"]}'");
                    sql.Append($" AND LocationCode like N'%{queryParam["LocationCode"]}%'");
                }
                //在库标识 是否为空进行查询
                if (!queryParam["IsInWHs"].IsEmpty())
                {
                    //sql.Append($" AND IsInWHs = N'{queryParam["IsInWHs"]}'");
                    sql.Append($" AND IsInWHs like N'%{queryParam["IsInWHs"]}%'");
                }
                //报工人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //报工时间 是否为空进行查询
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
                //有效标识 是否为空进行查询
                if (!queryParam["IsEnabled"].IsEmpty())
                {
                    //sql.Append($" AND IsEnabled = N'{queryParam["IsEnabled"]}'");
                    sql.Append($" AND IsEnabled like N'%{queryParam["IsEnabled"]}%'");
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

        #endregion

        #region 查询分页列表(DataTable)

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.Id,
                               a.FactoryCode,
                               a.FactoryName,
                               a.ProcessCode,
                               d.ResourceName ProcessName,
                               b.ProductOrder,
                               c.CustomerPO,
                               c.ContainerNO,
                               c.MaterialCode,
                               b.MMXH,
                               b.Spec,
                               b.ExeWorkOrder,
                               b.CardCode,
                               b.WorkOrderType,
                               a.LocationCode,
                               e.ResourceName LocationName,
                               a.BusinessType,
                               v1.ItemName BusinessTypeName,
                               a.Flag,
                               a.SheetQty,
                               a.PieceQty,
                               a.CreateTime,
                               a.Creator,
                               f.Name CreatorName,
                               CASE
                                   WHEN dbo.fn_GetPrePackingOperation(b.WorkOrder) = a.ProcessCode THEN
                                       '1'
                                   ELSE
                                       0
                               END IsLastProcess
                        FROM dbo.PM_TransferCardResume a
                            INNER JOIN dbo.PM_TransferCard b
                                ON a.CardCode = b.CardCode
                            INNER JOIN dbo.PL_WorkOrder c
                                ON b.WorkOrder = c.WorkOrder
                            LEFT JOIN dbo.BS_ModelWithResource d
                                ON a.ProcessCode = d.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource e
                                ON a.LocationCode = e.ResourceCode
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'FlowIdentification'
                                   AND a.BusinessType = v1.ItemValue
                            LEFT JOIN dbo.BS_People f
                                ON a.Creator = f.Code
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //工厂
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND a.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND b.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                if (!queryParam["ExeWorkOrder"].IsEmpty())
                {
                    sql.Append($" AND b.ExeWorkOrder like N'%{queryParam["ExeWorkOrder"]}%'");
                }
                if (!queryParam["CardCode"].IsEmpty())
                {
                    sql.Append($" AND a.CardCode like N'%{queryParam["CardCode"]}%'");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND c.ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND b.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND b.MMXH like N'%{queryParam["MMXH"]}%'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND b.Spec like N'%{queryParam["Spec"]}%'");
                }
                //库位
                if (!queryParam["LocationName"].IsEmpty())
                {
                    sql.Append($" AND e.ResourceName like N'%{queryParam["LocationName"]}%'");
                }
                if (!queryParam["UserName"].IsEmpty())
                {
                    sql.Append($" AND f.Name like N'%{queryParam["UserName"]}%'");
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND a.CreateTime >= N'{queryParam["StartTime"]}'");
                }

                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND a.CreateTime <= N'{queryParam["EndTime"]}'");
                }
                if (!queryParam["Flag"].IsEmpty())
                {
                    sql.Append($" AND a.Flag = N'{queryParam["Flag"]}'");
                }
                if (!queryParam["BusinessType"].IsEmpty())
                {
                    sql.Append($" AND a.BusinessType = N'{queryParam["BusinessType"]}'");
                }
                if (!queryParam["WorkOrderType"].IsEmpty())
                {
                    sql.Append($" AND b.WorkOrderType = N'{queryParam["WorkOrderType"]}'");
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

        #region 工序接收库存查询

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableListReceive(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"  WITH Sql1 AS(
                            SELECT 
		                            PO.FactoryCode,PO.FactoryName,TC.ProductOrder,TC.WorkOrder,TC.WorkOrderType,TC.ExeWorkOrder,TC.CardCode,TC.CardName,TC.CardType,TC.ContainerNO,PO.CustomerPO,
		                            TC.MaterialCode,TC.MMXH,TC.JCGG,
		                            TCR.CreateTime,TCR.ProcessCode,TCR.WhsCode,TCR.LocationCode,TCR.OperationId
		                             
                            FROM dbo.PL_WorkOrder PO
                            INNER JOIN dbo.PM_TransferCard TC ON TC.WorkOrder = PO.WorkOrder 
                            INNER JOIN dbo.PM_TransferCardResume TCR ON TCR.CardCode = TC.CardCode AND TCR.Flag='1'
                            INNER JOIN  dbo.BS_ModelResourceExtendInfo MRI ON TCR.LocationCode=MRI.ResourceCode AND MRI.FieldCode='KWLX' AND MRI.FieldValue='2'
                            WHERE 1=1    ");


            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND PO.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND TCR.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND TC.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND TC.ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND TC.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND TC.MMXH like N'%{queryParam["MMXH"]}%'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND TC.JCGG like N'%{queryParam["Spec"]}%'");
                }
            }
            try
            {
                sql.Append(@"
                        ),
                         
                           Sql2 AS(
                            SELECT *  
 
                             FROM (
	                            SELECT 
		                           PT.ProductOrder,PT.WorkOrder,PT.WorkOrderType,PT.ExeWorkOrder,PT.CardCode,PT.CardName,PT.CardType,PT.ContainerNO,PT.CustomerPO,PT.MaterialCode,PT.MMXH,
		                            PT.JCGG,PT.CreateTime,PT.WhsCode,PT.LocationCode,PT.OperationId,POP.OperationCode ProcessCode,POP.OperationName ProcessName,
									POP1.OperationCode,POP1.OperationName,POP.SN,
		                            ROW_NUMBER() OVER(PARTITION BY PT.WorkOrder,PT.CardCode,P.Id,POP.OperationCode ORDER BY POP.OperationCode,POP1.SN DESC) RowNum
	                            FROM Sql1 PT
	                            INNER JOIN dbo.PL_Process P ON P.WorkOrder = PT.WorkOrder 
	                            INNER JOIN  dbo.PL_ProcessOfOperations POP ON P.Id=POP.ProcessId AND PT.ProcessCode=POP.OperationCode
	                            INNER JOIN  dbo.PL_ProcessOfOperations POP1 ON P.Id=POP1.ProcessId AND POP1.SN<POP.SN
	 
                            ) A WHERE A.RowNum='1'
                           ) 

                            --上个工序报工算当前的接收
                             	SELECT * FROM(
                             SELECT 
	                            PT.ProductOrder,PT.ContainerNO,PT.MaterialCode,PT.MMXH,PT.SN,
	                            PT.JCGG,PT.ProcessCode,PT.ProcessName,SUM(TF.Qty) Qty,COUNT(1) PalletNum,
								 ");
                if (pagination != null)
                {
                    sql.Append($@" ROW_NUMBER() OVER(ORDER BY {pagination.sidx}    {pagination.sord}  ");
                    sql.Append(@" ) LineNum,COUNT(1) OVER() TotalNum
                             FROM Sql2 PT
                             INNER JOIN dbo.PM_TranferCardBGRecord TF ON TF.CardCode = PT.CardCode AND PT.OperationCode=TF.ProcessCode AND TF.IsRework='0' --and PT.OperationId=TF.Id
                             GROUP BY PT.ProductOrder,PT.ContainerNO,PT.MaterialCode,PT.MMXH,
	                            PT.JCGG,PT.ProcessCode,PT.ProcessName ,PT.SN
							) A ");
                    sql.Append($@" WHERE A.LineNum BETWEEN {(pagination.page - 1) * pagination.rows + 1} AND {pagination.page * pagination.rows}");
                    var dt = this.BaseRepository().FindTable(sql.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        pagination.records = int.Parse(dt.Rows[0]["TotalNum"].ToString());
                    }
                    return dt;

                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                    //return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 工序接收库存查询Item

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableListReceiveItem(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"--当前工序
                            SELECT 
		                            TC.ProductOrder,TC.WorkOrder,TC.WorkOrderType,TC.ExeWorkOrder,TC.CardCode,TC.CardName,TC.CardType,TC.ContainerNO,PO.CustomerPO,
		                            TC.MaterialCode,TC.MMXH,TC.JCGG,
		                            TCR.CreateTime,TCR.Creator,TCR.ProcessCode,TCR.WhsCode,TCR.LocationCode,TCR.OperationId,MW.ResourceName LocationName
		                            INTO #Temp1
                            FROM dbo.PL_WorkOrder PO
                            INNER JOIN dbo.PM_TransferCard TC ON TC.WorkOrder = PO.WorkOrder 
                            INNER JOIN dbo.PM_TransferCardResume TCR ON TCR.CardCode = TC.CardCode  AND TCR.Flag='1'
                            INNER JOIN  dbo.BS_ModelResourceExtendInfo MRI ON TCR.LocationCode=MRI.ResourceCode AND MRI.FieldCode='KWLX' AND MRI.FieldValue='2'
                            LEFT JOIN  dbo.BS_ModelWithResource MW ON MW.ResourceCode=TCR.LocationCode 
                            WHERE 1=1  
                            ");

            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();

                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND TCR.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND TC.ProductOrder = N'{queryParam["ProductOrder"]}'");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND TC.ContainerNO = N'{queryParam["ContainerNO"]}'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND TC.MaterialCode = N'{queryParam["MaterialCode"]}'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND TC.MMXH = N'{queryParam["MMXH"]}'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND TC.JCGG = N'{queryParam["Spec"]}'");
                }
            }
            try
            {
                sql.Append(@"
                            --上个工序
                            SELECT *  
                              INTO #Temp2
                             FROM (
	                            SELECT 
		                           PT.ProductOrder,PT.WorkOrder,PT.WorkOrderType,PT.ExeWorkOrder,PT.CardCode,PT.CardName,PT.CardType,PT.ContainerNO,PT.CustomerPO,PT.MaterialCode,PT.MMXH,
									PT.JCGG,PT.CreateTime,PT.WhsCode,PT.LocationCode,PT.OperationId,POP.OperationCode ProcessCode,POP.OperationName ProcessName,
									  POP1.OperationCode,POP1.OperationName,POP.SN,PT.LocationName,PT.Creator,
									ROW_NUMBER() OVER(PARTITION BY PT.WorkOrder,PT.CardCode,P.Id,POP.OperationCode ORDER BY POP.OperationCode,POP1.SN DESC) RowNum
	                            FROM #Temp1 PT
	                            INNER JOIN dbo.PL_Process P ON P.WorkOrder = PT.WorkOrder 
	                            INNER JOIN  dbo.PL_ProcessOfOperations POP ON P.Id=POP.ProcessId AND PT.ProcessCode=POP.OperationCode
	                            INNER JOIN  dbo.PL_ProcessOfOperations POP1 ON P.Id=POP1.ProcessId AND POP1.SN<POP.SN
                            ) A WHERE A.RowNum='1'
                       

                            --上个工序报工算当前的接收
                            SELECT * FROM(
                             SELECT 
	                            PT.ProductOrder,PT.WorkOrder,PT.WorkOrderType,PT.ExeWorkOrder,PT.CardCode,PT.CardName,PT.CardType,PT.ContainerNO,PT.CustomerPO,PT.MaterialCode,PT.MMXH,
		                        PT.JCGG,PT.CreateTime,BP.[Name] UserName,PT.ProcessCode,PT.WhsCode,PT.LocationCode,PT.LocationName,PT.ProcessName,SUM(TF.Qty) Qty,COUNT(1) PalletNum,
                              
                            ");
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {

                    sql.Append($@"  ROW_NUMBER() OVER(ORDER BY { pagination.sidx}  { pagination.sord}  ) LineNum,COUNT(1) OVER() TotalNum
                             FROM #Temp2 PT
                             INNER JOIN dbo.PM_TranferCardBGRecord TF ON TF.CardCode = PT.CardCode AND PT.OperationCode=TF.ProcessCode AND TF.IsRework='0' --AND PT.OperationId=TF.Id
                             LEFT JOIN dbo.BS_People BP ON BP.Code=PT.Creator
                             GROUP BY PT.ProductOrder,PT.WorkOrder,PT.WorkOrderType,PT.ExeWorkOrder,PT.CardCode,PT.CardName,PT.CardType,PT.ContainerNO,PT.CustomerPO,PT.MaterialCode,PT.MMXH,
		                            PT.JCGG,PT.CreateTime,BP.[Name],PT.ProcessCode,PT.WhsCode,PT.LocationCode,PT.LocationName,PT.ProcessName
                            ) A");

                    sql.Append($@" WHERE A.LineNum BETWEEN {(pagination.page - 1) * pagination.rows + 1} AND {pagination.page * pagination.rows}
                                DROP TABLE #Temp1,#Temp2  ");
                    var dt = this.BaseRepository().FindTable(sql.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        pagination.records = int.Parse(dt.Rows[0]["TotalNum"].ToString());
                    }
                    return dt;
                    // return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 开槽前库存查询

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableListBeforeSlotting(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"  WITH Sql1 AS(
                            SELECT 
		                            PO.FactoryCode,PO.FactoryName,TC.ProductOrder,TC.WorkOrder,TC.WorkOrderType,TC.ExeWorkOrder,TC.CardCode,TC.CardName,TC.CardType,TC.ContainerNO,PO.CustomerPO,
		                            TC.MaterialCode,TC.MMXH,TC.JCGG,
		                            TCR.CreateTime,TCR.ProcessCode,TCR.WhsCode,TCR.LocationCode,TCR.OperationId
                            FROM dbo.PL_WorkOrder PO
                            INNER JOIN dbo.PM_TransferCard TC ON TC.WorkOrder = PO.WorkOrder 
                            INNER JOIN dbo.PM_TransferCardResume TCR ON TCR.CardCode = TC.CardCode AND TCR.Flag='1'
                            INNER JOIN  dbo.BS_ModelResourceExtendInfo MRI ON TCR.LocationCode=MRI.ResourceCode AND MRI.FieldCode='KWLX' AND MRI.FieldValue='2'
                            WHERE TCR.ProcessCode='FHKC'  ");

            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND PO.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND TCR.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND TC.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND TC.ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND TC.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND TC.MMXH like N'%{queryParam["MMXH"]}%'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND TC.JCGG like N'%{queryParam["Spec"]}%'");
                }
            }
            try
            {
                sql.Append(@"
                        ),
                         
                           Sql2 AS(
                            SELECT *  
 
                             FROM (
	                            SELECT 
		                           PT.FactoryCode,PT.FactoryName,PT.ProductOrder,PT.WorkOrder,PT.WorkOrderType,PT.ExeWorkOrder,PT.CardCode,PT.CardName,PT.CardType,PT.ContainerNO,PT.CustomerPO,PT.MaterialCode,PT.MMXH,
		                            PT.JCGG,PT.CreateTime,PT.WhsCode,PT.LocationCode,PT.OperationId,POP.OperationCode ,POP.OperationName ,
									POP1.OperationCode ProcessCode,POP1.OperationName ProcessName,POP.SN,
		                            ROW_NUMBER() OVER(PARTITION BY PT.WorkOrder,PT.CardCode,P.Id,POP.OperationCode ORDER BY POP.OperationCode,POP1.SN DESC) RowNum
	                            FROM Sql1 PT
	                            INNER JOIN dbo.PL_Process P ON P.WorkOrder = PT.WorkOrder 
	                            INNER JOIN  dbo.PL_ProcessOfOperations POP ON P.Id=POP.ProcessId AND PT.ProcessCode=POP.OperationCode
	                            INNER JOIN  dbo.PL_ProcessOfOperations POP1 ON P.Id=POP1.ProcessId AND POP1.SN<POP.SN
	 
                            ) A WHERE A.RowNum='1'
                           ) 

                            --上个工序报工算当前的接收
                             	SELECT * FROM(
                             SELECT 
	                            PT.FactoryCode,PT.FactoryName,PT.ProductOrder,PT.ContainerNO,PT.MaterialCode,PT.MMXH,PT.SN,
	                            PT.JCGG,PT.ProcessCode,PT.ProcessName,SUM(TF.Qty) Qty,COUNT(1) PalletNum,
								 ");
                if (pagination != null)
                {
                    sql.Append($@" ROW_NUMBER() OVER(ORDER BY {pagination.sidx}    {pagination.sord}  ");
                    sql.Append(@" ) LineNum,COUNT(1) OVER() TotalNum
                             FROM Sql2 PT
                             INNER JOIN dbo.PM_TranferCardBGRecord TF ON TF.CardCode = PT.CardCode AND PT.OperationCode=TF.ProcessCode AND TF.IsRework='0' --and PT.OperationId=TF.Id
                             GROUP BY PT.FactoryCode,PT.FactoryName,PT.ProductOrder,PT.ContainerNO,PT.MaterialCode,PT.MMXH,
	                            PT.JCGG,PT.ProcessCode,PT.ProcessName ,PT.SN
							) A ");
                    sql.Append($@" WHERE A.LineNum BETWEEN {(pagination.page - 1) * pagination.rows + 1} AND {pagination.page * pagination.rows}");
                    var dt = this.BaseRepository().FindTable(sql.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        pagination.records = int.Parse(dt.Rows[0]["TotalNum"].ToString());
                    }
                    return dt;

                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                    //return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 开槽前库存查询Item

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableListBeforeSlottingItem(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"--当前工序
                            SELECT PO.FactoryCode,
                                   PO.FactoryName,
                                   TC.ProductOrder,
                                   TC.WorkOrder,
                                   TC.WorkOrderType,
                                   TC.ExeWorkOrder,
                                   TC.CardCode,
                                   TC.CardName,
                                   TC.CardType,
                                   TC.ContainerNO,
                                   PO.CustomerPO,
                                   TC.MaterialCode,
                                   TC.MMXH,
                                   TC.JCGG,
                                   TCR.CreateTime,
                                   TCR.Creator,
                                   TCR.ProcessCode,
                                   TCR.WhsCode,
                                   TCR.LocationCode,
                                   TCR.OperationId,
                                   MW.ResourceName LocationName
                            INTO #Temp1
                            FROM dbo.PL_WorkOrder PO
                                INNER JOIN dbo.PM_TransferCard TC
                                    ON TC.WorkOrder = PO.WorkOrder
                                INNER JOIN dbo.PM_TransferCardResume TCR
                                    ON TCR.CardCode = TC.CardCode
                                       AND TCR.Flag = '1'
                                INNER JOIN dbo.BS_ModelResourceExtendInfo MRI
                                    ON TCR.LocationCode = MRI.ResourceCode
                                       AND MRI.FieldCode = 'KWLX'
                                       AND MRI.FieldValue = '2'
                                LEFT JOIN dbo.BS_ModelWithResource MW
                                    ON MW.ResourceCode = TCR.LocationCode
                            WHERE TCR.ProcessCode = 'FHKC' ");

            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND PO.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND TCR.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND TC.ProductOrder = N'{queryParam["ProductOrder"]}'");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND TC.ContainerNO = N'{queryParam["ContainerNO"]}'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND TC.MaterialCode = N'{queryParam["MaterialCode"]}'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND TC.MMXH = N'{queryParam["MMXH"]}'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND TC.JCGG = N'{queryParam["Spec"]}'");
                }
            }
            try
            {
                sql.Append(@"
                            --上个工序
                            SELECT *  
                              INTO #Temp2
                             FROM (
	                            SELECT 
		                           PT.ProductOrder,PT.WorkOrder,PT.WorkOrderType,PT.ExeWorkOrder,PT.CardCode,PT.CardName,PT.CardType,PT.ContainerNO,PT.CustomerPO,PT.MaterialCode,PT.MMXH,
									PT.JCGG,PT.CreateTime,PT.WhsCode,PT.LocationCode,PT.OperationId,POP.OperationCode ,POP.OperationName ,
									  POP1.OperationCode ProcessCode,POP1.OperationName ProcessName,POP.SN,PT.LocationName,PT.Creator,
									ROW_NUMBER() OVER(PARTITION BY PT.WorkOrder,PT.CardCode,P.Id,POP.OperationCode ORDER BY POP.OperationCode,POP1.SN DESC) RowNum
	                            FROM #Temp1 PT
	                            INNER JOIN dbo.PL_Process P ON P.WorkOrder = PT.WorkOrder 
	                            INNER JOIN  dbo.PL_ProcessOfOperations POP ON P.Id=POP.ProcessId AND PT.ProcessCode=POP.OperationCode
	                            INNER JOIN  dbo.PL_ProcessOfOperations POP1 ON P.Id=POP1.ProcessId AND POP1.SN<POP.SN
                            ) A WHERE A.RowNum='1'
                       

                            --上个工序报工算当前的接收
                            SELECT * FROM(
                             SELECT 
	                            PT.ProductOrder,PT.WorkOrder,PT.WorkOrderType,PT.ExeWorkOrder,PT.CardCode,PT.CardName,PT.CardType,PT.ContainerNO,PT.CustomerPO,PT.MaterialCode,PT.MMXH,
		                        PT.JCGG,PT.CreateTime,BP.[Name] UserName,PT.ProcessCode,PT.WhsCode,PT.LocationCode,PT.LocationName,PT.ProcessName,SUM(TF.Qty) Qty,COUNT(1) PalletNum,
                              
                            ");
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {

                    sql.Append($@"  ROW_NUMBER() OVER(ORDER BY { pagination.sidx}  { pagination.sord}  ) LineNum,COUNT(1) OVER() TotalNum
                             FROM #Temp2 PT
                             INNER JOIN dbo.PM_TranferCardBGRecord TF ON TF.CardCode = PT.CardCode AND PT.OperationCode=TF.ProcessCode AND TF.IsRework='0' --AND PT.OperationId=TF.Id
                             LEFT JOIN dbo.BS_People BP ON BP.Code=PT.Creator
                             GROUP BY PT.ProductOrder,PT.WorkOrder,PT.WorkOrderType,PT.ExeWorkOrder,PT.CardCode,PT.CardName,PT.CardType,PT.ContainerNO,PT.CustomerPO,PT.MaterialCode,PT.MMXH,
		                            PT.JCGG,PT.CreateTime,BP.[Name],PT.ProcessCode,PT.WhsCode,PT.LocationCode,PT.LocationName,PT.ProcessName
                            ) A");

                    sql.Append($@" WHERE A.LineNum BETWEEN {(pagination.page - 1) * pagination.rows + 1} AND {pagination.page * pagination.rows}
                                DROP TABLE #Temp1,#Temp2  ");
                    var dt = this.BaseRepository().FindTable(sql.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        pagination.records = int.Parse(dt.Rows[0]["TotalNum"].ToString());
                    }
                    return dt;
                    // return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 工序完工库存查询

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableListCompete(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT TC.ProductOrder,
                               TC.WorkOrder,
                               TC.WorkOrderType,
                               TC.ExeWorkOrder,
                               TC.CardCode,
                               TC.CardName,
                               TC.CardType,
                               TC.ContainerNO,
                               PO.CustomerPO,
                               TC.MaterialCode,
                               TC.MMXH,
                               TC.JCGG,
                               TCR.FactoryCode,
                               TCR.FactoryName,
                               TCR.CreateTime,
                               TCR.Creator,
                               TCR.ProcessCode,
                               MW1.ResourceName ProcessName,
                               TCR.WhsCode,
                               TCR.LocationCode,
                               TCR.OperationId,
                               MW.ResourceName LocationName
                        INTO #Temp1
                        FROM dbo.PL_WorkOrder PO
                            INNER JOIN dbo.PM_TransferCard TC
                                ON TC.WorkOrder = PO.WorkOrder
                            INNER JOIN dbo.PM_TransferCardResume TCR
                                ON TCR.CardCode = TC.CardCode
                                   AND TCR.Flag = '1'
                            INNER JOIN dbo.BS_ModelResourceExtendInfo MRI
                                ON TCR.LocationCode = MRI.ResourceCode
                                   AND MRI.FieldCode = 'KWLX'
                                   AND MRI.FieldValue = '3'
                            LEFT JOIN dbo.BS_ModelWithResource MW
                                ON MW.ResourceCode = TCR.LocationCode
                            LEFT JOIN dbo.BS_ModelWithResource MW1
                                ON MW1.ResourceCode = TCR.ProcessCode
                        WHERE 1 = 1 ");

            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND TCR.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND TCR.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND TC.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND TC.ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND TC.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND TC.MMXH like N'%{queryParam["MMXH"]}%'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND TC.JCGG like N'%{queryParam["Spec"]}%'");
                }
            }
            try
            {
                sql.Append(@"
                            SELECT * FROM(
                              SELECT 
	                            PT.ProductOrder,PT.ContainerNO,PT.MaterialCode,PT.MMXH,
	                            PT.JCGG,PT.ProcessCode,PT.ProcessName,SUM(TF.Qty) Qty,COUNT(1) PalletNum,
                         ");
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    sql.Append($@"  ROW_NUMBER() OVER(ORDER BY { pagination.sidx}  { pagination.sord}  ) LineNum,COUNT(1) OVER() TotalNum
                                FROM #Temp1 PT
                             INNER JOIN dbo.PM_TranferCardBGRecord TF ON TF.CardCode = PT.CardCode AND PT.ProcessCode=TF.ProcessCode AND TF.IsRework='0' AND PT.OperationId=TF.Id
                             LEFT JOIN dbo.BS_People BP ON BP.Code=PT.Creator
                             GROUP BY  PT.ProductOrder,PT.ContainerNO,PT.MaterialCode,PT.MMXH,
	                                                        PT.JCGG,PT.ProcessCode,PT.ProcessName
                            ) A ");

                    sql.Append($@" WHERE A.LineNum BETWEEN {(pagination.page - 1) * pagination.rows + 1} AND {pagination.page * pagination.rows}
                                DROP TABLE #Temp1  ");
                    var dt = this.BaseRepository().FindTable(sql.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        pagination.records = int.Parse(dt.Rows[0]["TotalNum"].ToString());
                    }
                    return dt;
                    //return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 工序完工库存查询Item

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableListCompeteItem(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PO.FactoryCode,
                               PO.FactoryName,
                               TC.ProductOrder,
                               TC.WorkOrder,
                               TC.WorkOrderType,
                               TC.ExeWorkOrder,
                               TC.CardCode,
                               TC.CardName,
                               TC.CardType,
                               TC.ContainerNO,
                               PO.CustomerPO,
                               TC.MaterialCode,
                               TC.MMXH,
                               TC.JCGG,
                               TCR.CreateTime,
                               TCR.Creator,
                               TCR.ProcessCode,
                               MW1.ResourceName ProcessName,
                               TCR.WhsCode,
                               TCR.LocationCode,
                               TCR.OperationId,
                               MW.ResourceName LocationName
                        INTO #Temp1
                        FROM dbo.PL_WorkOrder PO
                            INNER JOIN dbo.PM_TransferCard TC
                                ON TC.WorkOrder = PO.WorkOrder
                            INNER JOIN dbo.PM_TransferCardResume TCR
                                ON TCR.CardCode = TC.CardCode
                                   AND TCR.Flag = '1'
                            INNER JOIN dbo.BS_ModelResourceExtendInfo MRI
                                ON TCR.LocationCode = MRI.ResourceCode
                                   AND MRI.FieldCode = 'KWLX'
                                   AND MRI.FieldValue = '3'
                            LEFT JOIN dbo.BS_ModelWithResource MW
                                ON MW.ResourceCode = TCR.LocationCode
                            LEFT JOIN dbo.BS_ModelWithResource MW1
                                ON MW1.ResourceCode = TCR.ProcessCode
                        WHERE 1 = 1 ");

            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND PO.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND TCR.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND TC.ProductOrder = N'{queryParam["ProductOrder"]}'");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND TC.ContainerNO = N'{queryParam["ContainerNO"]}'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND TC.MaterialCode = N'{queryParam["MaterialCode"]}'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND TC.MMXH = N'{queryParam["MMXH"]}'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND TC.JCGG = N'{queryParam["Spec"]}'");
                }
            }
            try
            {
                sql.Append(@"
                            SELECT * FROM(
                              SELECT 
	                            PT.ProductOrder,PT.WorkOrder,PT.WorkOrderType,PT.ExeWorkOrder,PT.CardCode,PT.CardName,PT.CardType,PT.ContainerNO,PT.CustomerPO,PT.MaterialCode,PT.MMXH,
		                        PT.JCGG,PT.CreateTime,BP.[Name] UserName,PT.ProcessCode,PT.ProcessName,PT.WhsCode,PT.LocationCode,PT.LocationName,SUM(TF.Qty) Qty,COUNT(1) PalletNum,
                           ");
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    sql.Append($@"  ROW_NUMBER() OVER(ORDER BY { pagination.sidx}  { pagination.sord}  ) LineNum,COUNT(1) OVER() TotalNum
                                FROM #Temp1 PT
                             INNER JOIN dbo.PM_TranferCardBGRecord TF ON TF.CardCode = PT.CardCode AND PT.ProcessCode=TF.ProcessCode AND TF.IsRework='0'
                             LEFT JOIN dbo.BS_People BP ON BP.Code=PT.Creator
                             GROUP BY PT.ProductOrder,PT.WorkOrder,PT.WorkOrderType,PT.ExeWorkOrder,PT.CardCode,PT.CardName,PT.CardType,PT.ContainerNO,PT.CustomerPO,PT.MaterialCode,PT.MMXH,
		                            PT.JCGG,PT.CreateTime,BP.[Name],PT.ProcessCode,PT.ProcessName,PT.WhsCode,PT.LocationCode,PT.LocationName
                            ) A ");

                    sql.Append($@" WHERE A.LineNum BETWEEN {(pagination.page - 1) * pagination.rows + 1} AND {pagination.page * pagination.rows}
                                DROP TABLE #Temp1  ");
                    var dt = this.BaseRepository().FindTable(sql.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        pagination.records = int.Parse(dt.Rows[0]["TotalNum"].ToString());
                    }
                    return dt;
                    //return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="dic">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_TransferCardResumeEntity> GetList(Dictionary<string, object> dic)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.Id,a.FactoryCode,a.FactoryName,
                               a.ProcessCode,
                               a.CardCode,
                               a.BusinessType,
                               a.OperationId,
                               a.Flag,
                               a.WhsCode,
                               a.LocationCode,
                               a.SheetQty,
                               a.PieceQty,
                               a.IsInWHs,
                               a.Creator,
                               a.CreateTime,
                               a.ModifyBy,
                               a.ModifyTime,
                               a.IsEnabled,
                               b.ProductOrder,
                               b.ContainerNO,
                               b.MaterialCode
                        FROM dbo.PM_TransferCardResume a
                            INNER JOIN dbo.PM_TransferCard b
                                ON a.CardCode = b.CardCode
                        WHERE EXISTS
                        (
                            SELECT 1
                            FROM dbo.BS_ModelResourceExtendInfo c
                            WHERE c.FieldCode = 'GLFS'
                                  AND c.FieldValue = '1'
                                  AND c.EnabledMark = 1
                                  AND c.ResourceCode = a.LocationCode
                        ) ");
            //订单号
            if (dic.ContainsKey("ProductOrder"))
            {
                sql.Append($@" AND b.ProductOrder='{dic["ProductOrder"]}' ");
            }
            //柜号
            if (dic.ContainsKey("ContainerNO"))
            {
                sql.Append($@" AND b.ContainerNO='{dic["ContainerNO"]}' ");
            }
            //客户型号
            if (dic.ContainsKey("MaterialCode"))
            {
                sql.Append($@" AND b.MaterialCode='{dic["MaterialCode"]}' ");
            }
            //仓库编码
            if (dic.ContainsKey("WhsCode"))
            {
                sql.Append($@" AND a.WhsCode='{dic["WhsCode"]}' ");
            }
            //有效标识
            if (dic.ContainsKey("Flag"))
            {
                sql.Append($@" AND a.Flag='{dic["Flag"]}' ");
            }

            return this.BaseRepository().FindList(sql.ToString());

        }
        public IEnumerable<PM_TransferCardResumeEntity> GetList(Expression<Func<PM_TransferCardResumeEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PM_TransferCardResumeEntity entity, out string msg)
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
                throw ex;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_TransferCardResumeEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_TransferCardResumeEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PM_TransferCardResume] set ");
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

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
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
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            PM_TransferCardResumeEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记

                this.BaseRepository().Delete(entity);
                result = 1;
            }
            else
            {
                result = 0;//没有找到记录
            }

            return result;
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
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
                sql.Append($@"DELETE FROM [dbo].[PM_TransferCardResume] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_TransferCardResumeEntity</returns>
        public PM_TransferCardResumeEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_TransferCardResumeEntity</returns>
        public PM_TransferCardResumeEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_TransferCardResumeEntity 对象</returns>
        public PM_TransferCardResumeEntity Get_ExpressionEntity(Expression<Func<PM_TransferCardResumeEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_TransferCardResumeEntity 列表</returns>
        public IEnumerable<PM_TransferCardResumeEntity> Get_ExpressionList(Expression<Func<PM_TransferCardResumeEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition).ToList();
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
        //    RepositoryFactory<PM_TransferCardResumeEntity> bomService = new RepositoryFactory<PM_TransferCardResumeEntity>();

        //    PM_TransferCardResumeEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PM_TransferCardResumeDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PM_TransferCardResume_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_TransferCardResumeEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PM_TransferCardResumeEntity> PM_TransferCardResumeEntity_list = db2.FindList<PM_TransferCardResumeEntity>(sql.ToString());
                return PM_TransferCardResumeEntity_list;
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
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
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
                DataTable PM_TransferCardResumeEntity_DataTable = db2.FindTable(sql.ToString());
                return PM_TransferCardResumeEntity_DataTable;
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
        /// 创建日期: 2021-09-06 09:28:34
        /// 任务编号: 流转卡履历表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [ProcessCode] as '工序'
                      ,[CardCode] as '流转卡编码'
                      ,[BusinessType] as '业务类型'
                      ,[OperationId] as '操作Id'
                      ,[Flag] as '流转标识'
                      ,[WhsCode] as '仓库'
                      ,[LocationCode] as '库位'
                      ,[IsInWHs] as '在库标识'
                      ,[Creator] as '报工人'
                      ,[CreateTime] as '报工时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                      ,[IsEnabled] as '有效标识'
                  FROM [dbo].[PM_TransferCardResume] where 1=1  ");
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
                string saveFileName = "流转卡履历表_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("流转卡履历表", dt, true);
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

        public int RemoveForm(Expression<Func<PM_TransferCardResumeEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
    }
}
