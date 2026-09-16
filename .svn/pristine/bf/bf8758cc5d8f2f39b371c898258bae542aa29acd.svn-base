using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.EquipmentManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using ALP.Application.UtilExtend.Util;
using System.IO;
//using ALP.Application.Service.QualityManage;

namespace ALP.Application.Service.EquipmentManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-05
    /// 2.创建作者: 王坤
    /// 3.功能描述: EP_EquipmentMaintainTaskService 业务服务类
    /// 4.任务编号: 设备保养项目详情
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentMaintainTask_Service : RepositoryFactory<EP_EquipmentMaintainTaskEntity>
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentMaintainTaskEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EquipmentIdentifyCode]
                      ,[EquipmentIdentifyStatus]
                      ,[EquipmentId]
                      ,[EquipmentMaintainTaskId]
                      ,[PlanDate]
                      ,[MaintainPerson]
                      ,[ActiveDate]
                      ,[Remark]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentMaintainTask] where IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //主键 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //保养工单号 是否为空进行查询
                if (!queryParam["EquipmentIdentifyCode"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentIdentifyCode = N'{queryParam["EquipmentIdentifyCode"]}'");
                    sql.Append($" AND EquipmentIdentifyCode like N'%{queryParam["EquipmentIdentifyCode"]}%'");
                }
                //工单状态 是否为空进行查询
                if (!queryParam["EquipmentIdentifyStatus"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentIdentifyStatus = N'{queryParam["EquipmentIdentifyStatus"]}'");
                    sql.Append($" AND EquipmentIdentifyStatus like N'%{queryParam["EquipmentIdentifyStatus"]}%'");
                }
                //设备编号 是否为空进行查询
                if (!queryParam["EquipmentId"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentId = N'{queryParam["EquipmentId"]}'");
                    sql.Append($" AND EquipmentId like N'%{queryParam["EquipmentId"]}%'");
                }
                //保养任务编号 是否为空进行查询
                if (!queryParam["EquipmentMaintainTaskId"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentMaintainTaskId = N'{queryParam["EquipmentMaintainTaskId"]}'");
                    sql.Append($" AND EquipmentMaintainTaskId like N'%{queryParam["EquipmentMaintainTaskId"]}%'");
                }
                //计划保养日期 是否为空进行查询
                if (!queryParam["PlanDate"].IsEmpty())
                {
                    //sql.Append($" AND PlanDate = N'{queryParam["PlanDate"]}'");
                    sql.Append($" AND PlanDate like N'%{queryParam["PlanDate"]}%'");
                }
                //保养人员 是否为空进行查询
                if (!queryParam["MaintainPerson"].IsEmpty())
                {
                    //sql.Append($" AND MaintainPerson = N'{queryParam["MaintainPerson"]}'");
                    sql.Append($" AND MaintainPerson like N'%{queryParam["MaintainPerson"]}%'");
                }
                //实际保养日期 是否为空进行查询
                if (!queryParam["ActiveDate"].IsEmpty())
                {
                    //sql.Append($" AND ActiveDate = N'{queryParam["ActiveDate"]}'");
                    sql.Append($" AND ActiveDate like N'%{queryParam["ActiveDate"]}%'");
                }
                //保养详情 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT A.[Id],
                               --,[EquipmentIdentifyCode]
                               --,[EquipmentIdentifyStatus]
                               A.FactoryCode,
                               A.FactoryName,
                               A.[EquipmentId],
                               B.EquipmentName,
                               [EquipmentMaintainTaskId],
                               C.EquipmentTaskName AS EquipmentMaintainTaskName,
                               A.ProcessCode,
                               E.ResourceName AS ProcessName,
                               CONVERT(VARCHAR(20), [PlanDate], 120) AS PlanDate,
                               [MaintainPerson],
                               [MaintenanceStatus],
                               D.Name AS MaintainPersonName,
                               CONVERT(VARCHAR(20), [ActiveDate], 120) AS ActiveDate,
                               dbo.get_dicName('menuMaintenanceStatus', A.MaintenanceStatus) AS MaintenanceStatusName,
                               A.[Remark],
                               A.[Creator],
                               A.[CreateTime],
                               A.[ModifyBy],
                               A.[ModifyTime]
                        FROM [dbo].[EP_EquipmentMaintainTask] AS A
                            LEFT JOIN [dbo].[V_EP_EquipmentManage] AS B
                                ON A.EquipmentId = B.EquipmentId
                            LEFT JOIN EP_EquipmentMaintain AS C
                                ON A.EquipmentMaintainTaskId = C.EquipmentTaskId
                                   AND C.EnabledMark = 1
                            LEFT JOIN BS_People AS D
                                ON A.MaintainPerson = D.Code
                                   AND D.IsEnabled = 1
                            LEFT JOIN BS_ModelWithResource AS E
                                ON A.ProcessCode = E.ResourceCode
                                   AND E.ModelLeve = 'Process'
                                   AND E.EnabledMark = 1
                        WHERE A.EnabledMark = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //主键 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //保养状态
                if (!queryParam["MaintenanceStatus"].IsEmpty())
                {
                    sql.Append($" and A.MaintenanceStatus='{queryParam["MaintenanceStatus"]}' ");
                }
                //工厂
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" and A.FactoryCode='{queryParam["FactoryCode"]}' ");
                }
                //工序
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" and A.ProcessCode='{queryParam["ProcessCode"]}' ");
                }
                //保养工单号 是否为空进行查询
                if (!queryParam["EquipmentIdentifyCode"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentIdentifyCode = N'{queryParam["EquipmentIdentifyCode"]}'");
                    sql.Append($" AND EquipmentIdentifyCode like N'%{queryParam["EquipmentIdentifyCode"]}%'");
                }
                //工单状态 是否为空进行查询
                if (!queryParam["EquipmentIdentifyStatus"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentIdentifyStatus = N'{queryParam["EquipmentIdentifyStatus"]}'");
                    sql.Append($" AND EquipmentIdentifyStatus like N'%{queryParam["EquipmentIdentifyStatus"]}%'");
                }
                //设备编号 是否为空进行查询
                if (!queryParam["EquipmentId"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentId = N'{queryParam["EquipmentId"]}'");
                    //sql.Append($" AND EquipmentId like N'%{queryParam["EquipmentId"]}%'");
                    sql.Append($" and CHARINDEX('{queryParam["EquipmentId"]}',A.EquipmentId)>0 ");
                }
                if (!queryParam["EquipmentName"].IsEmpty())
                {
                    sql.Append($" and CHARINDEX('{queryParam["EquipmentName"]}',B.EquipmentName)>0 ");
                }
                //保养任务编号 是否为空进行查询
                if (!queryParam["EquipmentMaintainTaskId"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentMaintainTaskId = N'{queryParam["EquipmentMaintainTaskId"]}'");
                    sql.Append($" AND EquipmentMaintainTaskId like N'%{queryParam["EquipmentMaintainTaskId"]}%'");
                }
                if (!queryParam["EquipmentTaskName"].IsEmpty())
                {
                    sql.Append($" and CHARINDEX('{queryParam["EquipmentTaskName"]}',C.EquipmentTaskName)>0 ");
                }
                //计划保养日期 是否为空进行查询
                if (!queryParam["PlanDate"].IsEmpty())
                {
                    //sql.Append($" AND PlanDate = N'{queryParam["PlanDate"]}'");
                    sql.Append($" AND PlanDate like N'%{queryParam["PlanDate"]}%'");
                }
                if (!queryParam["PlanStartDate"].IsEmpty())
                {
                    //sql.Append($" AND PlanDate = N'{queryParam["PlanDate"]}'");
                    sql.Append($" and A.PlanDate>='{Tools.CovertToDateStr(queryParam["PlanStartDate"])}' ");
                }
                if (!queryParam["PlanEndDate"].IsEmpty())
                {
                    sql.Append($" and A.PlanDate<='{Tools.CovertToNextDateStr(queryParam["PlanEndDate"])}' ");
                }
                //保养人员 是否为空进行查询
                if (!queryParam["MaintainPerson"].IsEmpty())
                {
                    //sql.Append($" AND MaintainPerson = N'{queryParam["MaintainPerson"]}'");
                    sql.Append($" AND MaintainPerson like N'%{queryParam["MaintainPerson"]}%'");
                }
                //实际保养日期 是否为空进行查询
                if (!queryParam["ActiveDate"].IsEmpty())
                {
                    //sql.Append($" AND ActiveDate = N'{queryParam["ActiveDate"]}'");
                    sql.Append($" AND ActiveDate like N'%{queryParam["ActiveDate"]}%'");
                }
                if (!queryParam["ActiveStartDate"].IsEmpty())
                {
                    //sql.Append($" AND PlanDate = N'{queryParam["PlanDate"]}'");
                    sql.Append($" and A.ActiveDate>='{Tools.CovertToDateStr(queryParam["ActiveStartDate"])}' ");
                }
                if (!queryParam["ActiveEndDate"].IsEmpty())
                {
                    sql.Append($" and A.ActiveDate<='{Tools.CovertToNextDateStr(queryParam["ActiveEndDate"])}' ");
                }
                //保养详情 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentMaintainTaskEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EquipmentIdentifyCode]
                      ,[EquipmentIdentifyStatus]
                      ,[EquipmentId]
                      ,[EquipmentMaintainTaskId]
                      ,[PlanDate]
                      ,[MaintainPerson]
                      ,[ActiveDate]
                      ,[Remark]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentMaintainTask] where IsDeleted = 0 ");
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

        public DataTable GetMaintenceResult(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            DataTable dt = new DataTable();
            var queryParam = queryJson.ToJObject();
            string Id = queryParam["Id"] == null ? "" : queryParam["Id"].ToString();
            string taskId = queryParam["TaskId"] == null ? "" : queryParam["TaskId"].ToString();
            #region 原王坤查询保养项目语句, 停用
            //      sql.Append($@"select distinct B.EquipmentTaskId,B.EquipmentTaskName,A.EquipmentMaintainId,A.EquipmentMaintainName,A.EquipmentMaintainStandard,A.DataType,
            //C.Id,C.EquipmentMaintainResult,C.ParentId,
            //case when A.DataType='1' or A.DataType='2' or A.DataType='3' then C.EquipmentMaintainResult 
            //when A.DataType='4' and C.EquipmentMaintainResult='1' then '合格' when A.DataType='4' and C.EquipmentMaintainResult='2' then '不合格'
            //when A.DataType='5' and C.EquipmentMaintainResult='1' then '是' when A.DataType='5' and C.EquipmentMaintainResult='2' then '否'
            //when A.DataType='6' and C.EquipmentMaintainResult='1' then '正常' when A.DataType='6' and C.EquipmentMaintainResult='2' then '不正常' end as EquipmentMaintainResultValue
            //                  from EP_EquipmentMaintainDetail as A
            //                  left join EP_EquipmentMaintain as B on A.EquipmentTaskId=B.EquipmentTaskId and B.EnabledMark=1 and B.IsUsed=1
            //                  left join EP_EquipmentMaintainResult as C on C.EquipmentMaintainId=A.EquipmentMaintainId and C.ParentId='{Id}'
            //                  where 1=1 and A.EnabledMark=1 and A.EquipmentTaskId='{taskId}' "); 
            #endregion
            //新  修改 刘万军  2021-9-27
            sql.Append($@"SELECT DISTINCT
                           B.EquipmentTaskId,
                           B.EquipmentTaskName,
                           A.EquipmentMaintainId,
                           A.EquipmentMaintainName,
                           A.EquipmentMaintainStandard,
                           A.DataType,
                           C.Id,
                           C.EquipmentMaintainResult,
                           C.ParentId,
                           C.EquipmentMaintainResult AS EquipmentMaintainResultValue
                    FROM EP_EquipmentMaintainDetail AS A
                        LEFT JOIN EP_EquipmentMaintain AS B
                            ON A.EquipmentTaskId = B.EquipmentTaskId
                               AND B.EnabledMark = 1
                               AND B.IsUsed = 1
                        LEFT JOIN EP_EquipmentMaintainResult AS C
                            ON C.EquipmentMaintainId = A.EquipmentMaintainId
                               AND C.ParentId = '{Id}'
                    WHERE 1 = 1
                          AND A.EnabledMark = 1
                          AND A.EquipmentTaskId = '{taskId}' ");
            if (pagination == null)
                dt = this.BaseRepository().FindTable(sql.ToString());
            else
                dt = this.BaseRepository().FindTable(sql.ToString(), pagination);
            return dt;
        }
        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, EP_EquipmentMaintainTaskEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    //entity.MaintenanceStatus = "2";
                    this.BaseRepository().Update(entity);
                    n = 2;
                }
                else
                {
                    RepositoryFactory<EP_EquipmentManage> equipmentService = new RepositoryFactory<EP_EquipmentManage>();
                    EP_EquipmentMaintainTaskEntity _entity = this.BaseRepository().FindEntity(t => t.EquipmentId == entity.EquipmentId && t.EquipmentMaintainTaskId == entity.EquipmentMaintainTaskId && t.EnabledMark == true);
                    if (_entity == null)
                    {
                        EP_EquipmentManage equipmentEntity = equipmentService.BaseRepository().FindEntity(t => t.EquipmentId == entity.EquipmentId);
                        if (equipmentEntity != null)
                        {
                            entity.ProcessCode = equipmentEntity.ProcessBelong;
                        }
                        entity.Create();
                        entity.MaintenanceStatus = "2";
                        this.BaseRepository().Insert(entity);
                        n = 1;
                    }
                    else
                    {
                        n = 3;
                    }
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<EP_EquipmentMaintainTaskEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<EP_EquipmentMaintainTaskEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[EP_EquipmentMaintainTask] set ");
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
                    //n = this.BaseRepository().Insert(entity_list);
                    StringBuilder sql = new StringBuilder();
                    sql.Append($@"INSERT INTO [dbo].[EP_EquipmentMaintainTask] (
                                            [Id]
                                            ,[EquipmentIdentifyCode]
                                            ,[EquipmentIdentifyStatus]
                                            ,[EquipmentId]
                                            ,[EquipmentMaintainTaskId]
                                            ,[PlanDate]
                                            ,[MaintainPerson]
                                            ,[ActiveDate]
                                            ,[Remark]
                                            ,[Creator]
                                            ,[CreateTime]
                                            ,[ModifyBy]
                                            ,[ModifyTime]
                                    ) VALUES ");
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            sql.Append($@"(
                                N'{Save_obj.Id}'
                                ,N'{Save_obj.EquipmentIdentifyCode}'
                                ,N'{Save_obj.EquipmentIdentifyStatus}'
                                ,N'{Save_obj.EquipmentId}'
                                ,N'{Save_obj.EquipmentMaintainTaskId}'
                                ,'{(Save_obj.PlanDate == null ? DateTime.Now : Save_obj.PlanDate)}'
                                ,N'{Save_obj.MaintainPerson}'
                                ,'{(Save_obj.ActiveDate == null ? DateTime.Now : Save_obj.ActiveDate)}'
                                ,N'{Save_obj.Remark}'
                                ,N'{Save_obj.Creator}'
                                ,'{(Save_obj.CreateTime == null ? DateTime.Now : Save_obj.CreateTime)}'
                                ,N'{Save_obj.ModifyBy}'
                                ,'{(Save_obj.ModifyTime == null ? DateTime.Now : Save_obj.ModifyTime)}'
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            EP_EquipmentMaintainTaskEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                entity.EnabledMark = false;
                entity.ModifyBy = UpdateByName;
                entity.ModifyTime = DateTime.Now;
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
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
                sql.Append($@"DELETE FROM [dbo].[EP_EquipmentMaintainTask] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回EP_EquipmentMaintainTaskEntity</returns>
        public EP_EquipmentMaintainTaskEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回EP_EquipmentMaintainTaskEntity</returns>
        public EP_EquipmentMaintainTaskEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentMaintainTaskEntity 对象</returns>
        public EP_EquipmentMaintainTaskEntity Get_ExpressionEntity(Expression<Func<EP_EquipmentMaintainTaskEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentMaintainTaskEntity 列表</returns>
        public IEnumerable<EP_EquipmentMaintainTaskEntity> Get_ExpressionList(Expression<Func<EP_EquipmentMaintainTaskEntity, bool>> condition)
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
        //    RepositoryFactory<EP_EquipmentMaintainTaskEntity> bomService = new RepositoryFactory<EP_EquipmentMaintainTaskEntity>();

        //    EP_EquipmentMaintainTaskEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    EP_EquipmentMaintainTaskDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.EP_EquipmentMaintainTask_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentMaintainTaskEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<EP_EquipmentMaintainTaskEntity> EP_EquipmentMaintainTaskEntity_list = db2.FindList<EP_EquipmentMaintainTaskEntity>(sql.ToString());
                return EP_EquipmentMaintainTaskEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
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
                DataTable EP_EquipmentMaintainTaskEntity_DataTable = db2.FindTable(sql.ToString());
                return EP_EquipmentMaintainTaskEntity_DataTable;
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        /*public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [EquipmentIdentifyCode] as '保养工单号'
                      ,[EquipmentIdentifyStatus] as '工单状态'
                      ,[EquipmentId] as '设备编号'
                      ,[EquipmentMaintainTaskId] as '保养任务编号'
                      ,[PlanDate] as '计划保养日期'
                      ,[MaintainPerson] as '保养人员'
                      ,[ActiveDate] as '实际保养日期'
                      ,[Remark] as '保养详情'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[EP_EquipmentMaintainTask] where IsDeleted = 0 ");
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
                string saveFileName = "设备保养项目详情_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("设备保养项目详情", dt, true);
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
        }*/

        /// <summary>
        /// 功能描述: 根据时间范围和设备类别 获取未保养的任务
        /// 创　　建: 刘万军
        /// 创建日期: 2021-9-25 10:09:17
        /// 任务编号: 设备保养任务
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public List<dynamic> GetEquipmentMaintainTaskByDateAndType(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT A.[Id],
                               A.FactoryCode,
                               A.FactoryName,
                               A.[EquipmentId],
                               B.EquipmentName,
                               [EquipmentMaintainTaskId],
                               C.EquipmentTaskName AS EquipmentMaintainTaskName,
                               A.ProcessCode,
                               E.ResourceName AS ProcessName,
                               CONVERT(VARCHAR(20), [PlanDate], 120) AS PlanDate,
                               [MaintainPerson],
                               [MaintenanceStatus],
                               D.Name AS MaintainPersonName,
                               CONVERT(VARCHAR(20), [ActiveDate], 120) AS ActiveDate,
                               dbo.get_dicName('menuMaintenanceStatus', A.MaintenanceStatus) AS MaintenanceStatusName,
                               A.[Remark],
                               A.[Creator],
                               A.[CreateTime],
                               A.[ModifyBy],
                               A.[ModifyTime],
                               B.EquipmentType
                        FROM [dbo].[EP_EquipmentMaintainTask] AS A
                            LEFT JOIN [dbo].[V_EP_EquipmentManage] AS B
                                ON A.EquipmentId = B.EquipmentId
                            LEFT JOIN EP_EquipmentMaintain AS C
                                ON A.EquipmentMaintainTaskId = C.EquipmentTaskId
                                   AND C.EnabledMark = 1
                            LEFT JOIN BS_People AS D
                                ON A.MaintainPerson = D.Code
                                   AND D.IsEnabled = 1
                            LEFT JOIN BS_ModelWithResource AS E
                                ON A.ProcessCode = E.ResourceCode
                                   AND E.ModelLeve = 'Process'
                                   AND E.EnabledMark = 1
                        WHERE A.EnabledMark = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryCode = '{queryParam["FactoryCode"]}'");
                }
                //查询条件        设备类别      
                if (!queryParam["RepairingType"].IsEmpty())
                {
                    sql.Append($" AND B.EquipmentType = '{queryParam["RepairingType"]}'");
                }
                if (!queryParam["StartDate"].IsEmpty() && !queryParam["EndDate"].IsEmpty())
                {
                    sql.Append($"  AND A.PlanDate BETWEEN N'{queryParam["StartDate"]}' AND  N'{queryParam["EndDate"]} 23:59:59'");
                }
               
                //未保养
                sql.Append($" AND A.MaintenanceStatus = '2'");
            }
            try
            {
                return this.BaseRepository().Query(sql.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #region App获取设备保养任务
        /// <summary>
        /// 设备保养-获取设备保养任务
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<dynamic> GetEquipmentMaintainTask(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT
					ET.Id,ET.EquipmentId,EM.EquipmentName,CONVERT(VARCHAR(16),ET.PlanDate,120)PlanDate,
					EMT.EquipmentTaskName,ET.EquipmentMaintainTaskId
				  FROM dbo.[EP_EquipmentMaintainTask] ET
				  LEFT JOIN dbo.EP_EquipmentManage EM ON EM.EquipmentId=ET.EquipmentId
				  LEFT JOIN [dbo].[EP_EquipmentMaintain] EMT ON EMT.EquipmentTaskId=ET.EquipmentMaintainTaskId
				  WHERE ET.MaintenanceStatus='1' AND ET.EnabledMark='1' ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["EquipmentType"].IsEmpty())
                {
                    sql.Append($" AND EM.EquipmentType = N'{queryParam["EquipmentType"]}'");
                }
                if (!queryParam["StartDate"].IsEmpty())
                {
                    sql.Append($" AND ET.PlanDate >= N'{queryParam["StartDate"]}'");
                }
                if (!queryParam["EndDate"].IsEmpty())
                {
                    sql.Append($" AND ET.PlanDate <= N'{queryParam["EndDate"]}'");
                }
            }
            return this.BaseRepository().Query(sql.ToString());
        }
        #endregion   
    }
}
