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
using ALP.Application.UtilExtend.Offices;

namespace ALP.Application.Service.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-03
    /// 2.创建作者: admin
    /// 3.功能描述: PM_ExeWorkOrderSWService 业务服务类
    /// 4.任务编号: 派工执行工单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_ExeWorkOrderSW_Service : RepositoryFactory<PM_ExeWorkOrderSWEntity>
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ExeWorkOrderSWEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[ExeWorkOrder]
                      ,[SWSeq]
                      ,[ProcessCode]
                      ,[EquipCode]
                      ,[PlanProductTime]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[SWStatus]
                  FROM [dbo].[PM_ExeWorkOrderSW] where 1=1 ");
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
                //订单号 是否为空进行查询
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
                //执行工单号 是否为空进行查询
                if (!queryParam["ExeWorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND ExeWorkOrder = N'{queryParam["ExeWorkOrder"]}'");
                    sql.Append($" AND ExeWorkOrder like N'%{queryParam["ExeWorkOrder"]}%'");
                }
                //派工顺序 是否为空进行查询
                if (!queryParam["SWSeq"].IsEmpty())
                {
                    //sql.Append($" AND SWSeq = N'{queryParam["SWSeq"]}'");
                    sql.Append($" AND SWSeq like N'%{queryParam["SWSeq"]}%'");
                }
                //工序编码 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND ProcessCode = N'{queryParam["ProcessCode"]}'");
                    sql.Append($" AND ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                }
                //机台编码 是否为空进行查询
                if (!queryParam["EquipCode"].IsEmpty())
                {
                    //sql.Append($" AND EquipCode = N'{queryParam["EquipCode"]}'");
                    sql.Append($" AND EquipCode like N'%{queryParam["EquipCode"]}%'");
                }
                //计划生产时间 是否为空进行查询
                if (!queryParam["PlanProductTime"].IsEmpty())
                {
                    //sql.Append($" AND PlanProductTime = N'{queryParam["PlanProductTime"]}'");
                    sql.Append($" AND PlanProductTime like N'%{queryParam["PlanProductTime"]}%'");
                }
                //派工人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //派工时间 是否为空进行查询
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
                //派工生产状态 是否为空进行查询
                if (!queryParam["SWStatus"].IsEmpty())
                {
                    //sql.Append($" AND SWStatus = N'{queryParam["SWStatus"]}'");
                    sql.Append($" AND SWStatus like N'%{queryParam["SWStatus"]}%'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.Id,
                               a.ExeWorkOrder,
                               a.FactoryCode,
							   a.FactoryName,
                               a.ProcessCode,
                               v.ResourceName ProcessName,
                               a.EquipCode,
                               e.ResourceName EquipName,
                               a.CreateTime,
                               a.PlanProductTime,
                               a.WorkOrder,
                               c.WorkOrderType,
                               v1.ItemName WorkOrderTypeName,
                               a.SWStatus,
                               a.ProductOrder,
                               c.ContainerNO,
                               f.MMXH,
                               f.MMCJ,
                               f.SmallClass,
							   v2.ItemName SmallClassName,
                               f.Spec,
                               f.BWXH,
                               f.UV,
                               f.KCKX,
                               c.ActualSheets,
                               c.OrderPieces,
                               g.BGQty,
                               a.SWSeq
                        FROM dbo.PM_ExeWorkOrderSW a
                            INNER JOIN dbo.PL_WorkOrder c
                                ON a.WorkOrder = c.WorkOrder
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'WorkOrderType'
                                   AND c.WorkOrderType = v1.ItemValue
                            LEFT JOIN dbo.BS_ModelWithResource v
                                ON v.ModelLeve = 'Process'
                                   --AND v.ResourceCode IN ( 'FH_JC', 'FH_KC' )
                                   AND a.ProcessCode = v.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource e
                                ON e.ModelLeve = 'Machine'
                                   --AND e.ParentResource IN ( 'FH_JC', 'FH_KC' )
                                   AND a.EquipCode = e.ResourceCode
                            LEFT JOIN dbo.fn_GetMaterialAttrs() f
                                ON c.MaterialCode = f.MaterialCode
                                   AND c.WorkOrder = f.WorkOrder
                            LEFT JOIN
                            (
                                SELECT g1.WorkOrder,
                                       g1.MachineCode,
                                       ISNULL(SUM(g1.Qty), 0) BGQty
                                FROM dbo.PM_TranferCardBGRecord g1
                                WHERE g1.IsEnabled = 1
                                GROUP BY g1.WorkOrder,
                                         g1.MachineCode
                            ) g
                                ON a.WorkOrder = g.WorkOrder
                                   AND a.EquipCode = g.MachineCode
							LEFT JOIN dbo.V_DataDictionary v2 ON v2.EnCode='MaterialSmall' AND f.SmallClass=v2.ItemValue
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //工厂 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND c.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //工序编码 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND a.ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                }
                //机台 是否为空进行查询
                if (!queryParam["EquipCode"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND a.EquipCode like N'%{queryParam["EquipCode"]}%'");
                }
                //计划生产开始时间 是否为空进行查询
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND a.PlanProductTime>=  N'{queryParam["StartTime"]}'");
                }
                //计划生产结束时间 是否为空进行查询
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND a.PlanProductTime<= N'{queryParam["EndTime"]}'");
                }
                //执行工单 是否为空进行查询
                if (!queryParam["ExeWorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND SWSeq = N'{queryParam["SWSeq"]}'");
                    sql.Append($" AND a.ExeWorkOrder like N'%{queryParam["ExeWorkOrder"]}%'");
                }
                //执行工单 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND SWSeq = N'{queryParam["SWSeq"]}'");
                    sql.Append($" AND a.WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //面膜型号 是否为空进行查询
                if (!queryParam["MMXH"].IsEmpty())
                {
                    //sql.Append($" AND EquipCode = N'{queryParam["EquipCode"]}'");
                    sql.Append($" AND f.MMXH like N'%{queryParam["MMXH"]}%'");
                }
                //板纹型号 是否为空进行查询
                if (!queryParam["BWXH"].IsEmpty())
                {
                    //sql.Append($" AND EquipCode = N'{queryParam["EquipCode"]}'");
                    sql.Append($" AND f.BWXH like N'%{queryParam["BWXH"]}%'");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND ProcessCode = N'{queryParam["ProcessCode"]}'");
                    sql.Append($" AND f.Spec like N'%{queryParam["Spec"]}%'");
                }

                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    //sql.Append($" AND PlanProductTime = N'{queryParam["PlanProductTime"]}'");
                    sql.Append($" AND c.ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                //派工人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //扣型 是否为空进行查询
                if (!queryParam["KCKX"].IsEmpty())
                {
                    //sql.Append($" AND CreateTime = N'{queryParam["CreateTime"]}'");
                    sql.Append($" AND f.KCKX like N'%{queryParam["KCKX"]}%'");
                }
                //规格 是否为空进行查询
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    //sql.Append($" AND ModifyBy = N'{queryParam["ModifyBy"]}'");
                    sql.Append($" AND f.SmallClass like N'%{queryParam["SmallClass"]}%'");
                }
                //派工生产状态 是否为空进行查询
                if (!queryParam["SWStatus"].IsEmpty())
                {
                    //sql.Append($" AND ModifyTime = N'{queryParam["ModifyTime"]}'");
                    sql.Append($" AND a.SWStatus like N'%{queryParam["SWStatus"]}%'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    sql.Append($" order by SWSeq ");
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
        /// 功能描述: 查询未开工、正在生产、当前工序没有派工的的执行工单
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="pagination">查询参数</param>
        /// /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetExeWorkOrderDataTableList(Pagination pagination,string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                               c.FactoryCode,
							   c.FactoryName,
                               v.ResourceCode ProcessCode,
                               v.ResourceName ProcessName,
                               c.WorkOrder,
                               c.WorkOrderType,
                               v1.ItemName WorkOrderTypeName,
                               c.ProductOrder,
                               c.ContainerNO,
                               f.MMXH,
                               f.MMCJ,
                               f.SmallClass,
                               v2.ItemName SmallClassName,
                               f.Spec,
                               f.BWXH,
                               f.UV,
                               f.KCKX,
                               c.ActualSheets,
                               c.OrderPieces,c.OrderStatus,
                               0 BGQty,
							   v3.ItemName AssignStatus
                        FROM dbo.PL_WorkOrder c
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'WorkOrderType'
                                   AND c.WorkOrderType = v1.ItemValue
                            LEFT JOIN dbo.BS_ModelWithResource v
                                ON v.ModelLeve = 'Process'
                                   AND v.EnabledMark = 1
                                   AND v.ParentResource IN
                                       (
                                           SELECT ResourceCode --车间
                                           FROM dbo.BS_ModelWithResource
                                           WHERE ParentResource =
                                           (
                                               SELECT TOP 1
                                                      ResourceCode --工厂
                                               FROM dbo.BS_ModelWithResource
                                               WHERE ModelLeve = 'Factory'
                                                     AND EnabledMark = 1
                                                     AND ResourceCode = c.FactoryCode
                                           )
                                                 AND EnabledMark = 1
                                       )

                            LEFT JOIN dbo.fn_GetMaterialAttrs() f
                                ON c.MaterialCode = f.MaterialCode
                                   AND c.WorkOrder = f.WorkOrder
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'MaterialSmall'
                                   AND f.SmallClass = v2.ItemValue
						    LEFT JOIN dbo.V_DataDictionary v3 ON v3.EnCode='AssignStatus' AND ISNULL(c.AssignStatus,1)=v3.ItemValue
                        WHERE  c.IsEnabled = 1
                            AND c.OrderStatus IN('3','4')                          
                              AND EXISTS
                        (
                            SELECT 1
                            FROM dbo.PL_Process p1
                                INNER JOIN dbo.PL_ProcessOfOperations po1
                                    ON p1.Id = po1.ProcessId
                            WHERE po1.SN >=
                            (
                                SELECT TOP 1
                                       po.SN
                                FROM dbo.PL_Process p
                                    INNER JOIN dbo.PL_ProcessOfOperations po
                                        ON p.Id = po.ProcessId
                                WHERE c.WorkOrder = p.WorkOrder
                                      AND po.OperationCode = c.StartOperation
                            )
                                  AND po1.OperationCode = v.ResourceCode
                        ) ");
            var parameter = new List<DbParameter>();

            try
            {
                if (!string.IsNullOrEmpty(queryJson))
                {
                    JObject queryParam = queryJson.ToJObject();
                    if (!queryParam["FactoryCode"].IsEmpty())
                    {
                        sql.Append($" AND c.FactoryCode = N'{queryParam["FactoryCode"]}'");
                    }
                    if (!queryParam["Process"].IsEmpty())
                    {
                        sql.Append($" AND v.ResourceName like N'%{queryParam["Process"]}%'");
                    }
                    if (!queryParam["ProcessCode"].IsEmpty())
                    {
                        sql.Append($" AND v.ResourceCode = N'{queryParam["ProcessCode"]}' ");
                    }
                    if (!queryParam["ProductOrder"].IsEmpty())
                    {
                        sql.Append($" AND c.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                    }
                    if (!queryParam["ContainerNO"].IsEmpty())
                    {
                        sql.Append($" AND c.ContainerNO = N'{queryParam["ContainerNO"]}'");
                    }
                    if (!queryParam["AssignStatus"].IsEmpty())
                    {
                        sql.Append($" AND ISNULL(c.AssignStatus,'1') = N'{queryParam["AssignStatus"]}'");
                    }
                    //if (!queryParam["Status"].IsEmpty())
                    //{
                    //    sql.Append($" AND b.Status = N'{queryParam["Status"]}'");
                    //}

                    //面膜型号
                    if (!queryParam["MMXH"].IsEmpty())
                    {
                        sql.Append($" AND f.MMXH like N'%{queryParam["MMXH"]}%'");
                    }
                    //版纹型号
                    if (!queryParam["BWXH"].IsEmpty())
                    {
                        sql.Append($" AND f.BWXH like N'%{queryParam["BWXH"]}%'");
                    }
                }
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ExeWorkOrderSWEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[ExeWorkOrder]
                      ,[SWSeq]
                      ,[ProcessCode]
                      ,[EquipCode]
                      ,[PlanProductTime]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[SWStatus]
                  FROM [dbo].[PM_ExeWorkOrderSW] where IsDeleted = 0 ");
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
        /// 功能描述: 工单信息
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 工单信息
        /// </summary>
        /// <param name="exeWorkOrder">查询条件</param>
        /// <returns>返回分页列表</returns>
        public object GetExeWorkOrderInfo(string exeWorkOrder, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT a.ProductOrder,
                               a.CustomerPO,
                               a.MaterialCode,
                               b.MMXH,
                               b.MMCJ,
                               b.Spec,
                               b.BWXH,
                               a.OrderPieces,
                               a.OrderBox,
                               c.ResourceName FactoryName,
                               a.OrderStartPallet,
                               a.OrderPallet,
                               a.ContainerNO,
                               b.UV,
                               b.KCKX,
                               p.DeliveryDate,
                               p.OrderDate,
                               b.XL,
                               b.SmallClass,
							   v1.ItemName SmallClassName,
                               b.DXZH,
                               b.Size,
                               a.ActualSheets,
                               b.JCGG,
                               b.TPGG
                        FROM  dbo.PL_WorkOrder a
                            INNER JOIN dbo.PL_ProductionOrder p
                                ON a.ProductOrder = p.ProductOrder
                            LEFT JOIN dbo.fn_GetMaterialAttrs() b
                                ON a.MaterialCode = b.MaterialCode AND a.WorkOrder=b.WorkOrder
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.ModelLeve = 'Factory'
                                   AND a.FactoryCode = c.ResourceCode
							LEFT JOIN dbo.V_DataDictionary v1 ON v1.EnCode='MaterialSmall' AND b.SmallClass=v1.ItemValue
                        WHERE 1 = 1

				
                              AND a.WorkOrder = '{exeWorkOrder}' ");

            msg = "";
            try
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PM_ExeWorkOrderSWEntity entity, out string msg)
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
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_ExeWorkOrderSWEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, List<PM_ExeWorkOrderSWEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PM_ExeWorkOrderSW] set ");
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
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            PM_ExeWorkOrderSWEntity entity = this.BaseRepository().FindEntity(keyValue);
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
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
                sql.Append($@"DELETE FROM [dbo].[PM_ExeWorkOrderSW] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_ExeWorkOrderSWEntity</returns>
        public PM_ExeWorkOrderSWEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }


        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_ExeWorkOrderSWEntity</returns>
        public PM_ExeWorkOrderSWEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_ExeWorkOrderSWEntity 列表</returns>
        public IEnumerable<PM_ExeWorkOrderSWEntity> Get_ExpressionList(Expression<Func<PM_ExeWorkOrderSWEntity, bool>> condition)
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
        //    RepositoryFactory<PM_ExeWorkOrderSWEntity> bomService = new RepositoryFactory<PM_ExeWorkOrderSWEntity>();

        //    PM_ExeWorkOrderSWEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PM_ExeWorkOrderSWDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PM_ExeWorkOrderSW_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ExeWorkOrderSWEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PM_ExeWorkOrderSWEntity> PM_ExeWorkOrderSWEntity_list = db2.FindList<PM_ExeWorkOrderSWEntity>(sql.ToString());
                return PM_ExeWorkOrderSWEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
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
                DataTable PM_ExeWorkOrderSWEntity_DataTable = db2.FindTable(sql.ToString());
                return PM_ExeWorkOrderSWEntity_DataTable;
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [ProductOrder] as '订单号'
                      ,[WorkOrder] as '工单号'
                      ,[ExeWorkOrder] as '执行工单号'
                      ,[SWSeq] as '派工顺序'
                      ,[ProcessCode] as '工序编码'
                      ,[EquipCode] as '机台编码'
                      ,[PlanProductTime] as '计划生产时间'
                      ,[Creator] as '派工人'
                      ,[CreateTime] as '派工时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                      ,[SWStatus] as '派工生产状态'
                  FROM [dbo].[PM_ExeWorkOrderSW] where IsDeleted = 0 ");
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
                string saveFileName = "派工执行工单_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("派工执行工单", dt, true);
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

    }
}
