using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.SAP;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.UtilExtend.Offices;
using ALP.Application.Service.PlanManage;

namespace ALP.Application.Service.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-11-16
    /// 2.创建作者: jpf
    /// 3.功能描述: PL_TransfersRecordService 业务服务类
    /// 4.任务编号: 跨工厂调拨
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_TransfersRecord_Service : RepositoryFactory<PL_TransfersRecordEntity>
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_TransfersRecordEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[SendFactoryCode]
                      ,[SendFactoryName]
                      ,[AcceptFactoryCode]
                      ,[AcceptFactoryName]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[MaterialCode]
                      ,[TransTypeCode]
                      ,[TransTypeName]
                      ,[TransProcessCode]
                      ,[TransProcessName]
                      ,[TransStateCode]
                      ,[TransStateName]
                      ,[Remark]
                      ,[IsDelete]
                      ,[Creator]
                      ,[CreateTime]
                      ,[CreateName]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[ModifyName]
                  FROM [dbo].[PL_TransfersRecord] where IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //ID 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //发起工厂编码 是否为空进行查询
                if (!queryParam["SendFactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND SendFactoryCode = N'{queryParam["SendFactoryCode"]}'");
                    sql.Append($" AND SendFactoryCode like N'%{queryParam["SendFactoryCode"]}%'");
                }
                //发起工厂名称 是否为空进行查询
                if (!queryParam["SendFactoryName"].IsEmpty())
                {
                    //sql.Append($" AND SendFactoryName = N'{queryParam["SendFactoryName"]}'");
                    sql.Append($" AND SendFactoryName like N'%{queryParam["SendFactoryName"]}%'");
                }
                //接收工厂编码 是否为空进行查询
                if (!queryParam["AcceptFactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND AcceptFactoryCode = N'{queryParam["AcceptFactoryCode"]}'");
                    sql.Append($" AND AcceptFactoryCode like N'%{queryParam["AcceptFactoryCode"]}%'");
                }
                //接收工厂名称 是否为空进行查询
                if (!queryParam["AcceptFactoryName"].IsEmpty())
                {
                    //sql.Append($" AND AcceptFactoryName = N'{queryParam["AcceptFactoryName"]}'");
                    sql.Append($" AND AcceptFactoryName like N'%{queryParam["AcceptFactoryName"]}%'");
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
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //调拨类型:全工序调拨、部分工序调拨数据字典取值 是否为空进行查询
                if (!queryParam["TransTypeCode"].IsEmpty())
                {
                    //sql.Append($" AND TransTypeCode = N'{queryParam["TransTypeCode"]}'");
                    sql.Append($" AND TransTypeCode like N'%{queryParam["TransTypeCode"]}%'");
                }
                //调拨类型:全工序调拨、部分工序调拨数据字典取值 是否为空进行查询
                if (!queryParam["TransTypeName"].IsEmpty())
                {
                    //sql.Append($" AND TransTypeName = N'{queryParam["TransTypeName"]}'");
                    sql.Append($" AND TransTypeName like N'%{queryParam["TransTypeName"]}%'");
                }
                //调拨工序编码 是否为空进行查询
                if (!queryParam["TransProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND TransProcessCode = N'{queryParam["TransProcessCode"]}'");
                    sql.Append($" AND TransProcessCode like N'%{queryParam["TransProcessCode"]}%'");
                }
                //调拨工序名称 是否为空进行查询
                if (!queryParam["TransProcessName"].IsEmpty())
                {
                    //sql.Append($" AND TransProcessName = N'{queryParam["TransProcessName"]}'");
                    sql.Append($" AND TransProcessName like N'%{queryParam["TransProcessName"]}%'");
                }
                //调拨状态：0待确认、1已确认、2已回退 是否为空进行查询
                if (!queryParam["TransStateCode"].IsEmpty())
                {
                    //sql.Append($" AND TransStateCode = N'{queryParam["TransStateCode"]}'");
                    sql.Append($" AND TransStateCode like N'%{queryParam["TransStateCode"]}%'");
                }
                //调拨状态：0待确认、1已确认、2已回退 是否为空进行查询
                if (!queryParam["TransStateName"].IsEmpty())
                {
                    //sql.Append($" AND TransStateName = N'{queryParam["TransStateName"]}'");
                    sql.Append($" AND TransStateName like N'%{queryParam["TransStateName"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //删除标识1代表已删除0代表未删除 是否为空进行查询
                if (!queryParam["IsDelete"].IsEmpty())
                {
                    //sql.Append($" AND IsDelete = N'{queryParam["IsDelete"]}'");
                    sql.Append($" AND IsDelete like N'%{queryParam["IsDelete"]}%'");
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
                //创建人名称 是否为空进行查询
                if (!queryParam["CreateName"].IsEmpty())
                {
                    //sql.Append($" AND CreateName = N'{queryParam["CreateName"]}'");
                    sql.Append($" AND CreateName like N'%{queryParam["CreateName"]}%'");
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
                //修改人名称 是否为空进行查询
                if (!queryParam["ModifyName"].IsEmpty())
                {
                    //sql.Append($" AND ModifyName = N'{queryParam["ModifyName"]}'");
                    sql.Append($" AND ModifyName like N'%{queryParam["ModifyName"]}%'");
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      a.[Id]
                      ,[SendFactoryCode]
                      ,[SendFactoryName]
                      ,[AcceptFactoryCode]
                      ,[AcceptFactoryName]
                      ,a.[ProductOrder]
                      ,a.[WorkOrder]
                      ,a.[MaterialCode]
                      ,[TransTypeCode]
                      ,[TransTypeName]
                      ,[TransProcessCode]
                      ,[TransProcessName]
                      ,[TransStateCode]
                      ,[TransStateName]
					   ,case WHEN c.Spec IS NULL THEN   PMA.Spec ELSE c.Spec END AS Spec
					   , case WHEN c.MMXH IS NULL THEN   PMA.MMXH ELSE c.MMXH END AS MMXH
					    ,case WHEN c.UV IS NULL THEN   PMA.UV ELSE c.UV END AS UV,
                               PMA.BWXH
					  ,CASE WHEN c.KCKX IS NULL THEN   PMA.KCKX ELSE c.KCKX END AS KCKX
					  ,c.OrderPiecesAll
                      ,c.OrderPiecesNum
					   , c.OrderPieces
                       ,c.OrderBox
                       ,c.OrderPallet
                      ,a.[Remark]
                      ,[IsDelete]
                      ,a.[Creator]
                      ,a.[CreateTime]
                      ,a.[CreateName]
                      ,a.[ModifyBy]
                      ,a.[ModifyTime]
                      ,[ModifyName]
                  FROM [dbo].[PL_TransfersRecord] 
				  a LEFT JOIN dbo.PL_ProductionOrder b ON a.ProductOrder=b.ProductOrder
				  LEFT JOIN dbo.PL_WorkOrder c ON a.WorkOrder= c.WorkOrder
				   LEFT JOIN dbo.fn_GetMaterialAttrs() PMA
                                ON c.WorkOrder = PMA.WorkOrder AND pma.FactoryCode=a.SendFactoryCode
				  WHERE IsDelete = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //ID 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //发起工厂编码 是否为空进行查询
                if (!queryParam["SendFactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND SendFactoryCode = N'{queryParam["SendFactoryCode"]}'");
                    sql.Append($" AND SendFactoryCode like N'%{queryParam["SendFactoryCode"]}%'");
                }
                //发起工厂名称 是否为空进行查询
                if (!queryParam["SendFactoryName"].IsEmpty())
                {
                    //sql.Append($" AND SendFactoryName = N'{queryParam["SendFactoryName"]}'");
                    sql.Append($" AND SendFactoryName like N'%{queryParam["SendFactoryName"]}%'");
                }
                //接收工厂编码 是否为空进行查询
                if (!queryParam["AcceptFactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND AcceptFactoryCode = N'{queryParam["AcceptFactoryCode"]}'");
                    sql.Append($" AND AcceptFactoryCode like N'%{queryParam["AcceptFactoryCode"]}%'");
                }
                //接收工厂名称 是否为空进行查询
                if (!queryParam["AcceptFactoryName"].IsEmpty())
                {
                    //sql.Append($" AND AcceptFactoryName = N'{queryParam["AcceptFactoryName"]}'");
                    sql.Append($" AND AcceptFactoryName like N'%{queryParam["AcceptFactoryName"]}%'");
                }
                //订单编码 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND b.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //工单号 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND a.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND (PMA.Spec like N'%{queryParam["Spec"]}%' or c.Spec like N'%{queryParam["Spec"]}%')");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND (PMA.MMXH like N'%{queryParam["MMXH"]}%' or c.MMXH like N'%{queryParam["MMXH"]}%' )");
                }
                //调拨类型:全工序调拨、部分工序调拨数据字典取值 是否为空进行查询
                if (!queryParam["TransTypeCode"].IsEmpty())
                {
                    //sql.Append($" AND TransTypeCode = N'{queryParam["TransTypeCode"]}'");
                    sql.Append($" AND TransTypeCode like N'%{queryParam["TransTypeCode"]}%'");
                }
                //调拨类型:全工序调拨、部分工序调拨数据字典取值 是否为空进行查询
                if (!queryParam["TransTypeName"].IsEmpty())
                {
                    //sql.Append($" AND TransTypeName = N'{queryParam["TransTypeName"]}'");
                    sql.Append($" AND TransTypeName like N'%{queryParam["TransTypeName"]}%'");
                }
                //调拨工序编码 是否为空进行查询
                if (!queryParam["TransProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND TransProcessCode = N'{queryParam["TransProcessCode"]}'");
                    sql.Append($" AND TransProcessCode like N'%{queryParam["TransProcessCode"]}%'");
                }
                //调拨工序名称 是否为空进行查询
                if (!queryParam["TransProcessName"].IsEmpty())
                {
                    //sql.Append($" AND TransProcessName = N'{queryParam["TransProcessName"]}'");
                    sql.Append($" AND TransProcessName like N'%{queryParam["TransProcessName"]}%'");
                }
                //调拨状态：0待确认、1已确认、2已回退 是否为空进行查询
                if (!queryParam["TransStateCode"].IsEmpty())
                {
                    //sql.Append($" AND TransStateCode = N'{queryParam["TransStateCode"]}'");
                    sql.Append($" AND TransStateCode like N'%{queryParam["TransStateCode"]}%'");
                }
                //调拨状态：0待确认、1已确认、2已回退 是否为空进行查询
                if (!queryParam["TransStateName"].IsEmpty())
                {
                    //sql.Append($" AND TransStateName = N'{queryParam["TransStateName"]}'");
                    sql.Append($" AND TransStateName like N'%{queryParam["TransStateName"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //删除标识1代表已删除0代表未删除 是否为空进行查询
                if (!queryParam["IsDelete"].IsEmpty())
                {
                    //sql.Append($" AND IsDelete = N'{queryParam["IsDelete"]}'");
                    sql.Append($" AND IsDelete like N'%{queryParam["IsDelete"]}%'");
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
                //创建人名称 是否为空进行查询
                if (!queryParam["CreateName"].IsEmpty())
                {
                    //sql.Append($" AND CreateName = N'{queryParam["CreateName"]}'");
                    sql.Append($" AND CreateName like N'%{queryParam["CreateName"]}%'");
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
                //修改人名称 是否为空进行查询
                if (!queryParam["ModifyName"].IsEmpty())
                {
                    //sql.Append($" AND ModifyName = N'{queryParam["ModifyName"]}'");
                    sql.Append($" AND ModifyName like N'%{queryParam["ModifyName"]}%'");
                }
                //queryName(选择弹窗关键名称) 是否为空进行查询
                if (!queryParam["queryName"].IsEmpty())
                {
                    //sql.Append($" AND 关键名称 = N'{queryParam["queryName"]}'");
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_TransfersRecordEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[SendFactoryCode]
                      ,[SendFactoryName]
                      ,[AcceptFactoryCode]
                      ,[AcceptFactoryName]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[MaterialCode]
                      ,[TransTypeCode]
                      ,[TransTypeName]
                      ,[TransProcessCode]
                      ,[TransProcessName]
                      ,[TransStateCode]
                      ,[TransStateName]
                      ,[Remark]
                      ,[IsDelete]
                      ,[Creator]
                      ,[CreateTime]
                      ,[CreateName]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[ModifyName]
                  FROM [dbo].[PL_TransfersRecord] where IsDeleted = 0 ");
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
        /// 调用存储过程进行全工序确认
        /// jpf 2023-1-5 
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="listPrd"></param>
        /// <returns></returns>
        public string PL_AllConfirmTransfersRecord(string Creator, List<PL_TransfersRecordEntity> listPrd)
        {
            var msg = "";
            DataTable dt = WorkOrderToDataTable(listPrd);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkOrders", dt),
                     new SqlParameter("@SendFactoryCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@AcceptFactoryCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@AcceptFactoryName", SqlDbType.VarChar, 50),
                    new SqlParameter("@create",SqlDbType.VarChar, 50),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };
            parameters[1].Value = listPrd[0].SendFactoryCode;
            parameters[2].Value = listPrd[0].AcceptFactoryCode;
            parameters[3].Value = listPrd[0].AcceptFactoryName;
         
            parameters[4].Value = Creator;
            parameters[5].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("PL_AllConfirmTransfersRecord", parameters);
                var Resultmsg = parameters[5].Value;
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
                    throw new Exception("工单调拨确认失败");
                }
            }
            return msg;

        }
        /// <summary>
        /// 调用存储过程进行确认
        /// jpf 2023-1-5 
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="listPrd"></param>
        /// <returns></returns>
        public string PL_ConfirmTransfersRecord(string Creator, List<PL_TransfersRecordEntity> listPrd,int SN,string StartOperation)
        {
            var msg = "";
            DataTable dt = WorkOrderToDataTable(listPrd);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkOrders", dt),
                     new SqlParameter("@SendFactoryCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@AcceptFactoryCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@AcceptFactoryName", SqlDbType.VarChar, 50),
                    new SqlParameter("@StartOperation", SqlDbType.VarChar, 50),
                    new SqlParameter("@create",SqlDbType.VarChar, 50),
                    new SqlParameter("@sn", SqlDbType.Int),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };
            parameters[1].Value = listPrd[0].SendFactoryCode;
            parameters[2].Value = listPrd[0].AcceptFactoryCode;
            parameters[3].Value = listPrd[0].AcceptFactoryName;
            parameters[4].Value = StartOperation;
            parameters[5].Value = Creator;
            parameters[6].Value = SN;
            parameters[7].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("PL_ConfirmTransfersRecord", parameters);
                var Resultmsg = parameters[7].Value;
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
                    throw new Exception("工单调拨确认失败");
                }
            }
            return msg;
        }
        /// <summary>
        /// 将List转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        private DataTable WorkOrderToDataTable(List<PL_TransfersRecordEntity> list)
        {
            if (list == null || list.Count == 0) return null;
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("id", System.Type.GetType("System.String"));
            dt.Columns.Add("SendFactoryCode", System.Type.GetType("System.String"));
            dt.Columns.Add("SendFactoryName", System.Type.GetType("System.String"));
            dt.Columns.Add("AcceptFactoryCode", System.Type.GetType("System.String"));
            dt.Columns.Add("AcceptFactoryName", System.Type.GetType("System.String"));
            dt.Columns.Add("WorkOrder", System.Type.GetType("System.String"));


            foreach (PL_TransfersRecordEntity item in list)
            {
                dt.Rows.Add(item.Id, item.SendFactoryCode, item.SendFactoryName, item.AcceptFactoryCode, 
                    item.AcceptFactoryName, item.WorkOrder
                   );
            }
            return dt;
        }
        /// <summary>
        /// 将调拨工单进行确认
        /// jpf 2022-12-2 
        /// </summary>
        /// <param name="entiy"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int ConTransferRecode( List<PL_TransfersRecordEntity> entiy, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                foreach (var item in entiy)
                {
                    
                    this.BaseRepository().Update(item);
                }


                n = 1;
            }
            catch (Exception ex)
            {
                msg = ex.Message;

            }
            return n;
        }
        /// <summary>
        /// 将调拨工单进行回退
        /// jpf 2022-12-2 
        /// </summary>
        /// <param name="entiy"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int RebackTransferRecode(string usercode,string userName,string TransStateCode,string TransStateName, List<PL_TransfersRecordEntity> entiy,out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                foreach(var item in entiy)
                {
                    item.TransStateCode = TransStateCode;
                    item.TransStateName = TransStateName;
                    item.ModifyBy = usercode;
                    item.ModifyName = userName;
                    item.ModifyTime = DateTime.Now;
                    this.BaseRepository().Update(item);
                }
               
               
                n = 1;
            }
            catch(Exception ex)
            {
                msg = ex.Message;

            }
            return n;
        }
        
        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PL_TransfersRecordEntity entity, out string msg)
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
                    entity.Create();
                    n = this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }
        
        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PL_TransfersRecordEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_TransfersRecordEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PL_TransfersRecord] set ");
                            string keyValue = "";
                            //循环实体
                            Save_obj.GetType().GetProperties().ToList().ForEach(x =>
                            {
                                if (x.Name == "ID")
                                {
                                    keyValue = x.GetValue(Save_obj, null).ToString();
                                }
                                else
                                {
                                    if (x.Name == "IsDeleted")
                                    {
                                        if (x.GetValue(Save_obj, null) != null)
                                        {
                                            sql_temp.Append(x.Name + "=" + (x.GetValue(Save_obj, null) == null ? 0 : (x.GetValue(Save_obj, null).ToString() == "true" ? 1: 0))+ ",");
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
                            sql.Append(sql_temp.ToString().TrimEnd(',')  + $" WHERE Id='{keyValue}';");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    //n = this.BaseRepository().Insert(entity_list);
                    StringBuilder sql = new StringBuilder();
                    sql.Append($@"INSERT INTO [dbo].[PL_TransfersRecord] (
                                            [Id]
                                            ,[SendFactoryCode]
                                            ,[SendFactoryName]
                                            ,[AcceptFactoryCode]
                                            ,[AcceptFactoryName]
                                            ,[ProductOrder]
                                            ,[WorkOrder]
                                            ,[MaterialCode]
                                            ,[TransTypeCode]
                                            ,[TransTypeName]
                                            ,[TransProcessCode]
                                            ,[TransProcessName]
                                            ,[TransStateCode]
                                            ,[TransStateName]
                                            ,[Remark]
                                            ,[IsDelete]
                                            ,[Creator]
                                            ,[CreateTime]
                                            ,[CreateName]
                                            ,[ModifyBy]
                                            ,[ModifyTime]
                                            ,[ModifyName]
                                    ) VALUES ");
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            sql.Append($@"(
                                N'{Save_obj.Id}'
                                ,N'{Save_obj.SendFactoryCode}'
                                ,N'{Save_obj.SendFactoryName}'
                                ,N'{Save_obj.AcceptFactoryCode}'
                                ,N'{Save_obj.AcceptFactoryName}'
                                ,N'{Save_obj.ProductOrder}'
                                ,N'{Save_obj.WorkOrder}'
                                ,N'{Save_obj.MaterialCode}'
                                ,N'{Save_obj.TransTypeCode}'
                                ,N'{Save_obj.TransTypeName}'
                                ,N'{Save_obj.TransProcessCode}'
                                ,N'{Save_obj.TransProcessName}'
                                ,N'{Save_obj.TransStateCode}'
                                ,N'{Save_obj.TransStateName}'
                                ,N'{Save_obj.Remark}'
                                ,'0'
                                ,N'{Save_obj.Creator}'
                                ,'{(Save_obj.CreateTime == null? DateTime.Now:Save_obj.CreateTime)}'
                                ,N'{Save_obj.CreateName}'
                                ,N'{Save_obj.ModifyBy}'
                                ,'{(Save_obj.ModifyTime == null? DateTime.Now:Save_obj.ModifyTime)}'
                                ,N'{Save_obj.ModifyName}'
                            ),");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString().TrimEnd(','));
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }
        
        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            PL_TransfersRecordEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                entity.IsDelete = true;
                this.BaseRepository().Update(entity);
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
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
                sql.Append($@"DELETE FROM [dbo].[PL_TransfersRecord] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PL_TransfersRecordEntity</returns>
        public PL_TransfersRecordEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PL_TransfersRecordEntity</returns>
        public PL_TransfersRecordEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        public PL_TransfersRecordEntity Get_ExpressionEntity(Expression<Func<PL_TransfersRecordEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PL_TransfersRecordEntity 列表</returns>
        public IEnumerable<PL_TransfersRecordEntity> Get_ExpressionList(Expression<Func<PL_TransfersRecordEntity, bool>> condition)
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
        //    RepositoryFactory<PL_TransfersRecordEntity> bomService = new RepositoryFactory<PL_TransfersRecordEntity>();

        //    PL_TransfersRecordEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PL_TransfersRecordDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PL_TransfersRecord_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_TransfersRecordEntity> GetList_TransfersRecord(List<PL_TransfersRecordEntity> entiy, out string msg)
        {
            var workorders = "";
            foreach (var item in entiy)
            {
                workorders = workorders + "," + "'" + item.WorkOrder + "'";
            }
            workorders = workorders.Substring(1, workorders.Length - 1);
            // = string.Join(",", entiy.Select(t => t.WorkOrder));

            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT * from  PL_TransfersRecord where IsDelete=0 and TransStateCode='0' and  WorkOrder in ({workorders})");
           
            msg = "";
            try
            {
                //执行 
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                //实体映射查询
                IEnumerable<PL_TransfersRecordEntity> PL_TransfersRecordEntity_list = db2.FindList<PL_TransfersRecordEntity>(sql.ToString());
                return PL_TransfersRecordEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_TransfersRecordEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PL_TransfersRecordEntity> PL_TransfersRecordEntity_list =  db2.FindList<PL_TransfersRecordEntity>(sql.ToString());
                return PL_TransfersRecordEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        
        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
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
                DataTable PL_TransfersRecordEntity_DataTable = db2.FindTable(sql.ToString());
                return PL_TransfersRecordEntity_DataTable;
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [SendFactoryCode] as '发起工厂编码'
                      ,[SendFactoryName] as '发起工厂名称'
                      ,[AcceptFactoryCode] as '接收工厂编码'
                      ,[AcceptFactoryName] as '接收工厂名称'
                      ,[ProductOrder] as '订单编码'
                      ,[WorkOrder] as '工单号'
                      ,[MaterialCode] as '物料编码'
                      ,[TransTypeCode] as '调拨类型:全工序调拨、部分工序调拨数据字典取值'
                      ,[TransTypeName] as '调拨类型:全工序调拨、部分工序调拨数据字典取值'
                      ,[TransProcessCode] as '调拨工序编码'
                      ,[TransProcessName] as '调拨工序名称'
                      ,[TransStateCode] as '调拨状态：0待确认、1已确认、2已回退'
                      ,[TransStateName] as '调拨状态：0待确认、1已确认、2已回退'
                      ,[Remark] as '备注'
                      ,[IsDelete] as '删除标识1代表已删除0代表未删除'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[CreateName] as '创建人名称'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                      ,[ModifyName] as '修改人名称'
                  FROM [dbo].[PL_TransfersRecord] where IsDeleted = 0 ");
            msg = "成功!";
            if (!checkType.IsEmpty())
            {
                //此处换上你的关键查询条件 也可以为空 查询全部
                sql.Append($@" and CreatedByCode = '{checkType}' ");
            }
            try
            {
               // DataTable dt = this.BaseRepository().FindList(sql.ToString());
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
                string saveFileName = "跨工厂调拨_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
               // MemoryStream ms = Excel.DataTableToExcel("跨工厂调拨", dt, true);
                //保存
                //Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
               // Excel.Dispose();
                return $@"{dirPath}{folder}{saveFileName}";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        
    }
}
